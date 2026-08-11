using System.Collections;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace QSSSortButtonOffset
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency("goldenrevolver.quick_stack_store", BepInDependency.DependencyFlags.SoftDependency)]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid = "yanlo.QSSSortButtonOffset";
        public const string PluginName = "QSS Sort Button Offset";
        public const string PluginVersion = "1.0.0";

        internal static Plugin Instance;

        internal static ConfigEntry<bool> Enabled;
        internal static ConfigEntry<float> OffsetX;
        internal static ConfigEntry<float> OffsetY;
        internal static ConfigEntry<bool> DebugLog;

        private static int _lastOffsetInstanceId;

        private void Awake()
        {
            Instance = this;

            Enabled = Config.Bind("General", "Enabled", true,
                "Move QSS chest Sort button after QSS places it.");
            OffsetX = Config.Bind("Position", "OffsetX", 0f,
                "Added to Sort button localPosition.x after QSS layout (Unity UI units).");
            OffsetY = Config.Bind("Position", "OffsetY", -42f,
                "Added to Sort button localPosition.y after QSS layout. Negative = down (default clears Place Stacks / SC Unload).");
            DebugLog = Config.Bind("General", "DebugLog", false,
                "Log when the Sort button is moved.");

            OffsetX.SettingChanged += (_, __) => _lastOffsetInstanceId = 0;
            OffsetY.SettingChanged += (_, __) => _lastOffsetInstanceId = 0;
            Enabled.SettingChanged += (_, __) => _lastOffsetInstanceId = 0;

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), PluginGuid);
            Logger.LogInfo($"{PluginName} {PluginVersion} loaded (OffsetX={OffsetX.Value}, OffsetY={OffsetY.Value})");
        }

        internal static void ScheduleApply(InventoryGui gui)
        {
            if (!Enabled.Value || Instance == null || gui == null)
            {
                return;
            }

            Instance.StartCoroutine(ApplyNextFrame(gui));
        }

        private static IEnumerator ApplyNextFrame(InventoryGui gui)
        {
            // Let QSS finish Show postfixes / any same-frame layout.
            yield return null;
            ApplyOffset(gui);
        }

        internal static void ApplyOffset(InventoryGui gui)
        {
            if (!Enabled.Value || gui == null)
            {
                return;
            }

            Transform sort = FindNamedChild(gui.transform, "sortContainerButton");
            if (sort == null || !sort.gameObject.activeInHierarchy)
            {
                return;
            }

            int id = sort.GetInstanceID();
            if (id == _lastOffsetInstanceId)
            {
                return;
            }

            Vector3 before = sort.localPosition;
            sort.localPosition = before + new Vector3(OffsetX.Value, OffsetY.Value, 0f);
            _lastOffsetInstanceId = id;

            if (DebugLog.Value)
            {
                Debug.Log($"[QSSSortButtonOffset] {before} -> {sort.localPosition}");
            }
        }

        private static Transform FindNamedChild(Transform root, string name)
        {
            if (root == null)
            {
                return null;
            }

            if (root.name == name)
            {
                return root;
            }

            for (int i = 0; i < root.childCount; i++)
            {
                Transform found = FindNamedChild(root.GetChild(i), name);
                if (found != null)
                {
                    return found;
                }
            }

            return null;
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
