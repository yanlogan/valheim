using System;
using System.Collections.Generic;
using System.Reflection;
using AzuCraftyBoxes;
using AzuCraftyBoxes.APIs;
using AzuCraftyBoxes.IContainers;
using AzuCraftyBoxes.Util.Functions;
using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace CraftyBoxesDrawerFix
{
    /// <summary>
    /// CraftyBoxes 1.8.15 drawer inject + AAA Max fix.
    /// 1.1.7: draining a drawer to 0 keeps the item type (no Clear / Alt+E wipe).
    /// 1.1.6: also inject while hovering Smelter/etc (mill, spinning wheel, kiln) —
    /// those are not CraftingStation, so 1.1.4 skipped drawers (chests still worked).
    /// 1.1.5: pin AAA multi-craft/reclaim queue to the started recipe (no jump to next).
    /// 1.1.4: skip drawer inject outside craft station / build mode (inventory/chest FPS).
    /// 1.1.3: one AggregatedMkzContainer (dict ItemCount) instead of N wrappers;
    /// AAA GetAvailableItems only when at a crafting station.
    /// </summary>
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency("Azumatt.AzuCraftyBoxes", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("Azumatt.AzuAntiArthriticCrafting", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("mkz.itemdrawers", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInProcess("valheim.exe")]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid = "yanlo.CraftyBoxesDrawerFix";
        public const string PluginName = "CraftyBoxes Drawer Fix";
        public const string PluginVersion = "1.1.7";

        internal const string AaaGuid = "Azumatt.AzuAntiArthriticCrafting";
        private const float MkzInjectInterval = 0.5f;

        internal static Plugin Instance;
        internal static ConfigEntry<bool> Enabled;
        internal static ConfigEntry<bool> FixAaaMaxCraft;
        internal static ConfigEntry<bool> FixAaaCraftQueue;
        internal static ConfigEntry<bool> DebugLog;

        private static readonly FieldInfo EmptyListField =
            AccessTools.Field(typeof(Boxes), "_empty");

        private static readonly FieldInfo CachedAllField =
            AccessTools.Field(typeof(Boxes), "_cachedAll");

        private static readonly FieldInfo CraftyRangeField =
            AccessTools.Field(typeof(AzuCraftyBoxesPlugin), "mRange");

        private static readonly FieldInfo CraftyLeaveOneField =
            AccessTools.Field(typeof(AzuCraftyBoxesPlugin), "leaveOne");

        private static readonly MethodInfo ConsumeSilentlyMethod =
            AccessTools.Method(
                typeof(MkzItemDrawers_API.mkzDrawer),
                "ConsumeSilently",
                new[] { typeof(int) });

        private static readonly FieldInfo MkzDrawerComponentField =
            AccessTools.Field(typeof(MkzItemDrawers_API.mkzDrawer), "_drawer");

        private static Type DrawerContainerType;
        private static FieldInfo DrawerQuantityField;
        private static FieldInfo DrawerItemField;
        private static MethodInfo DrawerUpdateInventory;
        private static MethodInfo DrawerOnContainerChanged;
        private static MethodInfo DrawerUpdateVisuals;

        private static readonly FieldInfo PlayerHoveringField =
            AccessTools.Field(typeof(Player), "m_hovering");

        private static MethodInfo _getNearbyOpen;

        private static float _lastMkzInjectTime = -999f;
        private static int _countFrame = -1;
        private static List<IContainer> _countContainers;
        private static int _itemCacheFrame = -1;
        private static readonly Dictionary<string, int> _itemCountCache =
            new Dictionary<string, int>(64);

        private readonly Harmony _harmony = new Harmony(PluginGuid);

        /// <summary>
        /// One IContainer for all nearby mkz drawers — ItemCount is a dict hit.
        /// </summary>
        internal sealed class AggregatedMkzContainer : IContainer
        {
            private readonly List<(MkzItemDrawers_API.mkzDrawer drawer, string sharedName)> _drawers =
                new List<(MkzItemDrawers_API.mkzDrawer, string)>(64);

            private readonly Dictionary<string, int> _totals =
                new Dictionary<string, int>(64);

            public static AggregatedMkzContainer Build(
                IList<MkzItemDrawers_API.mkzDrawer> drawers)
            {
                AggregatedMkzContainer agg = new AggregatedMkzContainer();
                for (int i = 0; i < drawers.Count; i++)
                {
                    MkzItemDrawers_API.mkzDrawer drawer = drawers[i];
                    string shared = ResolveSharedName(drawer);
                    if (string.IsNullOrEmpty(shared))
                    {
                        continue;
                    }

                    agg._drawers.Add((drawer, shared));
                    if (agg._totals.TryGetValue(shared, out int have))
                    {
                        agg._totals[shared] = have + drawer.Amount;
                    }
                    else
                    {
                        agg._totals[shared] = drawer.Amount;
                    }
                }

                return agg;
            }

            private static string ResolveSharedName(MkzItemDrawers_API.mkzDrawer drawer)
            {
                string prefab = drawer.Prefab;
                if (string.IsNullOrEmpty(prefab) || ObjectDB.instance == null)
                {
                    return string.Empty;
                }

                GameObject itemPrefab = ObjectDB.instance.GetItemPrefab(prefab);
                if (itemPrefab == null)
                {
                    return string.Empty;
                }

                ItemDrop drop = itemPrefab.GetComponent<ItemDrop>();
                return drop?.m_itemData?.m_shared?.m_name ?? string.Empty;
            }

            private void RebuildTotals()
            {
                _totals.Clear();
                for (int i = 0; i < _drawers.Count; i++)
                {
                    (MkzItemDrawers_API.mkzDrawer drawer, string shared) = _drawers[i];
                    if (string.IsNullOrEmpty(shared))
                    {
                        continue;
                    }

                    if (_totals.TryGetValue(shared, out int have))
                    {
                        _totals[shared] = have + drawer.Amount;
                    }
                    else
                    {
                        _totals[shared] = drawer.Amount;
                    }
                }
            }

            public int ItemCount(string name)
            {
                return _totals.TryGetValue(name, out int n) ? n : 0;
            }

            /// <summary>
            /// How many drawers contribute to leaveOne accounting for this shared name.
            /// </summary>
            public int CountDrawersWith(string name)
            {
                int n = 0;
                for (int i = 0; i < _drawers.Count; i++)
                {
                    (MkzItemDrawers_API.mkzDrawer drawer, string shared) = _drawers[i];
                    if (string.Equals(shared, name, StringComparison.Ordinal) && drawer.Amount > 0)
                    {
                        n++;
                    }
                }

                return n;
            }

            public int ProcessContainerInventory(
                string reqName,
                int totalAmount,
                int totalRequirement)
            {
                if (string.IsNullOrEmpty(reqName) || totalAmount >= totalRequirement)
                {
                    return totalAmount;
                }

                int remaining = totalRequirement - totalAmount;
                for (int i = 0; i < _drawers.Count && remaining > 0; i++)
                {
                    (MkzItemDrawers_API.mkzDrawer drawer, string shared) = _drawers[i];
                    if (!string.Equals(shared, reqName, StringComparison.Ordinal))
                    {
                        continue;
                    }

                    int take = Mathf.Min(drawer.Amount, remaining);
                    if (take <= 0)
                    {
                        continue;
                    }

                    ConsumeDrawer(drawer, take);
                    totalAmount += take;
                    remaining -= take;
                }

                RebuildTotals();
                return totalAmount;
            }

            public void RemoveItem(string name, int amount)
            {
                if (amount <= 0 || string.IsNullOrEmpty(name))
                {
                    return;
                }

                int remaining = amount;
                for (int i = 0; i < _drawers.Count && remaining > 0; i++)
                {
                    (MkzItemDrawers_API.mkzDrawer drawer, string shared) = _drawers[i];
                    if (!string.Equals(shared, name, StringComparison.Ordinal))
                    {
                        continue;
                    }

                    int take = Mathf.Min(drawer.Amount, remaining);
                    if (take <= 0)
                    {
                        continue;
                    }

                    ConsumeDrawer(drawer, take);
                    remaining -= take;
                }

                RebuildTotals();
            }

            public void Save()
            {
            }

            public Vector3 GetPosition()
            {
                if (_drawers.Count > 0)
                {
                    return _drawers[0].drawer.Position;
                }

                return Player.m_localPlayer != null
                    ? Player.m_localPlayer.transform.position
                    : Vector3.zero;
            }

            public string GetPrefabName()
            {
                return "YanloAggregatedMkz";
            }

            public Inventory GetInventory()
            {
                return null;
            }
        }

        private static void ConsumeDrawer(MkzItemDrawers_API.mkzDrawer drawer, int amount)
        {
            ConsumeDrawerKeepType(drawer, amount);
        }

        /// <summary>
        /// Decrement qty without DrawerContainer.Clear(). Qty 0 keeps the locked item
        /// (Save writes prefab + 0). Stock ConsumeSilently called Clear at 0 = Alt+E.
        /// </summary>
        internal static void ConsumeDrawerKeepType(MkzItemDrawers_API.mkzDrawer drawer, int amount)
        {
            if (amount <= 0 || drawer == null)
            {
                return;
            }

            ZNetView nview = drawer.m_nview;
            if (nview == null || !nview.IsValid())
            {
                return;
            }

            int current = drawer.Amount;
            if (current <= 0)
            {
                return;
            }

            nview.ClaimOwnership();
            int newQty = Math.Max(0, current - amount);

            Component comp = MkzDrawerComponentField?.GetValue(drawer) as Component;
            if (comp == null || DrawerQuantityField == null)
            {
                Instance?.Logger.LogWarning(
                    "Drawer consume: DrawerContainer fields missing — skip (would Clear type).");
                return;
            }

            DrawerQuantityField.SetValue(comp, newQty);
            if (DrawerItemField?.GetValue(comp) != null && DrawerUpdateInventory != null)
            {
                DrawerUpdateInventory.Invoke(comp, null);
            }

            DrawerOnContainerChanged?.Invoke(comp, null);
            DrawerUpdateVisuals?.Invoke(comp, null);
        }

        private static bool ConsumeSilentlyKeepTypePrefix(
            MkzItemDrawers_API.mkzDrawer __instance,
            int amount)
        {
            ConsumeDrawerKeepType(__instance, amount);
            return false;
        }

        private void Awake()
        {
            Instance = this;
            Enabled = Config.Bind(
                "General",
                "Enabled",
                true,
                "Inject Makail ItemDrawers into AzuCraftyBoxes nearby container list.");
            FixAaaMaxCraft = Config.Bind(
                "General",
                "FixAaaMaxCraft",
                true,
                "Fix AAA Max: craft count (stale AcbExtra + never-zero Clamp). Soft-dep AAA.");
            FixAaaCraftQueue = Config.Bind(
                "General",
                "FixAaaCraftQueue",
                true,
                "Keep AAA multi-craft/reclaim queue on the started recipe (stop jump to next list item). Soft-dep AAA.");
            DebugLog = Config.Bind(
                "General",
                "DebugLog",
                false,
                "Log drawer inject / AAA max fixes.");

            _getNearbyOpen = AccessTools.FirstMethod(
                typeof(Boxes),
                info => info.Name == "GetNearbyContainers" && info.IsGenericMethodDefinition);

            PatchCraftyBoxesNearby();
            InitDrawerAccess();
            PatchConsumeSilentlyKeepType();

            if (Chainloader.PluginInfos.ContainsKey(AaaGuid))
            {
                if (FixAaaMaxCraft.Value)
                {
                    PatchAaaMaxCraft();
                }

                if (FixAaaCraftQueue.Value)
                {
                    AaaCraftQueueFix.Apply(_harmony);
                }
            }
            else if (FixAaaMaxCraft.Value || FixAaaCraftQueue.Value)
            {
                Logger.LogInfo("AAA not loaded - skip AAA craft patches.");
            }

            Logger.LogInfo($"{PluginName} {PluginVersion} loaded");
        }

        private void OnDestroy()
        {
            _harmony.UnpatchSelf();
        }

        private static float GetCraftyBoxesRange()
        {
            try
            {
                if (CraftyRangeField?.GetValue(null) is ConfigEntry<float> val)
                {
                    return val.Value;
                }
            }
            catch
            {
                // ignored
            }

            return 50f;
        }

        private static bool IsCraftyBoxesLeaveOne()
        {
            try
            {
                object obj = CraftyLeaveOneField?.GetValue(null);
                if (obj == null)
                {
                    return false;
                }

                object value = Traverse.Create(obj).Property("Value").GetValue();
                if (value == null)
                {
                    return false;
                }

                return Traverse.Create(value).Method("isOn").GetValue<bool>();
            }
            catch
            {
                return false;
            }
        }

        private static bool IsAtCraftingStation()
        {
            Player player = Player.m_localPlayer;
            return player != null && player.GetCurrentCraftingStation() != null;
        }

        /// <summary>
        /// Drawers only matter for craft-from-containers / build HUD / station fill.
        /// Inventory or chest alone: skip FindObjects + inject (was still hitching FPS).
        /// Mill / spinning wheel / kiln are Smelter, not CraftingStation — still inject on hover.
        /// </summary>
        private static bool NeedsDrawerInject()
        {
            Player player = Player.m_localPlayer;
            if (player == null)
            {
                return false;
            }

            if (player.GetCurrentCraftingStation() != null)
            {
                return true;
            }

            if (player.InPlaceMode())
            {
                return true;
            }

            return HoverIsCraftyBoxesFillTarget(player);
        }

        private static bool HoverIsCraftyBoxesFillTarget(Player player)
        {
            GameObject hover = PlayerHoveringField?.GetValue(player) as GameObject;
            if (hover == null)
            {
                return false;
            }

            Transform t = hover.transform;
            return t.GetComponentInParent<Smelter>() != null
                || t.GetComponentInParent<Fermenter>() != null
                || t.GetComponentInParent<Fireplace>() != null
                || t.GetComponentInParent<CookingStation>() != null
                || t.GetComponentInParent<Turret>() != null
                || t.GetComponentInParent<ShieldGenerator>() != null;
        }

        private static void InitDrawerAccess()
        {
            DrawerContainerType = Type.GetType("DrawerContainer, itemdrawers");
            if (DrawerContainerType == null)
            {
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                for (int i = 0; i < assemblies.Length; i++)
                {
                    if (assemblies[i].GetName().Name == "itemdrawers")
                    {
                        DrawerContainerType = assemblies[i].GetType("DrawerContainer");
                        break;
                    }
                }
            }

            if (DrawerContainerType == null)
            {
                Instance.Logger.LogWarning("DrawerContainer type not found — empty-drawer type keep disabled.");
                return;
            }

            DrawerQuantityField = AccessTools.Field(DrawerContainerType, "_quantity");
            DrawerItemField = AccessTools.Field(DrawerContainerType, "_item");
            DrawerUpdateInventory = AccessTools.Method(DrawerContainerType, "UpdateInventory");
            DrawerOnContainerChanged = AccessTools.Method(DrawerContainerType, "OnContainerChanged");
            DrawerUpdateVisuals = AccessTools.Method(DrawerContainerType, "UpdateVisuals");
        }

        private void PatchConsumeSilentlyKeepType()
        {
            if (ConsumeSilentlyMethod == null)
            {
                Logger.LogWarning("mkzDrawer.ConsumeSilently not found — cannot keep empty drawer type.");
                return;
            }

            _harmony.Patch(
                ConsumeSilentlyMethod,
                prefix: new HarmonyMethod(
                    AccessTools.Method(typeof(Plugin), nameof(ConsumeSilentlyKeepTypePrefix))));
            Logger.LogInfo("ConsumeSilently: keep item type at qty 0 (no Clear).");
        }

        private void PatchCraftyBoxesNearby()
        {
            if (_getNearbyOpen == null)
            {
                Logger.LogError("Boxes.GetNearbyContainers not found - CraftyBoxes API changed?");
                return;
            }

            HarmonyMethod postfix = new HarmonyMethod(
                AccessTools.Method(typeof(Plugin), nameof(GetNearbyContainersPostfix)));

            Type[] sources = { typeof(Player), typeof(Component), typeof(MonoBehaviour) };
            int patched = 0;
            for (int i = 0; i < sources.Length; i++)
            {
                try
                {
                    _harmony.Patch(
                        _getNearbyOpen.MakeGenericMethod(sources[i]),
                        postfix: postfix);
                    patched++;
                }
                catch (Exception ex)
                {
                    Logger.LogWarning($"Skip generic<{sources[i].Name}>: {ex.Message}");
                }
            }

            Logger.LogInfo($"CraftyBoxes nearby: patched {patched} GetNearbyContainers closed generics");
        }

        private void PatchAaaMaxCraft()
        {
            Type utilities = AccessTools.TypeByName("AzuAntiArthriticCrafting.Utilities.Utilities");
            Type aaaCraft = AccessTools.TypeByName("AzuAntiArthriticCrafting.Patches.AAACraft");
            MethodInfo getAvail = AccessTools.Method(utilities, "GetAvailableItems", new[] { typeof(string) });
            MethodInfo calcMax = AccessTools.Method(aaaCraft, "CalculateMaxCraftAmount");

            if (getAvail == null || calcMax == null)
            {
                Logger.LogError(
                    $"AAA methods missing (Utilities={utilities != null}, AAACraft={aaaCraft != null}, getAvail={getAvail != null}, calcMax={calcMax != null})");
                return;
            }

            _harmony.Patch(
                getAvail,
                postfix: new HarmonyMethod(
                    AccessTools.Method(typeof(Plugin), nameof(GetAvailableItemsPostfix))));
            _harmony.Patch(
                calcMax,
                postfix: new HarmonyMethod(
                    AccessTools.Method(typeof(Plugin), nameof(CalculateMaxCraftAmountPostfix))));

            Logger.LogInfo("AAA Max craft patches applied");
        }

        private static bool HasOurMkzWrapper(List<IContainer> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] is AggregatedMkzContainer)
                {
                    return true;
                }
            }

            return false;
        }

        private static List<IContainer> GetContainersForCounting(Player player)
        {
            int frameCount = Time.frameCount;
            if (_countFrame == frameCount && _countContainers != null)
            {
                return _countContainers;
            }

            float range = GetCraftyBoxesRange();
            if (CachedAllField?.GetValue(null) is List<IContainer> cached && cached.Count > 0)
            {
                EnsureMkzInjected(cached, player.transform.position, range);
                _countContainers = cached;
            }
            else
            {
                _countContainers = InvokeGetNearby(player, range) ?? new List<IContainer>();
            }

            _countFrame = frameCount;
            return _countContainers;
        }

        private static List<IContainer> InvokeGetNearby(Player player, float range)
        {
            if (_getNearbyOpen == null || player == null)
            {
                return new List<IContainer>();
            }

            return _getNearbyOpen.MakeGenericMethod(typeof(Player))
                       .Invoke(null, new object[] { player, range }) as List<IContainer>
                   ?? new List<IContainer>();
        }

        internal static int CountAvailableWithCraftyBoxes(string sharedName)
        {
            Player localPlayer = Player.m_localPlayer;
            if (localPlayer == null || string.IsNullOrEmpty(sharedName))
            {
                return 0;
            }

            if (localPlayer.NoCostCheat())
            {
                return 999;
            }

            int frameCount = Time.frameCount;
            if (_itemCacheFrame != frameCount)
            {
                _itemCacheFrame = frameCount;
                _itemCountCache.Clear();
            }
            else if (_itemCountCache.TryGetValue(sharedName, out int cached))
            {
                return cached;
            }

            List<IContainer> containers = GetContainersForCounting(localPlayer);
            int total = localPlayer.GetInventory().CountItems(sharedName, -1, true);
            int leaveOneBuckets = 0;

            for (int i = 0; i < containers.Count; i++)
            {
                IContainer c = containers[i];
                if (c == null)
                {
                    continue;
                }

                if (c is AggregatedMkzContainer agg)
                {
                    int n = agg.ItemCount(sharedName);
                    if (n > 0)
                    {
                        total += n;
                        leaveOneBuckets += agg.CountDrawersWith(sharedName);
                    }

                    continue;
                }

                int count = c.ItemCount(sharedName);
                if (count > 0)
                {
                    leaveOneBuckets++;
                    total += count;
                }
            }

            if (IsCraftyBoxesLeaveOne() && leaveOneBuckets > 0)
            {
                total = Mathf.Max(0, total - leaveOneBuckets);
            }

            _itemCountCache[sharedName] = total;
            return total;
        }

        private static void GetAvailableItemsPostfix(string itemName, ref int __result)
        {
            if (!Enabled.Value || !FixAaaMaxCraft.Value || string.IsNullOrEmpty(itemName))
            {
                return;
            }

            // Inventory / chest only: skip expensive recount (Max UI needs station).
            if (!IsAtCraftingStation())
            {
                return;
            }

            __result = CountAvailableWithCraftyBoxes(itemName);
        }

        private static void CalculateMaxCraftAmountPostfix(ref int __result)
        {
            if (!Enabled.Value || !FixAaaMaxCraft.Value)
            {
                return;
            }

            InventoryGui gui = InventoryGui.instance;
            Player localPlayer = Player.m_localPlayer;
            if (gui == null || localPlayer == null)
            {
                return;
            }

            object selected = Traverse.Create(gui).Field("m_selectedRecipe").GetValue();
            Recipe recipe = Traverse.Create(selected).Property("Recipe").GetValue<Recipe>()
                            ?? Traverse.Create(selected).Field("Recipe").GetValue<Recipe>();
            if (recipe == null)
            {
                __result = 0;
                return;
            }

            object itemData = Traverse.Create(selected).Property("ItemData").GetValue()
                              ?? Traverse.Create(selected).Field("ItemData").GetValue();
            if (itemData != null)
            {
                __result = 1;
                return;
            }

            if (localPlayer.NoCostCheat()
                || (ZoneSystem.instance != null
                    && ZoneSystem.instance.GetGlobalKey(GlobalKeys.NoCraftCost)))
            {
                __result = 100;
                return;
            }

            int maxCraftable;
            if (recipe.m_requireOnlyOneIngredient)
            {
                maxCraftable = 0;
                Piece.Requirement[] resources = recipe.m_resources;
                for (int i = 0; i < resources.Length; i++)
                {
                    Piece.Requirement req = resources[i];
                    if (req?.m_resItem == null || req.m_amount < 1)
                    {
                        continue;
                    }

                    string name = req.m_resItem.m_itemData?.m_shared?.m_name;
                    if (string.IsNullOrEmpty(name) || !localPlayer.IsKnownMaterial(name))
                    {
                        continue;
                    }

                    maxCraftable += CountAvailableWithCraftyBoxes(name) / req.m_amount;
                    if (maxCraftable >= 100)
                    {
                        maxCraftable = 100;
                        break;
                    }
                }
            }
            else
            {
                int limited = int.MaxValue;
                bool any = false;
                Piece.Requirement[] resources = recipe.m_resources;
                for (int i = 0; i < resources.Length; i++)
                {
                    Piece.Requirement req = resources[i];
                    if (req?.m_resItem == null || req.m_amount < 1)
                    {
                        continue;
                    }

                    any = true;
                    string name = req.m_resItem.m_itemData?.m_shared?.m_name;
                    if (string.IsNullOrEmpty(name))
                    {
                        limited = 0;
                        break;
                    }

                    int can = CountAvailableWithCraftyBoxes(name) / req.m_amount;
                    if (can < limited)
                    {
                        limited = can;
                    }

                    if (limited == 0)
                    {
                        break;
                    }
                }

                maxCraftable = any ? limited : 0;
            }

            __result = Mathf.Clamp(maxCraftable, 0, 100);
        }

        private static void EnsureMkzInjected(List<IContainer> list, Vector3 pos, float rangeMeters)
        {
            if (list == null)
            {
                return;
            }

            // Inventory / chest without station or hammer: no drawer scan.
            if (!NeedsDrawerInject())
            {
                if (HasOurMkzWrapper(list))
                {
                    StripExistingMkzWrappers(list);
                }

                return;
            }

            bool haveOurs = HasOurMkzWrapper(list);
            if (haveOurs && Time.time - _lastMkzInjectTime <= MkzInjectInterval)
            {
                return;
            }

            StripExistingMkzWrappers(list);
            StripVanillaDrawerWrappers(list);

            List<MkzItemDrawers_API.mkzDrawer> drawers =
                MkzItemDrawers_API.AllDrawersInRange(pos, rangeMeters);
            if (drawers != null && drawers.Count > 0)
            {
                list.Add(AggregatedMkzContainer.Build(drawers));
                if (DebugLog.Value)
                {
                    Instance.Logger.LogInfo(
                        $"[Yanlo] Aggregated {drawers.Count} ItemDrawers (_cachedAll={list.Count}).");
                }
            }

            _lastMkzInjectTime = Time.time;
        }

        private static void GetNearbyContainersPostfix(
            List<IContainer> __result,
            object src,
            float rangeMeters)
        {
            if (!Enabled.Value || __result == null || src == null)
            {
                return;
            }

            if (EmptyListField != null && ReferenceEquals(__result, EmptyListField.GetValue(null)))
            {
                return;
            }

            Component component = src as Component;
            if (component == null)
            {
                return;
            }

            EnsureMkzInjected(__result, component.transform.position, rangeMeters);
        }

        private static void StripExistingMkzWrappers(List<IContainer> list)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                IContainer c = list[i];
                if (c is AggregatedMkzContainer || c is AzuCraftyBoxes.IContainers.mkzDrawer)
                {
                    list.RemoveAt(i);
                }
            }
        }

        private static void StripVanillaDrawerWrappers(List<IContainer> list)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (!(list[i] is VanillaContainer vanilla))
                {
                    continue;
                }

                Container wrapped = GetWrappedContainer(vanilla);
                if (wrapped != null && wrapped.GetType().Name == "DrawerContainer")
                {
                    list.RemoveAt(i);
                }
            }
        }

        private static Container GetWrappedContainer(VanillaContainer vanilla)
        {
            Traverse t = Traverse.Create(vanilla);
            Container c = t.Field("<_container>P").GetValue<Container>();
            if (c != null)
            {
                return c;
            }

            return t.Field("_container").GetValue<Container>();
        }
    }
}
