using System;
using System.Collections;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace CraftyBoxesDrawerFix
{
    /// <summary>
    /// AAA multi-craft queueNextCraft auto-clicks Craft after each item.
    /// DoCrafting → UpdateCraftingPanel can jump selection to index 0 when the
    /// queued recipe drops out of the (re)sorted list (GetSelectedRecipeIndex→0).
    /// Same path hits Recycle reclaim. Keep selection on the queued recipe, or
    /// cancel the remaining AAA amount if it is gone / not craftable.
    /// </summary>
    internal static class AaaCraftQueueFix
    {
        private static Recipe _queuedRecipe;
        private static bool _active;

        internal static void Apply(Harmony harmony)
        {
            harmony.Patch(
                AccessTools.Method(typeof(InventoryGui), "OnCraftPressed"),
                postfix: new HarmonyMethod(typeof(AaaCraftQueueFix), nameof(OnCraftPressedPost)));
            harmony.Patch(
                AccessTools.Method(typeof(InventoryGui), "UpdateCraftingPanel"),
                postfix: new HarmonyMethod(typeof(AaaCraftQueueFix), nameof(UpdateCraftingPanelPost)));
            harmony.Patch(
                AccessTools.Method(typeof(InventoryGui), "OnCraftCancelPressed"),
                postfix: new HarmonyMethod(typeof(AaaCraftQueueFix), nameof(ClearQueue)));
            harmony.Patch(
                AccessTools.Method(typeof(InventoryGui), "Hide"),
                postfix: new HarmonyMethod(typeof(AaaCraftQueueFix), nameof(ClearQueue)));
        }

        private static void OnCraftPressedPost(InventoryGui __instance)
        {
            if (!Plugin.Enabled.Value || !Plugin.FixAaaCraftQueue.Value || __instance == null)
            {
                return;
            }

            Recipe selected = GetSelectedRecipe(__instance);
            Recipe craft = GetCraftRecipe(__instance);
            Recipe use = craft != null ? craft : selected;
            int amount = ReadAaaAmount();

            if (use == null || amount <= 1)
            {
                if (amount <= 1)
                {
                    ClearQueue();
                }

                return;
            }

            _queuedRecipe = use;
            _active = true;
        }

        private static void UpdateCraftingPanelPost(InventoryGui __instance)
        {
            if (!Plugin.Enabled.Value || !Plugin.FixAaaCraftQueue.Value || !_active || _queuedRecipe == null
                || __instance == null)
            {
                return;
            }

            int amount = ReadAaaAmount();
            float timer = GetCraftTimer(__instance);
            if (amount <= 0 && timer < 0f)
            {
                ClearQueue();
                return;
            }

            Recipe selected = GetSelectedRecipe(__instance);
            if (SameRecipe(selected, _queuedRecipe))
            {
                return;
            }

            int idx = FindRecipeIndex(__instance, _queuedRecipe);
            Player player = Player.m_localPlayer;
            bool canDo = idx >= 0 && player != null
                && (player.NoCostCheat()
                    || (ZoneSystem.instance != null
                        && ZoneSystem.instance.GetGlobalKey(GlobalKeys.NoCraftCost))
                    || player.HaveRequirements(_queuedRecipe, false, 1, 1));

            if (canDo)
            {
                Traverse.Create(__instance).Method("SetRecipe", new object[] { idx, false }).GetValue();
                return;
            }

            WriteAaaAmount(0);
            WriteCurrentCraftAmount(1);
            ClearQueue();
        }

        private static void ClearQueue()
        {
            _queuedRecipe = null;
            _active = false;
        }

        private static int FindRecipeIndex(InventoryGui gui, Recipe recipe)
        {
            object listObj = Traverse.Create(gui).Field("m_availableRecipes").GetValue();
            if (!(listObj is IList list) || recipe == null)
            {
                return -1;
            }

            for (int i = 0; i < list.Count; i++)
            {
                object pair = list[i];
                Recipe r = Traverse.Create(pair).Property("Recipe").GetValue<Recipe>()
                           ?? Traverse.Create(pair).Field("Recipe").GetValue<Recipe>();
                if (SameRecipe(r, recipe))
                {
                    return i;
                }
            }

            return -1;
        }

        private static bool SameRecipe(Recipe a, Recipe b)
        {
            if (a == b)
            {
                return true;
            }

            if (a == null || b == null || a.m_item == null || b.m_item == null)
            {
                return false;
            }

            string an = a.m_item.m_itemData?.m_shared?.m_name;
            string bn = b.m_item.m_itemData?.m_shared?.m_name;
            return !string.IsNullOrEmpty(an) && an == bn;
        }

        private static float GetCraftTimer(InventoryGui gui)
        {
            return Traverse.Create(gui).Field("m_craftTimer").GetValue<float>();
        }

        private static Recipe GetCraftRecipe(InventoryGui gui)
        {
            return Traverse.Create(gui).Field("m_craftRecipe").GetValue<Recipe>();
        }

        private static Recipe GetSelectedRecipe(InventoryGui gui)
        {
            object pair = Traverse.Create(gui).Field("m_selectedRecipe").GetValue();
            if (pair == null)
            {
                return null;
            }

            return Traverse.Create(pair).Property("Recipe").GetValue<Recipe>()
                   ?? Traverse.Create(pair).Field("Recipe").GetValue<Recipe>();
        }

        private static int ReadAaaAmount()
        {
            try
            {
                FieldInfo amountField = AccessTools.Field(
                    AccessTools.TypeByName("AzuAntiArthriticCrafting.Patches.AAACraft"),
                    "amount");
                if (amountField != null)
                {
                    return (int)amountField.GetValue(null);
                }
            }
            catch
            {
                // ignored
            }

            return -1;
        }

        private static void WriteAaaAmount(int value)
        {
            try
            {
                FieldInfo amountField = AccessTools.Field(
                    AccessTools.TypeByName("AzuAntiArthriticCrafting.Patches.AAACraft"),
                    "amount");
                amountField?.SetValue(null, value);

                object input = AccessTools.Field(
                    AccessTools.TypeByName("AzuAntiArthriticCrafting.Patches.AAACraft"),
                    "inputAmount")?.GetValue(null);
                if (input != null)
                {
                    MethodInfo setText = AccessTools.Method(input.GetType(), "SetTextWithoutNotify");
                    setText?.Invoke(input, new object[] { value.ToString() });
                }
            }
            catch
            {
                // ignored
            }
        }

        private static void WriteCurrentCraftAmount(int value)
        {
            try
            {
                Type t = AccessTools.TypeByName(
                    "AzuAntiArthriticCrafting.InventoryGuiShowCraftingPanelPatch");
                FieldInfo f = AccessTools.Field(t, "CurrentCraftAmount");
                f?.SetValue(null, value);
            }
            catch
            {
                // ignored
            }
        }
    }
}
