# Стек модов (чеклист)

Last updated: 2026-08-11  
Менеджер: **r2modman** → Valheim  
Yanlo zip: [Latest Release](https://github.com/yanlogan/valheim/releases/latest)  
Геймплей: [HOWTO.md](HOWTO.md)

В таблицах: **имя** — как искать Online; **folder** — папка в `plugins/` после Install; **Что делает** — коротко про геймплей.  
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

| Имя | Folder | Что делает | Почему убрать |
|-----|--------|------------|---------------|
| AzuAutoStore | `Azumatt-AzuAutoStore` | автоскладирование в сундуки | жрёт лут из ItemDrawers |
| TrashItems | `virtuaCode-TrashItems` | отдельный Trash | Trash уже в QSS |
| ShipExploration (GemHunter) | `GemHunter1-ShipExploration` | радиус карты на корабле | заменён на Yanlo-ShipExplorationAll |
| No Build Restriction | `BlackViking-NoBuildRestriction` | снимает лимиты стройки | не используем |
| Conditional Config Sync | `shudnal-ConditionalConfigSync` | условный sync cfg | сирота, не нужен |
| Timed Torches Stay Lit | `TastyChickenLegs-TimedTorchesStayLit` | факелы не гаснут | только на **dedicated**; на клиенте не нужен |

---

## ❗ Обязательно — зависимости

| Имя | Folder | Что делает | Thunderstore |
|-----|--------|------------|--------------|
| BepInExPack Valheim | (через r2modman) | загрузчик модов BepInEx | [denikson/BepInExPack_Valheim](https://thunderstore.io/c/valheim/p/denikson/BepInExPack_Valheim/) |
| Jotunn | `ValheimModding-Jotunn` | библиотека для контент-модов | [ValheimModding/Jotunn](https://thunderstore.io/c/valheim/p/ValheimModding/Jotunn/) |
| HookGenPatcher | `ValheimModding-HookGenPatcher` | генерит MMHOOK / хуки | [ValheimModding/HookGenPatcher](https://thunderstore.io/c/valheim/p/ValheimModding/HookGenPatcher/) |
| YamlDotNet | `ValheimModding-YamlDotNet` | YAML для модов | [ValheimModding/YamlDotNet](https://thunderstore.io/c/valheim/p/ValheimModding/YamlDotNet/) |
| JsonDotNET | `ValheimModding-JsonDotNET` | JSON для модов | [ValheimModding/JsonDotNET](https://thunderstore.io/c/valheim/p/ValheimModding/JsonDotNET/) |
| MMHOOK | `MMHOOK` | сгенерированные хуки Harmony | ставится/генерится с паком |

---

## ❗ Обязательно — инвентарь / крафт / wards

| Имя | Folder | Что делает | Thunderstore |
|-----|--------|------------|--------------|
| AzuExtendedPlayerInventory | `Azumatt-AzuExtendedPlayerInventory` | доп. ряды инвентаря и слоты экипа | [Azumatt/AzuExtendedPlayerInventory](https://thunderstore.io/c/valheim/p/Azumatt/AzuExtendedPlayerInventory/) |
| Quick Stack Store Sort Trash Restock (QSS) | `Goldenrevolver-Quick_Stack_Store_Sort_Trash_Restock` | **Sort + Trash** (stack/restock/store-all у нас выкл) | [Goldenrevolver/…](https://thunderstore.io/c/valheim/p/Goldenrevolver/Quick_Stack_Store_Sort_Trash_Restock/) |
| SmarterContainers | `Roses-SmarterContainers` | умная раскладка в сундуки + Unload (~14 м) | [Roses/SmarterContainers](https://thunderstore.io/c/valheim/p/Roses/SmarterContainers/) |
| ItemDrawers | `makail-ItemDrawers` | настенный ящик на 1 тип, до 9999 шт. | [makail/ItemDrawers](https://thunderstore.io/c/valheim/p/makail/ItemDrawers/) |
| AzuCraftyBoxes | `Azumatt-AzuCraftyBoxes` | крафт/стройка и HUD из сундуков (~50 м); **1.8.15**, без мода кик | [Azumatt/AzuCraftyBoxes](https://thunderstore.io/c/valheim/p/Azumatt/AzuCraftyBoxes/) |
| ValheimPlus (Grantapher) | `Grantapher-ValheimPlus_Grantapher_Temporary` | ряды с EPI, StructuralIntegrity; CraftFromChest **off** | [Grantapher/ValheimPlus_Grantapher_Temporary](https://thunderstore.io/c/valheim/p/Grantapher/ValheimPlus_Grantapher_Temporary/) |
| WardIsLove | `Azumatt-WardIsLove` | Thorward + радиус на дом (GUI); **3.7.2**, одна версия у всех | [Azumatt/WardIsLove](https://thunderstore.io/c/valheim/p/Azumatt/WardIsLove/) |
| MultiUserChest | `MSchmoecker-MultiUserChest` | несколько игроков в одном сундуке | [MSchmoecker/MultiUserChest](https://thunderstore.io/c/valheim/p/MSchmoecker/MultiUserChest/) |
| Recycle N Reclaim | `Azumatt-Recycle_N_Reclaim` | разбор у верстака / Reclaim all на сундуке | [Azumatt/Recycle_N_Reclaim](https://thunderstore.io/c/valheim/p/Azumatt/Recycle_N_Reclaim/) |
| Configuration Manager | `Azumatt-Official_BepInEx_ConfigurationManager` | меню настроек модов на **F1** | [Azumatt/Official_BepInEx_ConfigurationManager](https://thunderstore.io/c/valheim/p/Azumatt/Official_BepInEx_ConfigurationManager/) |
| ImpactfulSkills | `MidnightMods-ImpactfulSkills` | бонусы от скиллов + Voyager / Hauling / Animal Whisper | [MidnightMods/ImpactfulSkills](https://thunderstore.io/c/valheim/p/MidnightMods/ImpactfulSkills/) |
| PlanBuild | `MathiasDecrock-PlanBuild` | синие планы / Blueprint Rune / Plan Totem; **0.18.4** | [MathiasDecrock/PlanBuild](https://thunderstore.io/c/valheim/p/MathiasDecrock/PlanBuild/) |
| Better Cartography Table | `nbusseneau-Better_Cartography_Table` | шаринг пинов и эксплора у картографического стола | [nbusseneau/Better_Cartography_Table](https://thunderstore.io/c/valheim/p/nbusseneau/Better_Cartography_Table/) |

---

## ❗ Обязательно — контент / мир

Без этих модов — missing prefabs / нет кусков и предметов мира.

| Имя | Folder | Что делает | Thunderstore |
|-----|--------|------------|--------------|
| PlantEverything | `Advize-PlantEverything` | больше растений / семян / выращивания | [Advize/PlantEverything](https://thunderstore.io/c/valheim/p/Advize/PlantEverything/) |
| Seasonality | `RustyMods-Seasonality` | сезоны (погода, окружение) | [RustyMods/Seasonality](https://thunderstore.io/c/valheim/p/RustyMods/Seasonality/) |
| BoneAppetit | `RockerKitten-BoneAppetit` | новая еда и станции готовки | [RockerKitten/BoneAppetit](https://thunderstore.io/c/valheim/p/RockerKitten/BoneAppetit/) |
| HoneyPlus | `OhhLoz-HoneyPlus` | расширенный мёд / ульи | [OhhLoz/HoneyPlus](https://thunderstore.io/c/valheim/p/OhhLoz/HoneyPlus/) |
| Valharvest | `Frenvius-Valharvest` | доп. фермерский контент | [Frenvius/Valharvest](https://thunderstore.io/c/valheim/p/Frenvius/Valharvest/) |
| OdinShip | `Marlthon-OdinShip` | доп. корабли (War/Cargo/…) | [Marlthon/OdinShip](https://thunderstore.io/c/valheim/p/Marlthon/OdinShip/) |
| OdinHorse | `OdinPlus-OdinHorse` | лошади / ездовые | [OdinPlus/OdinHorse](https://thunderstore.io/c/valheim/p/OdinPlus/OdinHorse/) |
| OdinsHorsePen | `OdinPlus-OdinsHorsePen` | загон для лошадей | [OdinPlus/OdinsHorsePen](https://thunderstore.io/c/valheim/p/OdinPlus/OdinsHorsePen/) |
| OdinCampsite | `OdinPlus-OdinCampsite` | кемп / походный контент | [OdinPlus/OdinCampsite](https://thunderstore.io/c/valheim/p/OdinPlus/OdinCampsite/) |
| PlantIt | `OdinPlus-PlantIt` | доп. посадка / растения | [OdinPlus/PlantIt](https://thunderstore.io/c/valheim/p/OdinPlus/PlantIt/) |
| Atos Arrows JVL | `Digitalroot-Atos_Arrows_JVL` | доп. стрелы | [Digitalroot/Atos_Arrows_JVL](https://thunderstore.io/c/valheim/p/Digitalroot/Atos_Arrows_JVL/) |
| BetterArchery | `ishid4-BetterArchery` | улучшения лука / стрельбы | [ishid4/BetterArchery](https://thunderstore.io/c/valheim/p/ishid4/BetterArchery/) |
| InfinityTools | `Numenos-InfinityTools` | инструменты без поломки (наш сетап) | [Numenos/InfinityTools](https://thunderstore.io/c/valheim/p/Numenos/InfinityTools/) |
| Clutter | `plumga-Clutter` | декоративные куски / clutter | [plumga/Clutter](https://thunderstore.io/c/valheim/p/plumga/Clutter/) |
| MoreGatesExtended | `shudnal-MoreGatesExtended` | доп. ворота / двери | [shudnal/MoreGatesExtended](https://thunderstore.io/c/valheim/p/shudnal/MoreGatesExtended/) |
| XPortal | `SpikeHimself-XPortal` | порталы между базами | [SpikeHimself/XPortal](https://thunderstore.io/c/valheim/p/SpikeHimself/XPortal/) |
| TreesReborn | `TastyChickenLegs-TreesReborn` | другие/улучшенные деревья | [TastyChickenLegs/TreesReborn](https://thunderstore.io/c/valheim/p/TastyChickenLegs/TreesReborn/) |
| Venture Terrain Reset | `VentureValheim-Venture_Terrain_Reset` | сброс/правка террейна | [VentureValheim/Venture_Terrain_Reset](https://thunderstore.io/c/valheim/p/VentureValheim/Venture_Terrain_Reset/) |
| Instant Monster Loot Drop | `cjayride-InstantMonsterLootDrop` | лут с мобов сразу на землю | [cjayride/InstantMonsterLootDrop](https://thunderstore.io/c/valheim/p/cjayride/InstantMonsterLootDrop/) |

---

## ❗ Обязательно — Yanlo (не Thunderstore)

Скачай zip с [Latest Release](https://github.com/yanlogan/valheim/releases/latest) → папки в `plugins/`:

| Folder | Версия | Что делает |
|--------|--------|------------|
| `Yanlo-ChestUnloadButton` | **1.3.0** | кнопка **Unload под** Take All; eligible leftovers → открытый сундук (нужны QSS + SC Unload) |
| `Yanlo-ShipExplorationAll` | **1.1.0** | больший радиус карты на ванили + OdinShip; не ставить GemHunter рядом |

---

## По желанию

| Имя | Folder | Что делает | Thunderstore |
|-----|--------|------------|--------------|
| AAA Crafting | `Azumatt-AAA_Crafting` | удобный UI крафта + Recipe Tracker | [Azumatt/AAA_Crafting](https://thunderstore.io/c/valheim/p/Azumatt/AAA_Crafting/) |
| ItemCompare | `Azumatt-ItemCompare` | сравнение статов предметов в UI | [Azumatt/ItemCompare](https://thunderstore.io/c/valheim/p/Azumatt/ItemCompare/) |
| VNEI | `MSchmoecker-VNEI` | поиск предметов и рецептов | [MSchmoecker/VNEI](https://thunderstore.io/c/valheim/p/MSchmoecker/VNEI/) |
| CraftGuard | `jg224-CraftGuard` | вкладки Hammer Default/Mod View (`OrganizeRecipes=false` с AAA) | [jg224/CraftGuard](https://thunderstore.io/c/valheim/p/jg224/CraftGuard/) |
| BetterSounds (male) | `Wiandar-BetterSounds` | замена SFX; после Install — `CustomAudio.zip`; не вместе с Female | [Wiandar/BetterSounds](https://thunderstore.io/c/valheim/p/Wiandar/BetterSounds/) |
| Expand World Music | `JereKuusela-Expand_World_Music` | движок кастомной музыки (нужен для Forteca) | [JereKuusela/Expand_World_Music](https://thunderstore.io/c/valheim/p/JereKuusela/Expand_World_Music/) |
| Forteca Soundtrack | `BlackViking-Forteca_Soundtrack` | треки Forteca в меню через EWM | [BlackViking/Forteca_Soundtrack](https://thunderstore.io/c/valheim/p/BlackViking/Forteca_Soundtrack/) |
| PlantEasily | `Advize-PlantEasily` | удобная посадка/сбор | [Advize/PlantEasily](https://thunderstore.io/c/valheim/p/Advize/PlantEasily/) |
| QuickTeleport | `OdinPlus-QuickTeleport` | быстрый телепорт по хоткею | [OdinPlus/QuickTeleport](https://thunderstore.io/c/valheim/p/OdinPlus/QuickTeleport/) |
| FenceSnap | `MSchmoecker-FenceSnap` | snap заборов при стройке | [MSchmoecker/FenceSnap](https://thunderstore.io/c/valheim/p/MSchmoecker/FenceSnap/) |
| Willybach HD Seasonality | `Willybach-Willybachs_HD_Seasonality` | HD-текстуры сезонов | [Willybach/Willybachs_HD_Seasonality](https://thunderstore.io/c/valheim/p/Willybach/Willybachs_HD_Seasonality/) |
| MyLittleUI | `shudnal-MyLittleUI` | мелкие UI-удобства | [shudnal/MyLittleUI](https://thunderstore.io/c/valheim/p/shudnal/MyLittleUI/) |
| Improved Build Hud | `RandyKnapp-ImprovedBuildHud` | удобнее HUD стройки | [RandyKnapp/ImprovedBuildHud](https://thunderstore.io/c/valheim/p/RandyKnapp/ImprovedBuildHud/) |
| BetterAutoRun | `nearbear-BetterAutoRun` | улучшенный autorun | [nearbear/BetterAutoRun](https://thunderstore.io/c/valheim/p/nearbear/BetterAutoRun/) |
| AutoMapPins | `abfielder-AutoMapPins` | автопины на карте | [abfielder/AutoMapPins](https://thunderstore.io/c/valheim/p/abfielder/AutoMapPins/) |

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
