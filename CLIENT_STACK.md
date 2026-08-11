# Client stack (друзья / Valheim_Client)

Last updated: 2026-08-11  
Профиль r2modman: **`Valheim_Client`**  
Yanlo zip: [Latest Release](https://github.com/yanlogan/valheim/releases/latest)  
Циклы: [`changelogs/`](changelogs/) · полный PENDING пока open

Чужие моды ставь через **r2modman Online**. Кастомные Yanlo — из zip релиза (не Thunderstore). Полный export профиля (~1 ГБ) **не** раздаём.

---

## Как поставить (кратко)

1. r2modman → профиль под Valheim → поставь **Required** ниже (и контент как у хоста).
2. Выключи / удали всё из **Must remove**.
3. Скачай `YanloMods-….zip` с [Latest Release](https://github.com/yanlogan/valheim/releases/latest) → распакуй папки `Yanlo-*` в  
   `…\Valheim_Client\BepInEx\plugins\`.
4. Проставь **shared cfg** (геймплей-ключи; хоткеи свои не затирай) — см. цикл / секцию ниже.
5. Optional — по желанию.

---

## Must remove

| Folder | Почему |
|--------|--------|
| `Azumatt-AzuAutoStore` | жрёт лут из ItemDrawers |
| `virtuaCode-TrashItems` | Trash уже в QSS |
| `GemHunter1-ShipExploration` | заменён на Yanlo-ShipExplorationAll |
| `BlackViking-NoBuildRestriction` | не используем |
| `Yanlo-QSSSortButtonOffset` | устарел; логика в ChestUnloadButton |

---

## Required (сервер + клиент)

| Folder | Геймплей |
|--------|----------|
| BepInEx pack + `Jotunn` / `HookGenPatcher` / `YamlDotNet` / `MMHOOK` | deps |
| `Azumatt-AzuExtendedPlayerInventory` | доп. ряды / экип |
| `Goldenrevolver-Quick_Stack_Store_Sort_Trash_Restock` | **только Sort + Trash** (stack/restock/store-all выкл) |
| `Roses-SmarterContainers` | умная раскладка + Unload (~14 м) |
| `makail-ItemDrawers` | настенный ящик на 1 тип, до 9999 |
| `Azumatt-AzuCraftyBoxes` **1.8.15** | крафт/стройка из сундуков ~50 м; без мода сервер кикает |
| `Grantapher-ValheimPlus_Grantapher_Temporary` | ряды с EPI; StructuralIntegrity; **CraftFromChest = false** |
| `Azumatt-WardIsLove` **3.7.2** | Thorward + радиус на дом (GUI); одна версия у всех |
| `MidnightMods-ImpactfulSkills` | бонусы скиллов + Voyager / Hauling / Animal Whisper |
| `MathiasDecrock-PlanBuild` **0.18.4** | синие планы / Blueprint Rune / Plan Totem |
| `MSchmoecker-MultiUserChest` | несколько человек в одном сундуке |
| `Azumatt-Recycle_N_Reclaim` | разбор у верстака / Reclaim all |
| `Azumatt-Official_BepInEx_ConfigurationManager` | F1 |
| `RockerKitten-BoneAppetit` | еда/станции BoneAppetit |
| `nbusseneau-Better_Cartography_Table` | шаринг пинов у картографического стола |
| Контент как у хоста | PlantEverything, Seasonality, Valharvest, HoneyPlus, OdinShip, OdinHorse, OdinsHorsePen, OdinCampsite, PlantIt, Atos_Arrows, BetterArchery, InfinityTools, Clutter, MoreGatesExtended, XPortal, TreesReborn, InstantMonsterLootDrop, … — иначе missing prefabs |

Практично: **тот же enabled-список, что у хоста**, минус личный optional; всегда **Must remove** + shared cfg.

---

## Required Yanlo (client-only)

Скачать zip с релиза → папки в `plugins/`:

| Folder | Версия | Геймплей |
|--------|--------|----------|
| `Yanlo-ChestUnloadButton` | **1.3.0** | кнопка **Unload под** Take All; leftovers eligible → открытый сундук. Нужны QSS + SC Unload. |
| `Yanlo-ShipExplorationAll` | **1.1.0** | больший радиус карты на ванили + OdinShip 0.7.6. Не ставить GemHunter рядом. |

Не нужны на dedicated.

---

## Optional (только клиент)

| Folder | Геймплей |
|--------|----------|
| `Azumatt-AAA_Crafting` | удобный крафт + Recipe Tracker |
| `Azumatt-ItemCompare` | сравнение статов |
| `MSchmoecker-VNEI` | поиск предметов/рецептов |
| `jg224-CraftGuard` | Hammer Mod View (`OrganizeRecipes=false` с AAA) |
| `Wiandar-BetterSounds` (male) | SFX; после Install — `CustomAudio.zip`; не вместе с Female |
| `JereKuusela-Expand_World_Music` + `BlackViking-Forteca_Soundtrack` | музыка меню |
| `Advize-PlantEasily` / `OdinPlus-QuickTeleport` / `MSchmoecker-FenceSnap` | QoL |
| `Willybach-Willybachs_HD_Seasonality` | HD сезоны |

---

## Shared cfg (без хоткеев)

Не затирай чужие `*Keybind*`. Минимум:

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
; UseTopDownLogicForEverything НЕ синкается с сервера — выставь у себя

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

WardIsLove: свой Thorward на доме, **Ward Range** = число со знака; на сервере `Ward Control=true` (перезайди).

---

## Как пользоваться (шпаргалка)

- **Sort** `O` / кнопка — сверху вниз; Place Stacks скрыт  
- **Trash** — корзина / `Delete`  
- **Unload** — под Take All; ~14 м + leftovers в открытый сундук  
- **CraftyBoxes** — тянет до ~50 м; пауза pull обычно `O+LeftAlt`  
- **Cartography** — public пин: у стола, `LeftShift`+клик  
- **PlanBuild** — Plan Hammer → материалы / Totem; Blueprint Rune для копий  

Детали цикла — в [`changelogs/PENDING.md`](changelogs/PENDING.md) или последнем archived файле.
