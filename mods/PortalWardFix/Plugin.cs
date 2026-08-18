using System;
using System.Collections;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace PortalWardFix
{
    /// <summary>
    /// WardIsLove 3.7.2: IsInside clobbers m_radius with the last ward in m_allAreas,
    /// so CheckAccess / chest Interact / CheckInWardMonoscript all lie. Postfix restores
    /// DistanceXZ vs this ward's GetWardRadius. Teleport force-allow only when the portal
    /// is honestly outside every enabled ward (does not bypass NoTeleport).
    /// </summary>
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency("Azumatt.WardIsLove", BepInDependency.DependencyFlags.HardDependency)]
    [BepInProcess("valheim.exe")]
    [BepInProcess("valheim_server.exe")]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid = "yanlo.PortalWardFix";
        public const string PluginName = "Portal Ward Fix";
        public const string PluginVersion = "1.1.0";

        private readonly Harmony _harmony = new Harmony(PluginGuid);
        private bool _tpHooked;
        private bool _insideHooked;

        private void Awake()
        {
            ArmHook("Awake");
        }

        private void Start()
        {
            if (!_tpHooked || !_insideHooked)
                ArmHook("Start");
        }

        private void ArmHook(string phase)
        {
            if (!_insideHooked)
                _insideHooked = TryHookIsInside();
            if (!_tpHooked)
                _tpHooked = TryHookWilTeleportPrefix();

            if (_insideHooked && _tpHooked)
                Logger.LogInfo($"[{PluginName}] {PluginVersion} hooked IsInside + teleport ({phase})");
            else if (_insideHooked || _tpHooked)
                Logger.LogWarning($"[{PluginName}] partial hook ({phase}): IsInside={_insideHooked} teleport={_tpHooked}");
            else
                Logger.LogWarning($"[{PluginName}] WardIsLove patches not found ({phase})");
        }

        private bool TryHookIsInside()
        {
            Type t = AccessTools.TypeByName("WardIsLove.Util.WardMonoscript");
            MethodInfo m = t != null
                ? AccessTools.Method(t, "IsInside", new[] { typeof(Vector3), typeof(float) })
                : null;
            if (m == null) return false;

            WilRadius.Ensure(t);
            _harmony.Patch(m,
                postfix: new HarmonyMethod(typeof(WilIsInsideFix), nameof(WilIsInsideFix.Postfix))
                {
                    priority = Priority.Last
                });
            return true;
        }

        private bool TryHookWilTeleportPrefix()
        {
            Type t = AccessTools.TypeByName("WardIsLove.PatchClasses.TeleportWorldTeleportPatch");
            if (t == null)
            {
                foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (asm.GetName().Name != "WardIsLove") continue;
                    foreach (var type in asm.GetTypes())
                    {
                        if (type.Name == "TeleportWorldTeleportPatch")
                        {
                            t = type;
                            break;
                        }
                    }
                }
            }

            if (t == null) return false;
            MethodInfo prefix = AccessTools.Method(t, "Prefix");
            if (prefix == null) return false;

            Type wardMono = AccessTools.TypeByName("WardIsLove.Util.WardMonoscript");
            if (wardMono != null)
                WilRadius.Ensure(wardMono);

            _harmony.Patch(prefix,
                postfix: new HarmonyMethod(typeof(WilTeleportFix), nameof(WilTeleportFix.Postfix))
                {
                    priority = Priority.Last
                });
            return true;
        }
    }

    internal static class WilRadius
    {
        private static bool _resolved;
        private static MethodInfo _getWardRadius;
        private static MethodInfo _isEnabled;
        private static FieldInfo _allAreas;

        internal static void Ensure(Type wardMono)
        {
            if (_resolved || wardMono == null) return;
            _resolved = true;

            Type ext = AccessTools.TypeByName("WardIsLove.Extensions.WardMonoscriptExt");
            _getWardRadius = ext != null
                ? AccessTools.Method(ext, "GetWardRadius", new[] { wardMono })
                : null;
            _isEnabled = AccessTools.Method(wardMono, "IsEnabled");
            _allAreas = AccessTools.Field(wardMono, "m_allAreas");
        }

        internal static bool TryGetRadius(object ward, out float radius)
        {
            radius = 0f;
            if (ward == null || _getWardRadius == null) return false;
            try
            {
                object r = _getWardRadius.Invoke(null, new[] { ward });
                if (!(r is float f)) return false;
                radius = f;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// True if any enabled ward's own GetWardRadius covers point (XZ).
        /// Does not call WiL IsInside (avoids clobber / NRE / this postfix).
        /// </summary>
        internal static bool HonestlyInsideAnyEnabled(Vector3 point)
        {
            if (_allAreas == null) return false;
            object all;
            try
            {
                all = _allAreas.GetValue(null);
            }
            catch
            {
                return false;
            }
            if (!(all is IEnumerable areas)) return false;

            foreach (object area in areas)
            {
                var mb = area as MonoBehaviour;
                if (mb == null) continue;
                try
                {
                    if (_isEnabled != null)
                    {
                        object en = _isEnabled.Invoke(area, null);
                        if (!(en is bool eb) || !eb) continue;
                    }
                    if (!TryGetRadius(area, out float r)) continue;
                    if (DistXZ(mb.transform.position, point) < r)
                        return true;
                }
                catch
                {
                    // skip broken ward
                }
            }
            return false;
        }

        internal static float DistXZ(Vector3 a, Vector3 b)
        {
            float dx = a.x - b.x;
            float dz = a.z - b.z;
            return Mathf.Sqrt(dx * dx + dz * dz);
        }
    }

    internal static class WilIsInsideFix
    {
        public static void Postfix(MonoBehaviour __instance, Vector3 point, float radius, ref bool __result)
        {
            if (__instance == null) return;
            if (!WilRadius.TryGetRadius(__instance, out float r)) return;
            __result = WilRadius.DistXZ(__instance.transform.position, point) < r + radius;
        }
    }

    internal static class WilTeleportFix
    {
        public static void Postfix(ref bool __result, object[] __args)
        {
            if (__result) return;

            TeleportWorld tw = null;
            try
            {
                if (__args != null)
                {
                    foreach (var a in __args)
                    {
                        if (a is TeleportWorld t) tw = t;
                    }
                }
            }
            catch { /* */ }

            if (tw == null) return;

            // WiL denied. Allow only if the portal is honestly outside every enabled ward.
            // Empty INSIDE / reflection errors must not bypass NoTeleport.
            if (!WilRadius.HonestlyInsideAnyEnabled(tw.transform.position))
                __result = true;
        }
    }
}
