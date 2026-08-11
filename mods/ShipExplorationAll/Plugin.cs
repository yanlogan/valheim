using System.Collections.Generic;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace ShipExplorationAll
{
    /// <summary>
    /// Client-side map explore radius boost on ships.
    /// Vanilla + OdinShip 0.7.6 vessels + DefaultShipMultiplier fallback.
    /// Standalone — do not install GemHunter1.ShipExploration alongside.
    /// </summary>
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInProcess("valheim.exe")]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid = "yanlo.ShipExplorationAll";
        public const string PluginName = "Ship Exploration All";
        public const string PluginVersion = "1.1.0";

        private static Plugin Instance;
        private static readonly FieldInfo ExploreRadiusField =
            AccessTools.Field(typeof(Minimap), "m_exploreRadius");

        private readonly Harmony _harmony = new Harmony(PluginGuid);

        private static ConfigEntry<bool> Enabled;
        private static ConfigEntry<float> DefaultShipMultiplier;
        private static ConfigEntry<bool> DebugLog;

        // Longer substrings first (bigcargoship before cargoship, etc.).
        private static readonly List<KeyValuePair<string, ConfigEntry<float>>> ShipMatchers =
            new List<KeyValuePair<string, ConfigEntry<float>>>();

        private static bool _onBoat;
        private static bool _needApply;
        private static bool _applied;
        private static float _mult = 1f;
        private static float _originalRadius = 50f;
        private static float _newRadius = 50f;

        private void Awake()
        {
            Instance = this;

            Enabled = Config.Bind("General", "Enabled", true,
                "Boost minimap explore radius while on a ship.");
            DefaultShipMultiplier = Config.Bind("General", "DefaultShipMultiplier", 4f,
                "Multiplier for any Ship whose prefab name did not match a specific entry.");
            DebugLog = Config.Bind("General", "DebugLog", false, "Log enter/exit/apply.");

            // --- Vanilla ---
            AddShip("Vanilla", "RaftMultiplier", "raft", 3f);
            AddShip("Vanilla", "KarveMultiplier", "karve", 4f);
            ConfigEntry<float> longship = Config.Bind("Vanilla", "LongshipMultiplier", 7f,
                "Vanilla longship (GameObject name contains vikingship / longship / viking).");
            ShipMatchers.Add(new KeyValuePair<string, ConfigEntry<float>>("vikingship", longship));
            ShipMatchers.Add(new KeyValuePair<string, ConfigEntry<float>>("longship", longship));
            ShipMatchers.Add(new KeyValuePair<string, ConfigEntry<float>>("viking", longship));

            // --- OdinShip 0.7.6 (our installed pack) ---
            // Prefabs: BigCargoShip, CargoShip, MercantShip, WarShip,
            // LittleBoat, RowingCanoe, DoubleRowingCanoe
            AddShip("OdinShip", "BigCargoShipMul", "bigcargoship", 7f);
            AddShip("OdinShip", "CargoShipMul", "cargoship", 5f);
            AddShip("OdinShip", "MercantShipMul", "mercantship", 5f);
            AddShip("OdinShip", "WarShipMul", "warship", 7f);
            AddShip("OdinShip", "LittleBoatMul", "littleboat", 4f);
            AddShip("OdinShip", "DoubleRowingCanoeMul", "doublerowingcanoe", 3f);
            AddShip("OdinShip", "RowingCanoeMul", "rowingcanoe", 2.5f);

            // Optional OdinShipPlus-style names (harmless if prefab absent)
            AddShip("OdinShipPlus", "HugeCargoShipMul", "hugecargoship", 7f);
            AddShip("OdinShipPlus", "CargoAnimalShipMul", "cargoanimalship", 5f);
            AddShip("OdinShipPlus", "CargoCaravelMul", "cargocaravel", 5f);
            AddShip("OdinShipPlus", "TaurusWarShipMul", "tauruswarship", 7f);
            AddShip("OdinShipPlus", "FastShipSkuldelevMul", "fastshipskuldelev", 6f);
            AddShip("OdinShipPlus", "WarShipSkuldelevMul", "skuldelev", 7f);
            AddShip("OdinShipPlus", "HerculeShipMul", "herculeship", 4f);
            AddShip("OdinShipPlus", "GoblinShipMul", "goblinship", 4f);

            ShipMatchers.Sort((a, b) => b.Key.Length.CompareTo(a.Key.Length));

            _harmony.PatchAll();
            Logger.LogInfo($"{PluginName} {PluginVersion} loaded (vanilla + OdinShip; DefaultShipMultiplier={DefaultShipMultiplier.Value})");
        }

        private void AddShip(string section, string key, string nameContains, float defaultMul)
        {
            ConfigEntry<float> entry = Config.Bind(section, key, defaultMul,
                $"Explore radius multiplier when ship GameObject name contains '{nameContains}'.");
            ShipMatchers.Add(new KeyValuePair<string, ConfigEntry<float>>(nameContains, entry));
        }

        private void OnDestroy()
        {
            _harmony.UnpatchSelf();
        }

        private void FixedUpdate()
        {
            if (!Enabled.Value || !_onBoat || Minimap.instance == null || ExploreRadiusField == null)
            {
                return;
            }

            if (_needApply && !_applied)
            {
                object cur = ExploreRadiusField.GetValue(Minimap.instance);
                _originalRadius = cur is float f ? f : 50f;
                _newRadius = _originalRadius * _mult;
                ExploreRadiusField.SetValue(Minimap.instance, _newRadius);
                _needApply = false;
                _applied = true;
                if (DebugLog.Value)
                {
                    Logger.LogInfo($"explore radius {_mult:0.##}x → {_newRadius}");
                }
            }

            if (_applied)
            {
                object cur = ExploreRadiusField.GetValue(Minimap.instance);
                float now = cur is float f ? f : _newRadius;
                if (!Mathf.Approximately(now, _newRadius))
                {
                    _needApply = true;
                    _applied = false;
                }
            }
        }

        internal static void OnEnterShip(Ship ship)
        {
            if (!Enabled.Value || ship == null || Player.m_localPlayer == null)
            {
                return;
            }

            string name = ship.gameObject != null ? ship.gameObject.name.ToLowerInvariant() : "";
            float mult = DefaultShipMultiplier.Value;
            string matched = "(default)";

            foreach (KeyValuePair<string, ConfigEntry<float>> kv in ShipMatchers)
            {
                if (name.Contains(kv.Key))
                {
                    mult = kv.Value.Value;
                    matched = kv.Key;
                    break;
                }
            }

            _onBoat = true;
            _mult = mult;
            _needApply = true;
            _applied = false;

            if (DebugLog.Value && Instance != null)
            {
                Instance.Logger.LogInfo($"enter ship '{name}' match={matched} mult={mult}");
            }
        }

        internal static void OnLeaveShip()
        {
            if (!_onBoat)
            {
                return;
            }

            _onBoat = false;
            _mult = 1f;
            _needApply = false;
            _applied = false;

            if (Minimap.instance != null && ExploreRadiusField != null)
            {
                ExploreRadiusField.SetValue(Minimap.instance, _originalRadius);
                if (DebugLog.Value && Instance != null)
                {
                    Instance.Logger.LogInfo($"leave ship, reset explore radius to {_originalRadius}");
                }
            }
        }

        [HarmonyPatch(typeof(Ship), "OnTriggerEnter")]
        private static class ShipEnterPatch
        {
            private static void Postfix(Collider collider, Ship __instance)
            {
                if (collider == null)
                {
                    return;
                }

                Player player = collider.GetComponent<Player>();
                if (player != null && player == Player.m_localPlayer)
                {
                    OnEnterShip(__instance);
                }
            }
        }

        [HarmonyPatch(typeof(Ship), "OnTriggerExit")]
        private static class ShipExitPatch
        {
            private static void Postfix(Collider collider)
            {
                if (collider == null)
                {
                    return;
                }

                Player player = collider.GetComponent<Player>();
                if (player != null && player == Player.m_localPlayer)
                {
                    OnLeaveShip();
                }
            }
        }

        [HarmonyPatch(typeof(Ship), "OnDestroyed")]
        private static class ShipDestroyedPatch
        {
            private static void Prefix()
            {
                OnLeaveShip();
            }
        }

        [HarmonyPatch(typeof(Player), "OnDeath")]
        private static class PlayerDeathPatch
        {
            private static void Prefix()
            {
                OnLeaveShip();
            }
        }
    }
}
