using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace EpiDeathDiag
{
    /// <summary>
    /// Temporary death/tombstone probe for AzuEPI quickslot loss (session 5fec17).
    /// </summary>
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInProcess("valheim.exe")]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid = "yanlo.EpiDeathDiag";
        public const string PluginName = "EPI Death Diag";
        public const string PluginVersion = "0.1.0";

        internal static Plugin Instance;
        private readonly Harmony _harmony = new Harmony(PluginGuid);

        // #region agent log
        internal static readonly string[] LogPaths =
        {
            @"c:\Program Files (x86)\Steam\steamapps\common\Valheim dedicated server\debug-5fec17.log",
            Path.Combine(Paths.BepInExRootPath ?? ".", "debug-5fec17.log"),
        };

        private const string IngestUrl = "http://127.0.0.1:7832/ingest/edff3ce3-d0be-4d0d-b4b5-408ba0a57bac";
        private const string SessionId = "5fec17";
        // #endregion

        private void Awake()
        {
            Instance = this;
            try
            {
                _harmony.PatchAll(Assembly.GetExecutingAssembly());
                Dlog("boot", "H0", "EpiDeathDiag awake", new
                {
                    process = Paths.ProcessName,
                    version = PluginVersion,
                    epiApi = AccessTools.TypeByName("AzuEPI.API") != null
                });
                Logger.LogInfo($"[{PluginName}] armed → debug-5fec17.log");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                Dlog("boot", "H0", "EpiDeathDiag awake FAILED", new { error = ex.ToString() });
            }
        }

        // #region agent log
        internal static void Dlog(string location, string hypothesisId, string message, object data)
        {
            try
            {
                long ts = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                string dataJson = data == null ? "null" : SimpleJson(data);
                string line =
                    "{\"sessionId\":\"" + SessionId + "\",\"runId\":\"pre-fix\",\"hypothesisId\":\"" + Escape(hypothesisId) +
                    "\",\"location\":\"" + Escape(location) +
                    "\",\"message\":\"" + Escape(message) +
                    "\",\"data\":" + dataJson +
                    ",\"timestamp\":" + ts +
                    ",\"process\":\"" + Escape(Paths.ProcessName ?? "") + "\"}\n";
                foreach (string path in LogPaths)
                {
                    try { File.AppendAllText(path, line); }
                    catch { /* ignore */ }
                }

                try
                {
                    using (var wc = new WebClient())
                    {
                        wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                        wc.Headers["X-Debug-Session-Id"] = SessionId;
                        wc.UploadString(IngestUrl, "POST", line.TrimEnd());
                    }
                }
                catch { /* ingest optional */ }
            }
            catch { /* never throw from diag */ }
        }

        private static string Escape(string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            return s.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "");
        }

        private static string SimpleJson(object data)
        {
            if (data == null) return "null";
            if (data is string str) return "\"" + Escape(str) + "\"";
            if (data is IEnumerable enumerable && !(data is string))
            {
                var items = new List<string>();
                foreach (object o in enumerable)
                    items.Add(SimpleJson(o));
                return "[" + string.Join(",", items.ToArray()) + "]";
            }

            var props = data.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var fields = data.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
            var parts = new List<string>();
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
                return Convert.ToString(v, CultureInfo.InvariantCulture);
            if (v is IEnumerable && !(v is string))
                return SimpleJson(v);
            return "\"" + Escape(Convert.ToString(v)) + "\"";
        }
        // #endregion

        internal static object SnapshotInventory(Player player, string phase)
        {
            var all = new List<object>();
            var quick = new List<object>();
            int w = 0, h = 0, count = 0;
            string epiErr = null;
            try
            {
                if (player == null) return new { phase, error = "no player" };
                Inventory inv = player.GetInventory();
                if (inv == null) return new { phase, error = "no inv" };
                w = inv.GetWidth();
                h = inv.GetHeight();
                count = inv.CountItems("", -1, true);
                foreach (ItemDrop.ItemData item in inv.GetAllItems())
                {
                    if (item == null) continue;
                    Vector2i gp = item.m_gridPos;
                    bool isQuick = false;
                    try
                    {
                        Type api = AccessTools.TypeByName("AzuEPI.API");
                        MethodInfo isQuickCell = api?.GetMethod("IsQuickCell", BindingFlags.Public | BindingFlags.Static);
                        if (isQuickCell != null)
                        {
                            object[] args = { inv, gp.x, gp.y, 0 };
                            isQuick = (bool)isQuickCell.Invoke(null, args);
                        }
                    }
                    catch (Exception ex) { epiErr = ex.GetType().Name + ": " + ex.Message; }

                    var row = new
                    {
                        name = item.m_shared != null ? item.m_shared.m_name : "?",
                        prefab = item.m_dropPrefab != null ? item.m_dropPrefab.name : "?",
                        x = gp.x,
                        y = gp.y,
                        stack = item.m_stack,
                        quality = item.m_quality,
                        equipped = item.m_equipped,
                        isQuick
                    };
                    all.Add(row);
                    if (isQuick) quick.Add(row);
                }

                // Also try GetQuickSlotsItems()
                try
                {
                    Type api = AccessTools.TypeByName("AzuEPI.API");
                    MethodInfo getQs = api?.GetMethod("GetQuickSlotsItems", BindingFlags.Public | BindingFlags.Static);
                    if (getQs != null)
                    {
                        var list = getQs.Invoke(null, null) as IEnumerable;
                        if (list != null)
                        {
                            quick.Clear();
                            foreach (object o in list)
                            {
                                var item = o as ItemDrop.ItemData;
                                if (item == null) continue;
                                quick.Add(new
                                {
                                    name = item.m_shared != null ? item.m_shared.m_name : "?",
                                    prefab = item.m_dropPrefab != null ? item.m_dropPrefab.name : "?",
                                    x = item.m_gridPos.x,
                                    y = item.m_gridPos.y,
                                    stack = item.m_stack,
                                    via = "GetQuickSlotsItems"
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    epiErr = (epiErr ?? "") + " GetQuickSlotsItems:" + ex.Message;
                }
            }
            catch (Exception ex)
            {
                return new { phase, error = ex.ToString() };
            }

            return new
            {
                phase,
                width = w,
                height = h,
                itemCount = count,
                quickCount = quick.Count,
                quick,
                allCount = all.Count,
                all,
                epiErr
            };
        }
    }

    [HarmonyPatch(typeof(Player), "CreateTombStone")]
    internal static class CreateTombStonePatch
    {
        private static void Prefix(Player __instance)
        {
            // H-A Sort scramble / H-C EPI tombstone / H-E other: state BEFORE tombstone fills
            Plugin.Dlog("Player.CreateTombStone:Prefix", "A", "inventory BEFORE CreateTombStone",
                Plugin.SnapshotInventory(__instance, "before_tomb"));
        }

        private static void Postfix(Player __instance)
        {
            Plugin.Dlog("Player.CreateTombStone:Postfix", "C", "inventory AFTER CreateTombStone",
                Plugin.SnapshotInventory(__instance, "after_tomb"));
        }
    }

    [HarmonyPatch(typeof(TombStone), "Interact")]
    internal static class TombStoneInteractPatch
    {
        private static void Prefix(TombStone __instance, Humanoid character)
        {
            var player = character as Player;
            Plugin.Dlog("TombStone.Interact:Prefix", "D", "before tomb interact", new
            {
                playerSnap = Plugin.SnapshotInventory(player, "before_interact"),
                tombOwner = __instance != null ? __instance.GetOwnerName() : "?"
            });
        }

        private static void Postfix(TombStone __instance, Humanoid character)
        {
            var player = character as Player;
            Plugin.Dlog("TombStone.Interact:Postfix", "B", "after tomb interact (merge/reclaim)",
                Plugin.SnapshotInventory(player, "after_interact"));
        }
    }

    [HarmonyPatch(typeof(Inventory), "MoveAll")]
    internal static class InventoryMoveAllPatch
    {
        private static void Prefix(Inventory __instance, Inventory fromInventory)
        {
            if (fromInventory == null || __instance == null) return;
            // Only care when moving INTO local player inv (reclaim / take all)
            Player local = Player.m_localPlayer;
            if (local == null || local.GetInventory() != __instance) return;

            Plugin.Dlog("Inventory.MoveAll:Prefix", "B", "MoveAll into player", new
            {
                fromCount = fromInventory.NrOfItems(),
                toBefore = Plugin.SnapshotInventory(local, "moveall_before")
            });
        }

        private static void Postfix(Inventory __instance, Inventory fromInventory)
        {
            Player local = Player.m_localPlayer;
            if (local == null || local.GetInventory() != __instance) return;
            Plugin.Dlog("Inventory.MoveAll:Postfix", "D", "MoveAll into player done",
                Plugin.SnapshotInventory(local, "moveall_after"));
        }
    }
}
