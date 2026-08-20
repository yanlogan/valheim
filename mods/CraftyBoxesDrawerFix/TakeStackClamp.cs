using System;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace CraftyBoxesDrawerFix
{
    /// <summary>
    /// ItemDrawers Take Stack uses <c>_item.m_shared.m_maxStackSize</c>.
    /// MUC OnInventoryChanged clones the 1-slot shadow item (max=MaxItems 9999)
    /// after V+ autodeposit into the drawer. E then Drop(9999) → one vanilla
    /// stack in inventory, rest Destroy'd.
    /// </summary>
    internal static class TakeStackClamp
    {
        private static FieldInfo NviewField;
        private static FieldInfo QtyField;
        private static FieldInfo ItemField;

        internal static bool Apply(Harmony harmony, Type drawerType, FieldInfo qty, FieldInfo item)
        {
            if (drawerType == null || qty == null || item == null)
            {
                return false;
            }

            QtyField = qty;
            ItemField = item;
            NviewField = AccessTools.Field(drawerType, "_nview");
            MethodInfo target = AccessTools.Method(drawerType, "ProcessInputInternal");
            if (target == null)
            {
                return false;
            }

            harmony.Patch(
                target,
                prefix: new HarmonyMethod(
                    AccessTools.Method(typeof(TakeStackClamp), nameof(ProcessInputPrefix))));
            return true;
        }

        private static bool ProcessInputPrefix(object __instance, bool mod0, bool mod1, ref bool __result)
        {
            if (mod0)
            {
                return true;
            }

            int qty = QtyField.GetValue(__instance) is int n ? n : 0;
            if (qty <= 0)
            {
                return true;
            }

            ItemDrop.ItemData item = ItemField.GetValue(__instance) as ItemDrop.ItemData;
            int amount = mod1 ? 1 : PrefabMax(item);
            if (amount < 1)
            {
                amount = 1;
            }

            ZNetView nview = NviewField?.GetValue(__instance) as ZNetView;
            if (nview == null || !nview.IsValid())
            {
                return true;
            }

            nview.InvokeRPC("Drop", amount);
            __result = true;
            return false;
        }

        private static int PrefabMax(ItemDrop.ItemData item)
        {
            if (item?.m_dropPrefab == null)
            {
                return -1;
            }

            ItemDrop drop = item.m_dropPrefab.GetComponent<ItemDrop>();
            return drop?.m_itemData?.m_shared != null
                ? drop.m_itemData.m_shared.m_maxStackSize
                : -1;
        }
    }
}
