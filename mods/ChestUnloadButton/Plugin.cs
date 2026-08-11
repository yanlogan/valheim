using System.Collections;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChestUnloadButton
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency("flueno.SmartContainers", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("goldenrevolver.quick_stack_store", BepInDependency.DependencyFlags.SoftDependency)]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid = "yanlo.ChestUnloadButton";
        public const string PluginName = "Chest Unload Button";
        public const string PluginVersion = "1.3.0";

        internal static Plugin Instance;

        internal static ConfigEntry<bool> Enabled;
        internal static ConfigEntry<bool> HidePlaceStacks;
        internal static ConfigEntry<string> UnloadLabel;
        internal static ConfigEntry<string> Placement;
        internal static ConfigEntry<float> Gap;
        internal static ConfigEntry<float> ExtraOffsetX;
        internal static ConfigEntry<float> ExtraOffsetY;
        internal static ConfigEntry<bool> MatchTakeAllSize;
        internal static ConfigEntry<bool> DumpLeftoversToOpenChest;
        internal static ConfigEntry<bool> DebugLog;

        private void Awake()
        {
            Instance = this;

            Enabled = Config.Bind("General", "Enabled", true,
                "Restyle SmarterContainers Unload button and optionally hide Place Stacks.");
            HidePlaceStacks = Config.Bind("General", "HidePlaceStacks", true,
                "Force-hide vanilla Place Stacks button.");
            UnloadLabel = Config.Bind("UnloadButton", "Label", "Unload",
                "Text on the Unload button.");
            Placement = Config.Bind("UnloadButton", "Placement", "Right",
                "Where to put Unload relative to Take All: Right | Below");
            Gap = Config.Bind("UnloadButton", "Gap", 6f,
                "Pixels between Take All and Unload.");
            ExtraOffsetX = Config.Bind("UnloadButton", "ExtraOffsetX", 0f,
                "Extra nudge after placement.");
            ExtraOffsetY = Config.Bind("UnloadButton", "ExtraOffsetY", 0f,
                "Extra nudge after placement.");
            MatchTakeAllSize = Config.Bind("UnloadButton", "MatchTakeAllSize", true,
                "Copy Take All width/height onto Unload.");
            DumpLeftoversToOpenChest = Config.Bind("Unload", "DumpLeftoversToOpenChest", true,
                "After SC Unload to nearby relevant chests, dump remaining eligible items into the open chest (not Store-All: same SC filters).");
            DebugLog = Config.Bind("General", "DebugLog", false, "Log restyle / leftover dump.");

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), PluginGuid);
            Logger.LogInfo($"{PluginName} {PluginVersion} loaded");
        }

        internal static void ScheduleApply(InventoryGui gui)
        {
            if (!Enabled.Value || Instance == null || gui == null)
            {
                return;
            }

            Instance.StartCoroutine(ApplyAfterLayout(gui));
        }

        private static IEnumerator ApplyAfterLayout(InventoryGui gui)
        {
            // SC / QSS may finish layout a frame or two later.
            yield return null;
            Apply(gui);
            yield return null;
            Apply(gui);
        }

        internal static void Apply(InventoryGui gui)
        {
            if (!Enabled.Value || gui == null)
            {
                return;
            }

            if (HidePlaceStacks.Value)
            {
                HideStackAll(gui);
            }

            Button takeAllBtn = GetPrivateButton(gui, "m_takeAllButton");
            if (takeAllBtn == null)
            {
                return;
            }

            RectTransform takeAll = takeAllBtn.transform as RectTransform;
            if (takeAll == null)
            {
                return;
            }

            Transform unloadTf = FindUnloadButton(gui.transform);
            if (unloadTf == null || !unloadTf.gameObject.activeInHierarchy)
            {
                return;
            }

            RectTransform unload = unloadTf as RectTransform;
            if (unload == null)
            {
                return;
            }

            // Same coordinate space as Take All (SC used a different parent + btnPos).
            if (unload.parent != takeAll.parent)
            {
                unload.SetParent(takeAll.parent, false);
            }

            unload.anchorMin = takeAll.anchorMin;
            unload.anchorMax = takeAll.anchorMax;
            unload.pivot = takeAll.pivot;
            unload.localScale = takeAll.localScale;
            unload.localRotation = takeAll.localRotation;

            if (MatchTakeAllSize.Value)
            {
                unload.sizeDelta = takeAll.sizeDelta;
            }

            float gap = Gap.Value;
            Vector2 size = takeAll.sizeDelta;
            Vector3 pos = takeAll.localPosition;
            string place = (Placement.Value ?? "Right").Trim().ToLowerInvariant();

            if (place == "below")
            {
                // Under Take All (still left column)
                pos += new Vector3(0f, -(size.y + gap), 0f);
            }
            else
            {
                // Default: same row, immediately right of Take All (left of Sort)
                pos += new Vector3(size.x + gap, 0f, 0f);
            }

            pos += new Vector3(ExtraOffsetX.Value, ExtraOffsetY.Value, 0f);
            unload.localPosition = pos;

            SetButtonLabel(unload, UnloadLabel.Value);
            MatchLabelStyle(unload, takeAll);

            if (DebugLog.Value)
            {
                Debug.Log($"[ChestUnloadButton] parent={unload.parent.name} pos={pos} size={unload.sizeDelta} place={place}");
            }
        }

        private static void HideStackAll(InventoryGui gui)
        {
            Button stackAll = GetPrivateButton(gui, "m_stackAllButton");
            if (stackAll != null && stackAll.gameObject.activeSelf)
            {
                stackAll.gameObject.SetActive(false);
            }
        }

        private static Button GetPrivateButton(InventoryGui gui, string fieldName)
        {
            FieldInfo fi = AccessTools.Field(typeof(InventoryGui), fieldName);
            return fi?.GetValue(gui) as Button;
        }

        private static Transform FindUnloadButton(Transform root)
        {
            Transform byGlyph = FindByLabel(root, IsUnloadGlyph);
            if (byGlyph != null)
            {
                return byGlyph;
            }

            return FindByLabel(root, t =>
                t == UnloadLabel.Value || t == "Unload" || t == "UNLOAD");
        }

        private static bool IsUnloadGlyph(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            string t = text.Trim();
            if (t == @"\||/" || t == "||/" || t == @"\|/" || t == "|/|")
            {
                return true;
            }

            return t.IndexOf("||") >= 0 && t.Length <= 8;
        }

        private static Transform FindByLabel(Transform root, System.Func<string, bool> pred)
        {
            foreach (TMP_Text tmp in root.GetComponentsInChildren<TMP_Text>(true))
            {
                if (pred(tmp.text))
                {
                    return FindButtonRoot(tmp.transform);
                }
            }

            foreach (Text ui in root.GetComponentsInChildren<Text>(true))
            {
                if (pred(ui.text))
                {
                    return FindButtonRoot(ui.transform);
                }
            }

            return null;
        }

        private static Transform FindButtonRoot(Transform from)
        {
            Transform t = from;
            while (t != null)
            {
                if (t.GetComponent<Button>() != null)
                {
                    return t;
                }

                t = t.parent;
            }

            return from;
        }

        private static void SetButtonLabel(Transform buttonRoot, string label)
        {
            TMP_Text tmp = buttonRoot.GetComponentInChildren<TMP_Text>(true);
            if (tmp != null)
            {
                tmp.text = label;
                return;
            }

            Text ui = buttonRoot.GetComponentInChildren<Text>(true);
            if (ui != null)
            {
                ui.text = label;
            }
        }

        private static void MatchLabelStyle(Transform unload, Transform takeAll)
        {
            TMP_Text src = takeAll.GetComponentInChildren<TMP_Text>(true);
            TMP_Text dst = unload.GetComponentInChildren<TMP_Text>(true);
            if (src != null && dst != null)
            {
                dst.fontSize = src.fontSize;
                dst.fontStyle = src.fontStyle;
                dst.alignment = src.alignment;
                dst.color = src.color;
                return;
            }

            Text srcUi = takeAll.GetComponentInChildren<Text>(true);
            Text dstUi = unload.GetComponentInChildren<Text>(true);
            if (srcUi != null && dstUi != null)
            {
                dstUi.fontSize = srcUi.fontSize;
                dstUi.fontStyle = srcUi.fontStyle;
                dstUi.alignment = srcUi.alignment;
                dstUi.color = srcUi.color;
            }
        }
    }

    [HarmonyPatch(typeof(InventoryGui), nameof(InventoryGui.Show))]
    internal static class InventoryGuiShowPatch
    {
        [HarmonyPostfix]
        [HarmonyPriority(Priority.Last)]
        private static void Postfix(InventoryGui __instance)
        {
            Plugin.ScheduleApply(__instance);
        }
    }
}
