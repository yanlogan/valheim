using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using SmartContainers;
using UnityEngine;
using ItemData = ItemDrop.ItemData;

namespace ChestUnloadButton
{
    /// <summary>
    /// After SC Unload routes eligible items to nearby relevant chests,
    /// dump any still-eligible leftovers into the currently open chest
    /// (even if it has no matching stack/group).
    /// </summary>
    [HarmonyPatch(typeof(UnloadItems), nameof(UnloadItems.UnloadAllItems))]
    internal static class UnloadLeftoverDumpPatch
    {
        private static readonly FieldInfo ItemsListField =
            AccessTools.Field(typeof(UnloadItems), "unloadAllItemsList");

        private static readonly FieldInfo SkipListField =
            AccessTools.Field(typeof(UnloadItems), "unloadAllItemsSkipList");

        private static readonly FieldInfo PrefixGroupsField =
            AccessTools.Field(typeof(UnloadItems), "unloadAllPrefixItemGroups");

        private static readonly FieldInfo PostfixGroupsField =
            AccessTools.Field(typeof(UnloadItems), "unloadAllPostfixItemGroups");

        private static readonly FieldInfo CurrentContainerField =
            AccessTools.Field(typeof(InventoryGui), "m_currentContainer");

        private static readonly MethodInfo ContainerSaveMethod =
            AccessTools.Method(typeof(Container), "Save");

        private static readonly MethodInfo InventoryChangedMethod =
            AccessTools.Method(typeof(Inventory), "Changed");

        [HarmonyPostfix]
        private static void Postfix(Inventory playerInv)
        {
            if (!Plugin.DumpLeftoversToOpenChest.Value || playerInv == null)
            {
                return;
            }

            if (Mod.unloadAllEnabled == null || !Mod.unloadAllEnabled.Value)
            {
                return;
            }

            if (InventoryGui.instance == null || !InventoryGui.instance.IsContainerOpen())
            {
                return;
            }

            Container open = CurrentContainerField?.GetValue(InventoryGui.instance) as Container;
            if (open == null)
            {
                return;
            }

            Inventory dest = open.GetInventory();
            if (dest == null)
            {
                return;
            }

            List<ItemData> leftovers = playerInv.GetAllItems()
                .Where(IsStillUnloadEligible)
                .ToList();

            if (leftovers.Count == 0)
            {
                return;
            }

            int moved = 0;
            foreach (ItemData item in leftovers)
            {
                if (!playerInv.ContainsItem(item))
                {
                    continue;
                }

                if (!dest.CanAddItem(item, -1))
                {
                    continue;
                }

                if (!dest.AddItem(item))
                {
                    continue;
                }

                playerInv.RemoveItem(item);
                ContainerSaveMethod?.Invoke(open, null);
                InventoryChangedMethod?.Invoke(dest, null);
                moved++;
            }

            if (moved > 0)
            {
                InventoryChangedMethod?.Invoke(playerInv, null);
                if (Plugin.DebugLog.Value)
                {
                    Debug.Log($"[ChestUnloadButton] DumpLeftoversToOpenChest moved {moved} stack(s)");
                }
            }
        }

        private static bool IsStillUnloadEligible(ItemData item)
        {
            if (item == null || item.m_shared == null || item.m_equipped)
            {
                return false;
            }

            if (Mod.onlyStackableItems != null && Mod.onlyStackableItems.Value
                && item.m_shared.m_maxStackSize <= 1)
            {
                return false;
            }

            string scName = ToScName(item.m_shared.m_name);

            ICollection<string> skip = GetStringCollection(SkipListField);
            if (skip != null && skip.Contains(scName))
            {
                return false;
            }

            if (Mod.unloadAllMaterialsFiltering != null && Mod.unloadAllMaterialsFiltering.Value
                && item.m_shared.m_itemType == ItemData.ItemType.Material)
            {
                return true;
            }

            if (Mod.unloadAllTrophiesFiltering != null && Mod.unloadAllTrophiesFiltering.Value
                && item.m_shared.m_itemType == ItemData.ItemType.Trophy)
            {
                return true;
            }

            if (Mod.unloadAllConsumableFiltering != null && Mod.unloadAllConsumableFiltering.Value
                && item.m_shared.m_itemType == ItemData.ItemType.Consumable)
            {
                return true;
            }

            ICollection<string> itemsList = GetStringCollection(ItemsListField);
            if (itemsList != null && itemsList.Count > 0 && itemsList.Contains(scName))
            {
                return true;
            }

            ICollection<string> prefixes = GetStringCollection(PrefixGroupsField);
            if (prefixes != null && prefixes.Count > 0 && prefixes.Any(scName.StartsWith))
            {
                return true;
            }

            ICollection<string> postfixes = GetStringCollection(PostfixGroupsField);
            if (postfixes != null && postfixes.Count > 0 && postfixes.Any(scName.EndsWith))
            {
                return true;
            }

            ICollection<string> groups = UnloadItems.unloadAllGroupsList;
            if (groups != null && groups.Count > 0)
            {
                foreach (string groupId in groups)
                {
                    if (ItemGroups.Groups != null
                        && ItemGroups.Groups.TryGetValue(groupId, out ISet<string> members)
                        && members != null
                        && members.Contains(scName))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static ICollection<string> GetStringCollection(FieldInfo field)
        {
            return field?.GetValue(null) as ICollection<string>;
        }

        private static string ToScName(string itemName)
        {
            if (string.IsNullOrEmpty(itemName))
            {
                return string.Empty;
            }

            return itemName.ToLower().Replace("$item_", "").Replace("_", "");
        }
    }
}
