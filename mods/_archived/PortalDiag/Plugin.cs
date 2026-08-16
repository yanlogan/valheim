using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace PortalDiag
{
    /// <summary>
    /// Temporary debug probe for portal outbound failures (session c6aa2d).
    /// Writes NDJSON to dedicated-server workspace debug-c6aa2d.log (and BepInEx fallback).
    /// </summary>
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInProcess("valheim.exe")]
    [BepInProcess("valheim_server.exe")]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid = "yanlo.PortalDiag";
        public const string PluginName = "Portal Diag";
        public const string PluginVersion = "0.1.0";

        internal static Plugin Instance;
        private readonly Harmony _harmony = new Harmony(PluginGuid);

        // #region agent log
        internal static readonly string[] LogPaths =
        {
            @"c:\Program Files (x86)\Steam\steamapps\common\Valheim dedicated server\debug-c6aa2d.log",
            Path.Combine(Paths.BepInExRootPath, "debug-c6aa2d.log"),
        };
        // #endregion

        private void Awake()
        {
            Instance = this;
            try
            {
                _harmony.PatchAll(Assembly.GetExecutingAssembly());
                TryPatchWardIsLoveTeleport();
                Dlog("boot", "H0", "PortalDiag awake", new
                {
                    process = Paths.ProcessName,
                    version = PluginVersion,
                    wilPatched = _wilPatched
                });
                Logger.LogInfo($"[{PluginName}] armed → debug-c6aa2d.log");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                Dlog("boot", "H0", "PortalDiag awake FAILED", new { error = ex.ToString() });
            }
        }

        private bool _wilPatched;

        private void TryPatchWardIsLoveTeleport()
        {
            // Soft-ref: WardIsLove.PatchClasses.TeleportWorldTeleportPatch Prefix
            Type t = AccessTools.TypeByName("WardIsLove.PatchClasses.TeleportWorldTeleportPatch")
                     ?? AccessTools.TypeByName("WardIsLove.Util.PatchClasses.TeleportWorldTeleportPatch");
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

            if (t == null)
            {
                Dlog("wil", "C", "WiL TeleportWorldTeleportPatch type not found", null);
                return;
            }

            MethodInfo prefix = AccessTools.Method(t, "Prefix");
            if (prefix == null)
            {
                Dlog("wil", "C", "WiL Prefix method not found", new { type = t.FullName });
                return;
            }

            _harmony.Patch(prefix,
                postfix: new HarmonyMethod(typeof(WilTeleportProbe), nameof(WilTeleportProbe.Postfix)));
            _wilPatched = true;
            Dlog("wil", "C", "WiL TeleportWorldTeleportPatch hooked", new { type = t.FullName, method = prefix.ToString() });
        }

        // #region agent log
        internal static void Dlog(string location, string hypothesisId, string message, object data)
        {
            try
            {
                long ts = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                string dataJson = data == null ? "null" : SimpleJson(data);
                string line =
                    "{\"sessionId\":\"c6aa2d\",\"runId\":\"pre-fix\",\"hypothesisId\":\"" + Escape(hypothesisId) +
                    "\",\"location\":\"" + Escape(location) +
                    "\",\"message\":\"" + Escape(message) +
                    "\",\"data\":" + dataJson +
                    ",\"timestamp\":" + ts +
                    ",\"process\":\"" + Escape(Paths.ProcessName ?? "") + "\"}\n";
                foreach (string path in LogPaths)
                {
                    try
                    {
                        File.AppendAllText(path, line);
                    }
                    catch
                    {
                        /* ignore per-path failures */
                    }
                }
            }
            catch
            {
                /* never throw from diag */
            }
        }

        private static string Escape(string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            return s.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "");
        }

        private static string SimpleJson(object data)
        {
            if (data == null) return "null";
            var props = data.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var fields = data.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
            var parts = new System.Collections.Generic.List<string>();
            foreach (var p in props)
            {
                object v = null;
                try { v = p.GetValue(data, null); } catch { /* */ }
                parts.Add("\"" + Escape(p.Name) + "\":" + JsonValue(v));
            }
            foreach (var f in fields)
            {
                object v = null;
                try { v = f.GetValue(data); } catch { /* */ }
                parts.Add("\"" + Escape(f.Name) + "\":" + JsonValue(v));
            }
            return "{" + string.Join(",", parts.ToArray()) + "}";
        }

        private static string JsonValue(object v)
        {
            if (v == null) return "null";
            if (v is bool b) return b ? "true" : "false";
            if (v is int || v is long || v is float || v is double || v is short || v is byte)
                return Convert.ToString(v, System.Globalization.CultureInfo.InvariantCulture);
            return "\"" + Escape(Convert.ToString(v)) + "\"";
        }
        // #endregion
    }

    internal static class PortalZdoInfo
    {
        internal static object Snapshot(TeleportWorld tw, Player player)
        {
            string tag = "?";
            string conn = "none";
            string xTarget = "none";
            string pos = "?";
            bool teleportable = false;
            bool inPa = false;
            try
            {
                if (tw != null)
                {
                    tag = tw.GetText() ?? "";
                    var nview = tw.GetComponent<ZNetView>();
                    if (nview != null && nview.IsValid())
                    {
                        ZDO zdo = nview.GetZDO();
                        if (zdo != null)
                        {
                            pos = zdo.GetPosition().ToString("F1");
                            ZDOID c = zdo.GetConnectionZDOID(ZDOExtraData.ConnectionType.Portal);
                            conn = c.ToString();
                            // XPortal Key_TargetId common string
                            ZDOID xt = zdo.GetZDOID("target_id");
                            if (xt.IsNone()) xt = zdo.GetZDOID("target");
                            xTarget = xt.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                tag = "err:" + ex.Message;
            }

            try
            {
                if (player != null)
                {
                    teleportable = player.IsTeleportable();
                    inPa = PrivateArea.InsideFactionArea(player.transform.position, Character.Faction.Players);
                }
            }
            catch { /* */ }

            return new
            {
                tag,
                connectionZdo = conn,
                xportalTargetField = xTarget,
                portalPos = pos,
                playerTeleportable = teleportable,
                playerInPrivateArea = inPa,
                playerName = player != null ? player.GetPlayerName() : ""
            };
        }
    }

    [HarmonyPatch(typeof(TeleportWorld), nameof(TeleportWorld.Teleport))]
    internal static class TeleportWorld_Teleport_Diag
    {
        private static void Prefix(TeleportWorld __instance, Player player)
        {
            // #region agent log
            Plugin.Dlog("TeleportWorld.Teleport:Prefix", "B,C,D,E", "Teleport() ENTER",
                PortalZdoInfo.Snapshot(__instance, player));
            // #endregion
        }

        private static void Postfix(TeleportWorld __instance, Player player)
        {
            // #region agent log
            Plugin.Dlog("TeleportWorld.Teleport:Postfix", "A,E", "Teleport() EXIT (method returned)",
                new
                {
                    playerName = player != null ? player.GetPlayerName() : "",
                    tag = __instance != null ? __instance.GetText() : ""
                });
            // #endregion
        }
    }

    [HarmonyPatch(typeof(Demister), "OnEnable")]
    internal static class Demister_OnEnable_Diag
    {
        private static void Prefix(Demister __instance)
        {
            // #region agent log
            bool parentNull = true;
            string root = "?";
            string parent = "null";
            try
            {
                var go = __instance != null ? __instance.gameObject : null;
                if (go != null)
                {
                    root = go.transform.root != null ? go.transform.root.name : "nullroot";
                    parentNull = go.transform.parent == null;
                    parent = parentNull ? "null" : go.transform.parent.name;
                }
            }
            catch (Exception ex)
            {
                root = "err:" + ex.Message;
            }

            Plugin.Dlog("Demister.OnEnable:Prefix", "A", "Demister OnEnable",
                new { root, parent, parentNull, wouldNreOnParentName = parentNull });
            // #endregion
        }

        private static Exception Finalizer(Exception __exception)
        {
            // #region agent log
            if (__exception != null)
            {
                Plugin.Dlog("Demister.OnEnable:Finalizer", "A", "Demister OnEnable threw",
                    new { type = __exception.GetType().Name, message = __exception.Message });
            }
            // #endregion
            return __exception;
        }
    }

    /// <summary>Postfix on WiL's own Prefix — runs after WiL decides; __result is whether original Teleport runs.</summary>
    internal static class WilTeleportProbe
    {
        public static void Postfix(ref bool __result, object[] __args)
        {
            // #region agent log
            string player = "";
            TeleportWorld tw = null;
            try
            {
                if (__args != null)
                {
                    foreach (var a in __args)
                    {
                        if (a is Player p) player = p.GetPlayerName();
                        if (a is TeleportWorld t) tw = t;
                    }
                }
            }
            catch { /* */ }

            string tag = "";
            Vector3 portalPos = Vector3.zero;
            try
            {
                if (tw != null)
                {
                    tag = tw.GetText() ?? "";
                    portalPos = tw.transform.position;
                }
            }
            catch { /* */ }

            bool checkIn = false;
            int insideCount = -1;
            int areaCount = -1;
            string wardsDump = "";
            try
            {
                Type wardMono = AccessTools.TypeByName("WardIsLove.Util.WardMonoscript");
                Type wardExt = AccessTools.TypeByName("WardIsLove.Extensions.WardMonoscriptExt");
                if (wardMono != null)
                {
                    var check = AccessTools.Method(wardMono, "CheckInWardMonoscript",
                                    new[] { typeof(Vector3), typeof(bool) })
                                ?? AccessTools.Method(wardMono, "CheckInWardMonoscript", new[] { typeof(Vector3) });
                    if (check != null)
                    {
                        object r = check.GetParameters().Length == 2
                            ? check.Invoke(null, new object[] { portalPos, false })
                            : check.Invoke(null, new object[] { portalPos });
                        checkIn = r is bool b && b;
                    }

                    var allField = AccessTools.Field(wardMono, "m_allAreas");
                    object all = allField != null ? allField.GetValue(null) : null;
                    if (all is System.Collections.IEnumerable areas)
                    {
                        var parts = new System.Collections.Generic.List<string>();
                        int i = 0;
                        foreach (object area in areas)
                        {
                            if (area == null) continue;
                            areaCount = areaCount < 0 ? 1 : areaCount + 1;
                            try
                            {
                                var comp = area as Component;
                                Vector3 wpos = comp != null ? comp.transform.position : Vector3.zero;
                                float dist = Vector2.Distance(
                                    new Vector2(wpos.x, wpos.z),
                                    new Vector2(portalPos.x, portalPos.z));
                                // GetWardRadius is an extension on WardMonoscriptExt, not instance method
                                float radius = -1f;
                                if (wardExt != null)
                                {
                                    MethodInfo getRExt = null;
                                    foreach (var m in wardExt.GetMethods(BindingFlags.Public | BindingFlags.Static))
                                    {
                                        if (m.Name == "GetWardRadius") { getRExt = m; break; }
                                    }
                                    if (getRExt != null)
                                    {
                                        object rr = getRExt.Invoke(null, new[] { area });
                                        if (rr != null) radius = Convert.ToSingle(rr);
                                    }
                                }
                                var en = AccessTools.Method(area.GetType(), "IsEnabled");
                                bool enabled = en == null || (en.Invoke(area, null) is bool eb && eb);
                                bool noTp = false;
                                if (wardExt != null)
                                {
                                    MethodInfo getNt = null;
                                    foreach (var m in wardExt.GetMethods(BindingFlags.Public | BindingFlags.Static))
                                    {
                                        if (m.Name == "GetNoTeleportOn") { getNt = m; break; }
                                    }
                                    if (getNt != null)
                                    {
                                        object nr = getNt.Invoke(null, new[] { area });
                                        noTp = nr is bool nb && nb;
                                    }
                                }
                                var isIn = AccessTools.Method(area.GetType(), "IsInside",
                                    new[] { typeof(Vector3), typeof(float) });
                                bool wilIsInside = false;
                                if (isIn != null)
                                {
                                    object ir = isIn.Invoke(area, new object[] { portalPos, 0f });
                                    wilIsInside = ir is bool ib && ib;
                                }
                                bool honestInside = radius >= 0 && dist < radius;
                                float margin = radius >= 0 ? radius - dist : float.NaN;
                                parts.Add(string.Format(
                                    CultureInfo.InvariantCulture,
                                    "#{0} en={1} noTp={2} r={3:F1} distXZ={4:F1} margin={5:F1} honestIn={6} wilIsInside={7} pos=({8:F0},{9:F0},{10:F0})",
                                    i, enabled, noTp, radius, dist, margin, honestInside, wilIsInside,
                                    wpos.x, wpos.y, wpos.z));
                            }
                            catch (Exception ex)
                            {
                                parts.Add("#" + i + " err:" + ex.Message);
                            }
                            i++;
                        }
                        if (areaCount < 0) areaCount = 0;
                        else areaCount = i;
                        wardsDump = string.Join(" | ", parts.ToArray());
                    }
                }

                if (wardExt != null)
                {
                    var prop = AccessTools.Property(wardExt, "WardMonoscriptsINSIDE");
                    object list = prop != null
                        ? prop.GetValue(null, null)
                        : AccessTools.Field(wardExt, "WardMonoscriptsINSIDE")?.GetValue(null);
                    if (list == null) insideCount = 0;
                    else if (list is System.Collections.ICollection c) insideCount = c.Count;
                    else
                    {
                        insideCount = 0;
                        foreach (var _ in (System.Collections.IEnumerable)list) insideCount++;
                    }
                }
            }
            catch (Exception ex)
            {
                wardsDump = "dump-err:" + ex.Message;
            }

            Plugin.Dlog("WiL.TeleportWorldTeleportPatch:Prefix/Postfix", "C-verify",
                "WiL teleport gate + ward geometry",
                new
                {
                    allowOriginalTeleport = __result,
                    player,
                    portalTag = tag,
                    portalPos = portalPos.ToString("F1"),
                    checkInWardMonoscript = checkIn,
                    wardMonoscriptsInsideCount = insideCount,
                    allAreasCount = areaCount,
                    wards = wardsDump
                });
            // #endregion
        }
    }
}

