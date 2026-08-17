using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace EpiTombFit
{
    /// <summary>
    /// Tomb prefab inventory is vanilla 8x4; height is not in the ZDO. EPI quickslots /
    /// equipment row (y&gt;=4) are compacted into y&lt;4 for persist, then restored
    /// (grid + EquipItem) on loot.
    /// </summary>
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInProcess("valheim.exe")]
    [BepInProcess("valheim_server.exe")]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid = "yanlo.EpiTombFit";
        public const string PluginName = "EPI Tomb Fit";
        public const string PluginVersion = "1.3.0";

        public const int VanillaPersistHeight = 4;

        internal const string MarkX = "yanlo.tombX";
        internal const string MarkY = "yanlo.tombY";
        internal const string MarkKind = "yanlo.tombKind";

        private readonly Harmony _harmony = new Harmony(PluginGuid);

        private void Awake()
        {
            _harmony.PatchAll(Assembly.GetExecutingAssembly());

            MethodInfo easyFit = AccessTools.Method(typeof(TombStone), "EasyFitInInventory");
            if (easyFit == null)
            {
                Logger.LogError("TombStone.EasyFitInInventory not found");
            }
            else
            {
                _harmony.Patch(
                    easyFit,
                    prefix: new HarmonyMethod(typeof(EasyFitGuard), nameof(EasyFitGuard.Prefix))
                    {
                        priority = Priority.First
                    },
                    postfix: new HarmonyMethod(typeof(EasyFitGuard), nameof(EasyFitGuard.Postfix))
                    {
                        priority = Priority.Last
                    });
            }

            Logger.LogInfo($"{PluginName} {PluginVersion} loaded ({Paths.ProcessName})");
        }
    }

    internal static class EpiApi
    {
        private static Type _api;
        private static MethodInfo _isEquipmentCell;
        private static MethodInfo _isQuickCell;
        private static MethodInfo _getEquipmentSnapshots;
        private static PropertyInfo _snapshotGridPos;

        internal static bool Available
        {
            get
            {
                EnsureInit();
                return _api != null && _isEquipmentCell != null && _isQuickCell != null;
            }
        }

        private static void EnsureInit()
        {
            if (_api != null)
            {
                return;
            }

            _api = AccessTools.TypeByName("AzuEPI.API");
            if (_api == null)
            {
                return;
            }

            _isEquipmentCell = AccessTools.Method(_api, "IsEquipmentCell", new[] { typeof(Inventory), typeof(int), typeof(int), typeof(int).MakeByRefType() });
            _isQuickCell = AccessTools.Method(_api, "IsQuickCell", new[] { typeof(Inventory), typeof(int), typeof(int), typeof(int).MakeByRefType() });
            _getEquipmentSnapshots = AccessTools.Method(_api, "GetEquipmentSlotSnapshots", new[] { typeof(Inventory) });

            Type snapType = AccessTools.TypeByName("AzuEPI.SlotSnapshot");
            if (snapType != null)
            {
                _snapshotGridPos = snapType.GetProperty("GridPos", BindingFlags.Instance | BindingFlags.Public);
            }
        }

        internal static bool IsEquipmentCell(Inventory inv, int x, int y)
        {
            if (!Available || inv == null)
            {
                return false;
            }

            object[] args = { inv, x, y, 0 };
            try
            {
                return (bool)_isEquipmentCell.Invoke(null, args);
            }
            catch
            {
                return false;
            }
        }

        internal static bool IsQuickCell(Inventory inv, int x, int y)
        {
            if (!Available || inv == null)
            {
                return false;
            }

            object[] args = { inv, x, y, 0 };
            try
            {
                return (bool)_isQuickCell.Invoke(null, args);
            }
            catch
            {
                return false;
            }
        }

        internal static string CellKind(Inventory inv, Vector2i pos)
        {
            if (IsEquipmentCell(inv, pos.x, pos.y))
            {
                return "equip";
            }

            if (IsQuickCell(inv, pos.x, pos.y))
            {
                return "quick";
            }

            return "bag";
        }

        internal static void ReapplyEquipment(Player player, Inventory inv)
        {
            if (player == null || inv == null)
            {
                return;
            }

            EnsureInit();
            if (_getEquipmentSnapshots == null || _snapshotGridPos == null)
            {
                return;
            }

            try
            {
                object snaps = _getEquipmentSnapshots.Invoke(null, new object[] { inv });
                IEnumerable enumerable = snaps as IEnumerable;
                if (enumerable == null)
                {
                    return;
                }

                foreach (object snap in enumerable)
                {
                    if (snap == null)
                    {
                        continue;
                    }

                    object gpObj = _snapshotGridPos.GetValue(snap, null);
                    if (!(gpObj is Vector2i))
                    {
                        continue;
                    }

                    Vector2i gp = (Vector2i)gpObj;
                    ItemDrop.ItemData item = inv.GetItemAt(gp.x, gp.y);
                    if (item != null)
                    {
                        player.EquipItem(item, true);
                    }
                }
            }
            catch
            {
                /* AzuEPI optional */
            }
        }
    }

    internal static class GraveGrid
    {
        internal static bool NeedsPersistCompact(Inventory inv, ItemDrop.ItemData item)
        {
            if (item == null)
            {
                return false;
            }

            Vector2i gp = item.m_gridPos;
            if (gp.y >= Plugin.VanillaPersistHeight)
            {
                return true;
            }

            if (EpiApi.Available)
            {
                return EpiApi.IsEquipmentCell(inv, gp.x, gp.y) || EpiApi.IsQuickCell(inv, gp.x, gp.y);
            }

            return false;
        }

        internal static Vector2i FindPersistSlot(Inventory inv, ItemDrop.ItemData skip)
        {
            if (inv == null)
            {
                return new Vector2i(-1, -1);
            }

            int w = inv.GetWidth();
            int h = Math.Min(Plugin.VanillaPersistHeight, inv.GetHeight());
            for (int y = h - 1; y >= 1; y--)
            {
                for (int x = 0; x < w; x++)
                {
                    ItemDrop.ItemData at = inv.GetItemAt(x, y);
                    if (at == null || at == skip)
                    {
                        return new Vector2i(x, y);
                    }
                }
            }

            for (int x = 0; x < w; x++)
            {
                ItemDrop.ItemData at = inv.GetItemAt(x, 0);
                if (at == null || at == skip)
                {
                    return new Vector2i(x, 0);
                }
            }

            return new Vector2i(-1, -1);
        }

        internal static void StampOrigin(Inventory inv, ItemDrop.ItemData item, Vector2i from)
        {
            if (item == null)
            {
                return;
            }

            if (item.m_customData == null)
            {
                item.m_customData = new Dictionary<string, string>();
            }

            if (!item.m_customData.ContainsKey(Plugin.MarkX))
            {
                item.m_customData[Plugin.MarkX] = from.x.ToString(CultureInfo.InvariantCulture);
                item.m_customData[Plugin.MarkY] = from.y.ToString(CultureInfo.InvariantCulture);
                item.m_customData[Plugin.MarkKind] = EpiApi.CellKind(inv, from);
            }
        }

        internal static bool TryReadOrigin(ItemDrop.ItemData item, out int x, out int y)
        {
            x = -1;
            y = -1;
            if (item == null || item.m_customData == null)
            {
                return false;
            }

            string sx, sy;
            if (!item.m_customData.TryGetValue(Plugin.MarkX, out sx) ||
                !item.m_customData.TryGetValue(Plugin.MarkY, out sy))
            {
                return false;
            }

            return int.TryParse(sx, NumberStyles.Integer, CultureInfo.InvariantCulture, out x) &&
                   int.TryParse(sy, NumberStyles.Integer, CultureInfo.InvariantCulture, out y);
        }

        internal static void ClearOrigin(ItemDrop.ItemData item)
        {
            if (item == null || item.m_customData == null)
            {
                return;
            }

            item.m_customData.Remove(Plugin.MarkX);
            item.m_customData.Remove(Plugin.MarkY);
            item.m_customData.Remove(Plugin.MarkKind);
        }

        internal static void RestoreOrigins(Inventory inv)
        {
            if (inv == null)
            {
                return;
            }

            bool changed = false;
            List<ItemDrop.ItemData> items = new List<ItemDrop.ItemData>(inv.GetAllItems());
            for (int i = 0; i < items.Count; i++)
            {
                ItemDrop.ItemData item = items[i];
                int ox, oy;
                if (!TryReadOrigin(item, out ox, out oy))
                {
                    continue;
                }

                if (ox < 0 || oy < 0 || ox >= inv.GetWidth() || oy >= inv.GetHeight())
                {
                    continue;
                }

                ItemDrop.ItemData at = inv.GetItemAt(ox, oy);
                if (at != null && at != item)
                {
                    continue;
                }

                item.m_gridPos = new Vector2i(ox, oy);
                ClearOrigin(item);
                changed = true;
            }

            if (changed)
            {
                MethodInfo changedMethod = AccessTools.Method(typeof(Inventory), "Changed");
                changedMethod?.Invoke(inv, null);
            }
        }

        internal static void RestoreAndReapply(Player player, Inventory inv)
        {
            RestoreOrigins(inv);
            EpiApi.ReapplyEquipment(player, inv);
        }

        internal static void CompactIntoVanilla(Inventory inv)
        {
            if (inv == null)
            {
                return;
            }

            List<ItemDrop.ItemData> items = new List<ItemDrop.ItemData>(inv.GetAllItems());
            for (int i = 0; i < items.Count; i++)
            {
                ItemDrop.ItemData item = items[i];
                if (item == null || !NeedsPersistCompact(inv, item))
                {
                    continue;
                }

                Vector2i from = item.m_gridPos;
                Vector2i slot = FindPersistSlot(inv, item);
                if (slot.x < 0)
                {
                    continue;
                }

                StampOrigin(inv, item, from);
                item.m_gridPos = slot;
            }
        }

        internal static bool IsTombInventory(Inventory inv)
        {
            if (inv == null)
            {
                return false;
            }

            TombStone[] tombs = UnityEngine.Object.FindObjectsOfType<TombStone>();
            if (tombs == null)
            {
                return false;
            }

            for (int i = 0; i < tombs.Length; i++)
            {
                Container box = tombs[i] != null ? tombs[i].GetComponent<Container>() : null;
                if (box != null && box.GetInventory() == inv)
                {
                    return true;
                }
            }

            return false;
        }
    }

    [HarmonyPatch(typeof(Inventory), "MoveInventoryToGrave")]
    internal static class MoveToGravePatch
    {
        private static void Prefix(Inventory original)
        {
            GraveGrid.CompactIntoVanilla(original);
        }
    }

    [HarmonyPatch(typeof(Inventory), "MoveAll")]
    internal static class MoveAllRestorePatch
    {
        private static void Postfix(Inventory __instance)
        {
            Player local = Player.m_localPlayer;
            if (local == null || local.GetInventory() != __instance)
            {
                return;
            }

            GraveGrid.RestoreAndReapply(local, __instance);
        }
    }

    [HarmonyPatch(typeof(TombStone), "OnTakeAllSuccess")]
    internal static class TombTakeAllRestorePatch
    {
        [HarmonyPriority(Priority.Last)]
        private static void Postfix()
        {
            Player local = Player.m_localPlayer;
            if (local == null)
            {
                return;
            }

            GraveGrid.RestoreAndReapply(local, local.GetInventory());
        }
    }

    [HarmonyPatch(typeof(Inventory), "MoveItemToThis", new Type[] { typeof(Inventory), typeof(ItemDrop.ItemData), typeof(int), typeof(int), typeof(int) })]
    internal static class MoveItemRestorePatch5
    {
        private static void Postfix(Inventory __instance)
        {
            Player local = Player.m_localPlayer;
            if (local == null || local.GetInventory() != __instance)
            {
                return;
            }

            GraveGrid.RestoreAndReapply(local, __instance);
        }
    }

    [HarmonyPatch(typeof(Inventory), "MoveItemToThis", new Type[] { typeof(Inventory), typeof(ItemDrop.ItemData) })]
    internal static class MoveItemRestorePatch2
    {
        private static void Postfix(Inventory __instance)
        {
            Player local = Player.m_localPlayer;
            if (local == null || local.GetInventory() != __instance)
            {
                return;
            }

            GraveGrid.RestoreAndReapply(local, __instance);
        }
    }

    [HarmonyPatch(typeof(Inventory), "AddItem", new Type[] { typeof(ItemDrop.ItemData), typeof(int), typeof(int), typeof(int) })]
    internal static class TombAddItemRelocatePatch
    {
        private static void Prefix(Inventory __instance, ItemDrop.ItemData item, int amount, ref int x, ref int y)
        {
            int w = __instance != null ? __instance.GetWidth() : -1;
            int h = __instance != null ? __instance.GetHeight() : -1;
            if (x >= 0 && y >= 0 && x < w && y < h)
            {
                return;
            }

            if (!GraveGrid.IsTombInventory(__instance))
            {
                return;
            }

            GraveGrid.StampOrigin(__instance, item, new Vector2i(x, y));
            Vector2i slot = GraveGrid.FindPersistSlot(__instance, item);
            if (slot.x >= 0)
            {
                x = slot.x;
                y = slot.y;
            }
        }
    }

    internal static class EasyFitGuard
    {
        public static bool Prefix(TombStone __instance, Player player, ref bool __result)
        {
            if (!ShouldBlock(__instance, player))
            {
                return true;
            }

            __result = false;
            return false;
        }

        public static void Postfix(TombStone __instance, Player player, ref bool __result)
        {
            if (ShouldBlock(__instance, player))
            {
                __result = false;
            }
        }

        private static bool ShouldBlock(TombStone tomb, Player player)
        {
            if (tomb == null || player == null)
            {
                return false;
            }

            Inventory pInv = player.GetInventory();
            Container box = tomb.GetComponent<Container>();
            Inventory tInv = box != null ? box.GetInventory() : null;
            if (pInv == null || tInv == null)
            {
                return false;
            }

            int pH = pInv.GetHeight();
            int tH = tInv.GetHeight();
            int maxY = -1;
            foreach (ItemDrop.ItemData item in tInv.GetAllItems())
            {
                if (item == null)
                {
                    continue;
                }

                if (item.m_gridPos.y > maxY)
                {
                    maxY = item.m_gridPos.y;
                }
            }

            return pH < tH || maxY >= pH;
        }
    }
}
