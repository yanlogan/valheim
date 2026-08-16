using System;
using System.Collections;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace PortalWardFix
{
    /// <summary>
    /// Fixes WardIsLove TeleportWorldTeleportPatch silent deny when CheckInWardMonoscript
    /// is true (often due to IsInside radius clobber) but WardMonoscriptsINSIDE is empty.
    /// </summary>
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency("Azumatt.WardIsLove", BepInDependency.DependencyFlags.HardDependency)]
    [BepInProcess("valheim.exe")]
    [BepInProcess("valheim_server.exe")]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid = "yanlo.PortalWardFix";
        public const string PluginName = "Portal Ward Fix";
        public const string PluginVersion = "1.0.1";

        private readonly Harmony _harmony = new Harmony(PluginGuid);
        private bool _hooked;

        private void Awake()
        {
            ArmHook("Awake");
        }

        private void Start()
        {
            if (!_hooked) ArmHook("Start");
        }

        private void ArmHook(string phase)
        {
            if (_hooked) return;
            if (!TryHookWilTeleportPrefix())
            {
                Logger.LogWarning($"[{PluginName}] WardIsLove teleport patch not found ({phase})");
                return;
            }

            _hooked = true;
            Logger.LogInfo($"[{PluginName}] hooked WiL TeleportWorldTeleportPatch ({phase})");
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

            _harmony.Patch(prefix,
                postfix: new HarmonyMethod(typeof(WilTeleportFix), nameof(WilTeleportFix.Postfix))
                {
                    priority = Priority.Last
                });
            return true;
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

            int inside = CountInside();

            // WiL bug: CheckIn true + empty INSIDE → default false (silent deny).
            if (inside <= 0)
            {
                __result = true;
                return;
            }

            bool noTeleport = false;
            try
            {
                if (tw != null)
                {
                    Type wardExt = AccessTools.TypeByName("WardIsLove.Extensions.WardMonoscriptExt");
                    var getWard = wardExt != null
                        ? AccessTools.Method(wardExt, "GetWardMonoscript", new[] { typeof(Vector3) })
                        : null;
                    object ward = getWard != null
                        ? getWard.Invoke(null, new object[] { tw.transform.position })
                        : null;
                    if (ward != null && !ward.Equals(null) && wardExt != null)
                    {
                        MethodInfo getNt = null;
                        foreach (var m in wardExt.GetMethods(BindingFlags.Public | BindingFlags.Static))
                        {
                            if (m.Name == "GetNoTeleportOn") { getNt = m; break; }
                        }
                        if (getNt != null)
                        {
                            object r = getNt.GetParameters().Length == 1
                                ? getNt.Invoke(null, new[] { ward })
                                : getNt.Invoke(ward, null);
                            noTeleport = r is bool b && b;
                        }
                    }
                }
            }
            catch { /* */ }

            if (!noTeleport)
                __result = true;
        }

        private static int CountInside()
        {
            try
            {
                Type wardExt = AccessTools.TypeByName("WardIsLove.Extensions.WardMonoscriptExt");
                if (wardExt == null) return -1;
                var prop = AccessTools.Property(wardExt, "WardMonoscriptsINSIDE");
                object list = prop != null
                    ? prop.GetValue(null, null)
                    : AccessTools.Field(wardExt, "WardMonoscriptsINSIDE")?.GetValue(null);
                if (list == null) return 0;
                if (list is ICollection c) return c.Count;
                int n = 0;
                foreach (var _ in (IEnumerable)list) n++;
                return n;
            }
            catch
            {
                return -2;
            }
        }
    }
}
