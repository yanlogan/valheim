# Стек модов (чеклист)

Last updated: 2026-08-11  
Менеджер: **r2modman** → Valheim  
Yanlo zip: [Latest Release](https://github.com/yanlogan/valheim/releases/latest)  
Геймплей: [HOWTO.md](HOWTO.md)

В таблицах: **имя** — как искать Online; **folder** — папка в `plugins/` после Install.  
Ссылки ведут на страницу пакета Thunderstore (версия у хоста может отличаться — ориентируйся на версии в таблицах **Обязательно**, где указано).

---

## Как поставить

1. Online → поиск по имени → Install → Enable.
2. Пройди **Удалить**.
3. Распакуй `YanloMods-….zip` из Release в `BepInEx/plugins/` (папки `Yanlo-*`).
4. Выставь [Конфиги](#конфиги).
5. **По желанию** — что нужно из списка ниже.

Практично: тот же enabled-список, что у хоста, минус личное из «По желанию».

---

## Удалить

| Имя | Folder | Почему |
|-----|--------|--------|
| AzuAutoStore | `Azumatt-AzuAutoStore` | жрёт лут из ItemDrawers |
| TrashItems | `virtuaCode-TrashItems` | Trash уже в QSS |
| ShipExploration (GemHunter) | `GemHunter1-ShipExploration` | заменён на Yanlo-ShipExplorationAll |
| No Build Restriction | `BlackViking-NoBuildRestriction` | не используем |
| Yanlo QSS Sort Offset | `Yanlo-QSSSortButtonOffset` | устарел |
| Conditional Config Sync | `shudnal-ConditionalConfigSync` | сирота, не нужен |

---

## ❗ Обязательно — зависимости

| Имя | Folder | Thunderstore |
|-----|--------|--------------|
| BepInExPack Valheim | (через r2modman) | [denikson/BepInExPack_Valheim](https://thunderstore.io/c/valheim/p/denikson/BepInExPack_Valheim/) |
| Jotunn | `ValheimModding-Jotunn` | [ValheimModding/Jotunn](https://thunderstore.io/c/valheim/p/ValheimModding/Jotunn/) |
| HookGenPatcher | `ValheimModding-HookGenPatcher` | [ValheimModding/HookGenPatcher](https://thunderstore.io/c/valheim/p/ValheimModding/HookGenPatcher/) |
| YamlDotNet | `ValheimModding-YamlDotNet` | [ValheimModding/YamlDotNet](https://thunderstore.io/c/valheim/p/ValheimModding/YamlDotNet/) |
| JsonDotNET | `ValheimModding-JsonDotNET` | [ValheimModding/JsonDotNET](https://thunderstore.io/c/valheim/p/ValheimModding/JsonDotNET/) |
| MMHOOK | `MMHOOK` | ставится/генерится с паком |

---

## ❗ Обязательно — инвентарь / крафт / wards

| Имя | Folder | Версия / заметка | Thunderstore |
|-----|--------|------------------|--------------|
| AzuExtendedPlayerInventory | `Azumatt-AzuExtendedPlayerInventory` | доп. ряды | [Azumatt/AzuExtendedPlayerInventory](https://thunderstore.io/c/valheim/p/Azumatt/AzuExtendedPlayerInventory/) |
| Quick Stack Store Sort Trash Restock (QSS) | `Goldenrevolver-Quick_Stack_Store_Sort_Trash_Restock` | **только Sort+Trash** | [Goldenrevolver/…](https://thunderstore.io/c/valheim/p/Goldenrevolver/Quick_Stack_Store_Sort_Trash_Restock/) |
| SmarterContainers | `Roses-SmarterContainers` | Unload ~14 м | [Roses/SmarterContainers](https://thunderstore.io/c/valheim/p/Roses/SmarterContainers/) |
| ItemDrawers | `makail-ItemDrawers` | ящики 9999 | [makail/ItemDrawers](https://thunderstore.io/c/valheim/p/makail/ItemDrawers/) |
| AzuCraftyBoxes | `Azumatt-AzuCraftyBoxes` | **1.8.15**, range 50; без мода кик | [Azumatt/AzuCraftyBoxes](https://thunderstore.io/c/valheim/p/Azumatt/AzuCraftyBoxes/) |
| ValheimPlus (Grantapher) | `Grantapher-ValheimPlus_Grantapher_Temporary` | CraftFromChest **off** | [Grantapher/ValheimPlus_Grantapher_Temporary](https://thunderstore.io/c/valheim/p/Grantapher/ValheimPlus_Grantapher_Temporary/) |
| WardIsLove | `Azumatt-WardIsLove` | **3.7.2**, одна версия у всех | [Azumatt/WardIsLove](https://thunderstore.io/c/valheim/p/Azumatt/WardIsLove/) |
| MultiUserChest | `MSchmoecker-MultiUserChest` | общий сундук | [MSchmoecker/MultiUserChest](https://thunderstore.io/c/valheim/p/MSchmoecker/MultiUserChest/) |
| Recycle N Reclaim | `Azumatt-Recycle_N_Reclaim` | разбор | [Azumatt/Recycle_N_Reclaim](https://thunderstore.io/c/valheim/p/Azumatt/Recycle_N_Reclaim/) |
| Configuration Manager | `Azumatt-Official_BepInEx_ConfigurationManager` | F1 | [Azumatt/Official_BepInEx_ConfigurationManager](https://thunderstore.io/c/valheim/p/Azumatt/Official_BepInEx_ConfigurationManager/) |
| ImpactfulSkills | `MidnightMods-ImpactfulSkills` | скиллы | [MidnightMods/ImpactfulSkills](https://thunderstore.io/c/valheim/p/MidnightMods/ImpactfulSkills/) |
| PlanBuild | `MathiasDecrock-PlanBuild` | **0.18.4** | [MathiasDecrock/PlanBuild](https://thunderstore.io/c/valheim/p/MathiasDecrock/PlanBuild/) |
| Better Cartography Table | `nbusseneau-Better_Cartography_Table` | пины | [nbusseneau/Better_Cartography_Table](https://thunderstore.io/c/valheim/p/nbusseneau/Better_Cartography_Table/) |

---

## ❗ Обязательно — контент / мир

Без этих модов — missing prefabs / нет кусков и предметов мира.

| Имя | Folder | Thunderstore |
|-----|--------|--------------|
| PlantEverything | `Advize-PlantEverything` | [Advize/PlantEverything](https://thunderstore.io/c/valheim/p/Advize/PlantEverything/) |
| Seasonality | `RustyMods-Seasonality` | [RustyMods/Seasonality](https://thunderstore.io/c/valheim/p/RustyMods/Seasonality/) |
| BoneAppetit | `RockerKitten-BoneAppetit` | [RockerKitten/BoneAppetit](https://thunderstore.io/c/valheim/p/RockerKitten/BoneAppetit/) |
| HoneyPlus | `OhhLoz-HoneyPlus` | [OhhLoz/HoneyPlus](https://thunderstore.io/c/valheim/p/OhhLoz/HoneyPlus/) |
| Valharvest | `Frenvius-Valharvest` | [Frenvius/Valharvest](https://thunderstore.io/c/valheim/p/Frenvius/Valharvest/) |
| OdinShip | `Marlthon-OdinShip` | [Marlthon/OdinShip](https://thunderstore.io/c/valheim/p/Marlthon/OdinShip/) |
| OdinHorse | `OdinPlus-OdinHorse` | [OdinPlus/OdinHorse](https://thunderstore.io/c/valheim/p/OdinPlus/OdinHorse/) |
| OdinsHorsePen | `OdinPlus-OdinsHorsePen` | [OdinPlus/OdinsHorsePen](https://thunderstore.io/c/valheim/p/OdinPlus/OdinsHorsePen/) |
| OdinCampsite | `OdinPlus-OdinCampsite` | [OdinPlus/OdinCampsite](https://thunderstore.io/c/valheim/p/OdinPlus/OdinCampsite/) |
| PlantIt | `OdinPlus-PlantIt` | [OdinPlus/PlantIt](https://thunderstore.io/c/valheim/p/OdinPlus/PlantIt/) |
| Atos Arrows JVL | `Digitalroot-Atos_Arrows_JVL` | [Digitalroot/Atos_Arrows_JVL](https://thunderstore.io/c/valheim/p/Digitalroot/Atos_Arrows_JVL/) |
| BetterArchery | `ishid4-BetterArchery` | [ishid4/BetterArchery](https://thunderstore.io/c/valheim/p/ishid4/BetterArchery/) |
| InfinityTools | `Numenos-InfinityTools` | [Numenos/InfinityTools](https://thunderstore.io/c/valheim/p/Numenos/InfinityTools/) |
| Clutter | `plumga-Clutter` | [plumga/Clutter](https://thunderstore.io/c/valheim/p/plumga/Clutter/) |
| MoreGatesExtended | `shudnal-MoreGatesExtended` | [shudnal/MoreGatesExtended](https://thunderstore.io/c/valheim/p/shudnal/MoreGatesExtended/) |
| XPortal | `SpikeHimself-XPortal` | [SpikeHimself/XPortal](https://thunderstore.io/c/valheim/p/SpikeHimself/XPortal/) |
| TreesReborn | `TastyChickenLegs-TreesReborn` | [TastyChickenLegs/TreesReborn](https://thunderstore.io/c/valheim/p/TastyChickenLegs/TreesReborn/) |
| Venture Terrain Reset | `VentureValheim-Venture_Terrain_Reset` | [VentureValheim/Venture_Terrain_Reset](https://thunderstore.io/c/valheim/p/VentureValheim/Venture_Terrain_Reset/) |
| Instant Monster Loot Drop | `cjayride-InstantMonsterLootDrop` | [cjayride/InstantMonsterLootDrop](https://thunderstore.io/c/valheim/p/cjayride/InstantMonsterLootDrop/) |

---

## ❗ Обязательно — Yanlo (не Thunderstore)

Скачай zip с [Latest Release](https://github.com/yanlogan/valheim/releases/latest) → папки в `plugins/`:

| Folder | Версия | Зачем |
|--------|--------|--------|
| `Yanlo-ChestUnloadButton` | **1.3.0** | Unload под Take All + leftovers → открытый сундук (нужны QSS + SC Unload) |
| `Yanlo-ShipExplorationAll` | **1.1.0** | радиус карты на ванили + OdinShip; не ставить GemHunter рядом |

---

## По желанию

| Имя | Folder | Thunderstore |
|-----|--------|--------------|
| AAA Crafting | `Azumatt-AAA_Crafting` | [Azumatt/AAA_Crafting](https://thunderstore.io/c/valheim/p/Azumatt/AAA_Crafting/) |
| ItemCompare | `Azumatt-ItemCompare` | [Azumatt/ItemCompare](https://thunderstore.io/c/valheim/p/Azumatt/ItemCompare/) |
| VNEI | `MSchmoecker-VNEI` | [MSchmoecker/VNEI](https://thunderstore.io/c/valheim/p/MSchmoecker/VNEI/) |
| CraftGuard | `jg224-CraftGuard` | [jg224/CraftGuard](https://thunderstore.io/c/valheim/p/jg224/CraftGuard/) — `OrganizeRecipes=false` с AAA |
| BetterSounds (male) | `Wiandar-BetterSounds` | [Wiandar/BetterSounds](https://thunderstore.io/c/valheim/p/Wiandar/BetterSounds/) — после Install распаковать `CustomAudio.zip`; не вместе с Female |
| Expand World Music | `JereKuusela-Expand_World_Music` | [JereKuusela/Expand_World_Music](https://thunderstore.io/c/valheim/p/JereKuusela/Expand_World_Music/) |
| Forteca Soundtrack | `BlackViking-Forteca_Soundtrack` | [BlackViking/Forteca_Soundtrack](https://thunderstore.io/c/valheim/p/BlackViking/Forteca_Soundtrack/) |
| PlantEasily | `Advize-PlantEasily` | [Advize/PlantEasily](https://thunderstore.io/c/valheim/p/Advize/PlantEasily/) |
| QuickTeleport | `OdinPlus-QuickTeleport` | [OdinPlus/QuickTeleport](https://thunderstore.io/c/valheim/p/OdinPlus/QuickTeleport/) |
| FenceSnap | `MSchmoecker-FenceSnap` | [MSchmoecker/FenceSnap](https://thunderstore.io/c/valheim/p/MSchmoecker/FenceSnap/) |
| Willybach HD Seasonality | `Willybach-Willybachs_HD_Seasonality` | [Willybach/Willybachs_HD_Seasonality](https://thunderstore.io/c/valheim/p/Willybach/Willybachs_HD_Seasonality/) |
| MyLittleUI | `shudnal-MyLittleUI` | [shudnal/MyLittleUI](https://thunderstore.io/c/valheim/p/shudnal/MyLittleUI/) |
| Improved Build Hud | `RandyKnapp-ImprovedBuildHud` | [RandyKnapp/ImprovedBuildHud](https://thunderstore.io/c/valheim/p/RandyKnapp/ImprovedBuildHud/) |
| BetterAutoRun | `nearbear-BetterAutoRun` | [nearbear/BetterAutoRun](https://thunderstore.io/c/valheim/p/nearbear/BetterAutoRun/) |
| AutoMapPins | `abfielder-AutoMapPins` | [abfielder/AutoMapPins](https://thunderstore.io/c/valheim/p/abfielder/AutoMapPins/) |

---

## Конфиги

Копируй **только эти ключи**. Не затирай `*Keybind*` / свои бинды.

```ini
; goldenrevolver.quick_stack_store.cfg
UseTopDownLogicForEverything = true
DisplayQuickStackButtons = Disabled
HideBaseGamePlaceStacksButton = true
DisplayRestockButtons = Disabled
DisplayStoreAllButton = false
NeverMoveTakeAllButton = true
DisplaySortButtons = Both
SortMergesStacks = false
DisplayTrashCanUI = true
AutoSort = Never
; UseTopDownLogicForEverything НЕ приходит с сервера — выставь у себя

; flueno.SmartContainers.cfg
[General]
range = 14
[Unload]
enabled = true
nativeButton = false
consumableFiltering = false
groupsList = valuables,ore,wood,mushrooms,berries,vegetables

; Azumatt.AzuCraftyBoxes.cfg
[2 - CraftyBoxes]
Container Range = 50

; valheim_plus.cfg
[CraftFromChest]
enabled = false
[StructuralIntegrity]
enabled = true
```

---

## Дополнительно

- **WardIsLove:** свой Thorward на доме, Ward Range = число со знака; после рестарта сервера просто перезайди.
