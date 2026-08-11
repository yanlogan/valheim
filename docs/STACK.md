# Стек модов (чеклист)

Last updated: 2026-08-11  
Менеджер: **r2modman** → Valheim  
Yanlo zip: [Latest Release](https://github.com/yanlogan/valheim/releases/latest)  
Геймплей: [HOWTO.md](HOWTO.md)

В таблицах: **Имя** — что вбить в Online → поиск (copy-paste). Версии — только где важно совпасть с хостом.

---

## Как поставить

1. Online → вставь **Имя** из таблицы → Install → Enable.
2. Пройди **Удалить** (Disable / Uninstall, если стоит).
3. Распакуй `YanloMods-….zip` из Release в `BepInEx/plugins/` (папки `Yanlo-*`).
4. Выставь [Конфиги](#конфиги).
5. **По желанию** — что нужно из списка ниже.

Практично: тот же enabled-список, что у хоста, минус личное из «По желанию».

---

## Удалить

| Имя | Что делает | Почему убрать |
|-----|------------|---------------|
| AzuAutoStore | автоскладирование в сундуки | жрёт лут из ItemDrawers |
| TrashItems | отдельный Trash | Trash уже в QSS |
| ShipExploration | радиус карты на корабле (GemHunter) | заменён на Yanlo-ShipExplorationAll |
| NoBuildRestriction | снимает лимиты стройки | не используем |
| ConditionalConfigSync | условный sync cfg | сирота, не нужен |
| TimedTorchesStayLit | факелы не гаснут | только на **dedicated**; на клиенте не нужен |

---

## ❗ Обязательно — зависимости

| Имя | Что делает |
|-----|------------|
| BepInExPack_Valheim | загрузчик модов (обычно уже есть с профилем r2modman) |
| Jotunn | библиотека для контент-модов |
| HookGenPatcher | генерит хуки / MMHOOK |
| YamlDotNet | YAML для модов |
| JsonDotNET | JSON для модов |
| MMHOOK | хуки Harmony (часто появляется сам после HookGenPatcher) |

---

## ❗ Обязательно — инвентарь / крафт / wards

| Имя | Что делает |
|-----|------------|
| AzuExtendedPlayerInventory | доп. ряды инвентаря и слоты экипа |
| Quick_Stack_Store_Sort_Trash_Restock | **Sort + Trash** (stack/restock/store-all у нас выкл) |
| SmarterContainers | умная раскладка в сундуки + Unload (~14 м) |
| ItemDrawers | настенный ящик на 1 тип, до 9999 шт. |
| AzuCraftyBoxes | крафт/стройка и HUD из сундуков (~50 м); версия **1.8.15**, без мода кик |
| ValheimPlus_Grantapher_Temporary | ряды с EPI, StructuralIntegrity; CraftFromChest **off** |
| WardIsLove | Thorward + радиус на дом (GUI); версия **3.7.2**, одна у всех |
| MultiUserChest | несколько игроков в одном сундуке |
| Recycle_N_Reclaim | разбор у верстака / Reclaim all на сундуке |
| Official_BepInEx_ConfigurationManager | меню настроек модов на **F1** |
| ImpactfulSkills | бонусы от скиллов + Voyager / Hauling / Animal Whisper |
| PlanBuild | синие планы / Blueprint Rune / Plan Totem; версия **0.18.4** |
| Better_Cartography_Table | шаринг пинов и эксплора у картографического стола |

---

## ❗ Обязательно — контент / мир

Без этих модов — missing prefabs / нет кусков и предметов мира.

| Имя | Что делает |
|-----|------------|
| PlantEverything | больше растений / семян / выращивания |
| Seasonality | сезоны (погода, окружение) |
| BoneAppetit | новая еда и станции готовки |
| HoneyPlus | расширенный мёд / ульи |
| Valharvest | доп. фермерский контент |
| OdinShip | доп. корабли (War/Cargo/…) |
| OdinHorse | лошади / ездовые |
| OdinsHorsePen | загон для лошадей |
| OdinCampsite | кемп / походный контент |
| PlantIt | доп. посадка / растения |
| Atos_Arrows_JVL | доп. стрелы |
| BetterArchery | улучшения лука / стрельбы |
| InfinityTools | инструменты без поломки (наш сетап) |
| Clutter | декоративные куски / clutter |
| MoreGatesExtended | доп. ворота / двери |
| XPortal | порталы между базами |
| TreesReborn | другие/улучшенные деревья |
| Venture_Terrain_Reset | сброс/правка террейна |
| InstantMonsterLootDrop | лут с мобов сразу на землю |

---

## ❗ Обязательно — Yanlo (не через Online)

Скачай zip с [Latest Release](https://github.com/yanlogan/valheim/releases/latest) → папки в `plugins/`:

| Имя (папка) | Версия | Что делает |
|-------------|--------|------------|
| Yanlo-ChestUnloadButton | **1.3.0** | кнопка **Unload под** Take All; leftovers → открытый сундук (нужны QSS + SC Unload) |
| Yanlo-ShipExplorationAll | **1.1.0** | больший радиус карты на ванили + OdinShip; не ставить GemHunter ShipExploration |

---

## По желанию

| Имя | Что делает |
|-----|------------|
| AAA_Crafting | удобный UI крафта + Recipe Tracker |
| ItemCompare | сравнение статов предметов в UI |
| VNEI | поиск предметов и рецептов |
| CraftGuard | вкладки Hammer Default/Mod View (`OrganizeRecipes=false` с AAA) |
| BetterSounds | замена SFX (**male**); после Install распаковать `CustomAudio.zip`; не вместе с Female |
| Expand_World_Music | движок кастомной музыки (нужен для Forteca) |
| Forteca_Soundtrack | треки Forteca в меню через EWM |
| PlantEasily | удобная посадка/сбор |
| QuickTeleport | быстрый телепорт по хоткею |
| FenceSnap | snap заборов при стройке |
| Willybachs_HD_Seasonality | HD-текстуры сезонов |
| MyLittleUI | мелкие UI-удобства |
| ImprovedBuildHud | удобнее HUD стройки |
| BetterAutoRun | улучшенный autorun |
| AutoMapPins | автопины на карте |

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
- Не находится в Online → уточни у хоста точное имя / автора (иногда несколько пакетов с похожим названием).
