# Стек модов (чеклист)

Last updated: 2026-08-11  
Менеджер: **r2modman** → Valheim  
Yanlo zip: [Latest Release](https://github.com/yanlogan/valheim/releases/latest)  
Геймплей: [HOWTO.md](HOWTO.md)

В таблицах: **Имя** — что вбить в Online → поиск (copy-paste); под ним мелким шрифтом — **автор** (если несколько результатов). Версии — только где важно совпасть с хостом.

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
| AzuAutoStore<br><sub>Azumatt</sub> | автоскладирование в сундуки | жрёт лут из ItemDrawers |
| TrashItems<br><sub>virtuaCode</sub> | отдельный Trash | Trash уже в QSS |
| ShipExploration<br><sub>GemHunter1</sub> | радиус карты на корабле | заменён на Yanlo-ShipExplorationAll |
| NoBuildRestriction<br><sub>BlackViking</sub> | снимает лимиты стройки | не используем |
| ConditionalConfigSync<br><sub>shudnal</sub> | условный sync cfg | сирота, не нужен |
| TimedTorchesStayLit<br><sub>TastyChickenLegs</sub> | факелы не гаснут | только на **dedicated**; на клиенте не нужен |

---

## ❗ Обязательно — зависимости

| Имя | Что делает |
|-----|------------|
| BepInExPack_Valheim<br><sub>denikson</sub> | загрузчик модов (обычно уже есть с профилем r2modman) |
| Jotunn<br><sub>ValheimModding</sub> | библиотека для контент-модов |
| HookGenPatcher<br><sub>ValheimModding</sub> | генерит хуки / MMHOOK |
| YamlDotNet<br><sub>ValheimModding</sub> | YAML для модов |
| JsonDotNET<br><sub>ValheimModding</sub> | JSON для модов |
| MMHOOK<br><sub>—</sub> | хуки Harmony (часто появляется сам после HookGenPatcher) |

---

## ❗ Обязательно — инвентарь / крафт / wards

| Имя | Что делает |
|-----|------------|
| AzuExtendedPlayerInventory<br><sub>Azumatt</sub> | доп. ряды инвентаря и слоты экипа |
| Quick_Stack_Store_Sort_Trash_Restock<br><sub>Goldenrevolver</sub> | **Sort + Trash** (stack/restock/store-all у нас выкл) |
| SmarterContainers<br><sub>Roses</sub> | умная раскладка в сундуки + Unload (~14 м) |
| ItemDrawers<br><sub>makail</sub> | настенный ящик на 1 тип, до 9999 шт. |
| AzuCraftyBoxes<br><sub>Azumatt</sub> | крафт/стройка и HUD из сундуков (~50 м); версия **1.8.15**, без мода кик |
| ValheimPlus_Grantapher_Temporary<br><sub>Grantapher</sub> | ряды с EPI, StructuralIntegrity; CraftFromChest **off** |
| WardIsLove<br><sub>Azumatt</sub> | Thorward + радиус на дом (GUI); версия **3.7.2**, одна у всех |
| MultiUserChest<br><sub>MSchmoecker</sub> | несколько игроков в одном сундуке |
| Recycle_N_Reclaim<br><sub>Azumatt</sub> | разбор у верстака / Reclaim all на сундуке |
| Official_BepInEx_ConfigurationManager<br><sub>Azumatt</sub> | меню настроек модов на **F1** |
| ImpactfulSkills<br><sub>MidnightMods</sub> | бонусы от скиллов + Voyager / Hauling / Animal Whisper |
| PlanBuild<br><sub>MathiasDecrock</sub> | синие планы / Blueprint Rune / Plan Totem; версия **0.18.4** |
| Better_Cartography_Table<br><sub>nbusseneau</sub> | шаринг пинов и эксплора у картографического стола |

---

## ❗ Обязательно — контент / мир

Без этих модов — missing prefabs / нет кусков и предметов мира.

| Имя | Что делает |
|-----|------------|
| PlantEverything<br><sub>Advize</sub> | больше растений / семян / выращивания |
| Seasonality<br><sub>RustyMods</sub> | сезоны (погода, окружение) |
| BoneAppetit<br><sub>RockerKitten</sub> | новая еда и станции готовки |
| HoneyPlus<br><sub>OhhLoz</sub> | расширенный мёд / ульи |
| Valharvest<br><sub>Frenvius</sub> | доп. фермерский контент |
| OdinShip<br><sub>Marlthon</sub> | доп. корабли (War/Cargo/…) |
| OdinHorse<br><sub>OdinPlus</sub> | лошади / ездовые |
| OdinsHorsePen<br><sub>OdinPlus</sub> | загон для лошадей |
| OdinCampsite<br><sub>OdinPlus</sub> | кемп / походный контент |
| PlantIt<br><sub>OdinPlus</sub> | доп. посадка / растения |
| Atos_Arrows_JVL<br><sub>Digitalroot</sub> | доп. стрелы |
| BetterArchery<br><sub>ishid4</sub> | улучшения лука / стрельбы |
| InfinityTools<br><sub>Numenos</sub> | инструменты без поломки (наш сетап) |
| Clutter<br><sub>plumga</sub> | декоративные куски / clutter |
| MoreGatesExtended<br><sub>shudnal</sub> | доп. ворота / двери |
| XPortal<br><sub>SpikeHimself</sub> | порталы между базами |
| TreesReborn<br><sub>TastyChickenLegs</sub> | другие/улучшенные деревья |
| Venture_Terrain_Reset<br><sub>VentureValheim</sub> | сброс/правка террейна |
| InstantMonsterLootDrop<br><sub>cjayride</sub> | лут с мобов сразу на землю |

---

## ❗ Обязательно — Yanlo (не через Online)

Скачай zip с [Latest Release](https://github.com/yanlogan/valheim/releases/latest) → папки в `plugins/`:

| Имя (папка) | Версия | Что делает |
|-------------|--------|------------|
| Yanlo-ChestUnloadButton<br><sub>Yanlo</sub> | **1.3.0** | кнопка **Unload под** Take All; leftovers → открытый сундук (нужны QSS + SC Unload) |
| Yanlo-ShipExplorationAll<br><sub>Yanlo</sub> | **1.1.0** | больший радиус карты на ванили + OdinShip; не ставить GemHunter ShipExploration |

---

## По желанию

| Имя | Что делает |
|-----|------------|
| AAA_Crafting<br><sub>Azumatt</sub> | удобный UI крафта + Recipe Tracker |
| ItemCompare<br><sub>Azumatt</sub> | сравнение статов предметов в UI |
| VNEI<br><sub>MSchmoecker</sub> | поиск предметов и рецептов |
| CraftGuard<br><sub>jg224</sub> | вкладки Hammer Default/Mod View (`OrganizeRecipes=false` с AAA) |
| BetterSounds<br><sub>Wiandar</sub> | замена SFX (**male**); после Install распаковать `CustomAudio.zip`; не вместе с Female |
| Expand_World_Music<br><sub>JereKuusela</sub> | движок кастомной музыки (нужен для Forteca) |
| Forteca_Soundtrack<br><sub>BlackViking</sub> | треки Forteca в меню через EWM |
| PlantEasily<br><sub>Advize</sub> | удобная посадка/сбор |
| QuickTeleport<br><sub>OdinPlus</sub> | быстрый телепорт по хоткею |
| FenceSnap<br><sub>MSchmoecker</sub> | snap заборов при стройке |
| Willybachs_HD_Seasonality<br><sub>Willybach</sub> | HD-текстуры сезонов |
| MyLittleUI<br><sub>shudnal</sub> | мелкие UI-удобства |
| ImprovedBuildHud<br><sub>RandyKnapp</sub> | удобнее HUD стройки |
| BetterAutoRun<br><sub>nearbear</sub> | улучшенный autorun |
| AutoMapPins<br><sub>abfielder</sub> | автопины на карте |

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
- Не находится в Online → смотри автора под именем; иногда несколько пакетов с похожим названием.
