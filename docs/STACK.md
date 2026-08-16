# Стек модов (чеклист)

Last updated: 2026-08-13  
Менеджер: **r2modman** → Valheim  
Yanlo zip: [Latest Release](https://github.com/yanlogan/valheim/releases/latest)  
Геймплей: [HOWTO.md](HOWTO.md)

В таблицах: **Имя** — что вбить в Online → поиск (copy-paste); под ним мелким шрифтом — **автор**. Внутри каждой таблицы — **по алфавиту**. Версии — только где важно совпасть с хостом. Подробнее про кнопки/хоткеи — [Как играть (HOWTO)](HOWTO.md).

---

## Как поставить

1. Online → вставь **Имя** из таблицы → Install → Enable.
2. Пройди **Удалить** (Disable / Uninstall, если стоит).
3. Распакуй `YanloMods-….zip` из Release в `BepInEx/plugins/` (папки `Yanlo-*`).
4. [Конфиги](#конфиги): ключи вручную **или** готовые файлы из [`cfg/`](../cfg/).
5. **По желанию** — что нужно из списка ниже.

Практично: тот же enabled-список, что у хоста, минус личное из «По желанию».

---

## Удалить
<a id="удалить"></a>

| Имя | Что делает | Почему убрать |
|-----|------------|---------------|
| AzuAutoStore<br><sub>Azumatt</sub> | Автоскладирование лута из инвентаря в соседние сундуки | С ItemDrawers может **съедать остаток** стака |
| Asocial_Cartography<br><sub>VentureValheim</sub> | Старый шаринг карты | Заменён [Better_Cartography_Table](#инвентарь); Disable/Uninstall |
| ConditionalConfigSync<br><sub>shudnal</sub> | Условный sync конфигов клиент↔сервер | Сирота, не используется |
| NoBuildRestriction<br><sub>BlackViking</sub> | Снимает ванильные лимиты стройки | Не используем |
| ShipExploration<br><sub>GemHunter1</sub> | Больший радиус карты на корабле | Заменён [Yanlo-ShipExplorationAll](#yanlo); вместе не ставить |
| TimedTorchesStayLit<br><sub>TastyChickenLegs</sub> | Факелы/костры не прогорают по таймеру | Только на **dedicated**; с клиента убрать |
| Seasonality_Fix<br><sub>H4nz0</sub> | Фикс сезонов | Только на **dedicated**; с клиента убрать |
| TrashItems<br><sub>virtuaCode</sub> | Отдельный Trash / удаление предметов | Trash уже в **QSS** |
| Venture_Floating_Items<br><sub>VentureValheim</sub> | Выборочный float лута в воде | У нас выкл; V+ `itemsFloatInWater=false` |

---

## ❗ Обязательно — зависимости
<a id="зависимости"></a>

| Имя | Что делает |
|-----|------------|
| BepInExPack_Valheim<br><sub>denikson</sub> | Загрузчик модов; без него остальное не работает (обычно уже есть в r2modman) |
| HookGenPatcher<br><sub>ValheimModding</sub> | Генерирует/обновляет MMHOOK при старте |
| JsonDotNET<br><sub>ValheimModding</sub> | JSON-библиотека для других модов |
| Jotunn<br><sub>ValheimModding</sub> | Фреймворк контент-модов (предметы, куски, рецепты) |
| MMHOOK<br><sub>—</sub> | Harmony-хуки; часто появляется сам после HookGenPatcher |
| YamlDotNet<br><sub>ValheimModding</sub> | YAML-библиотека для других модов |

---

## ❗ Обязательно — инвентарь / крафт / wards
<a id="инвентарь"></a>

| Имя | Что делает |
|-----|------------|
| AzuCraftyBoxes<br><sub>Azumatt</sub> | Крафт/стройка и счётчики HUD из соседних сундуков (~50 м). V+ CraftFromChest выкл |
| AzuExtendedPlayerInventory<br><sub>Azumatt</sub> | Доп. ряды инвентаря и слоты экипа |
| Better_Cartography_Table<br><sub>nbusseneau</sub> | Шаринг пинов и эксплора через картографический стол; public — Shift+клик у стола ([HOWTO](HOWTO.md#карта-и-корабли)) |
| ImpactfulSkills **0.12.0**<br><sub>MidnightMods</sub> | Бонусы от скиллов + **Voyager** / **Hauling** / **Animal Whisper** |
| ItemDrawers<br><sub>makail</sub> | Настенный ящик на 1 тип, до **9999**; E / Alt+E / Shift+E, подбор с пола ([HOWTO](HOWTO.md#инвентарь-и-сундуки)) |
| MultiUserChest<br><sub>MSchmoecker</sub> | Несколько игроков открывают один сундук одновременно |
| Official_BepInEx_ConfigurationManager<br><sub>Azumatt</sub> | Настройки модов по **F1** |
| PlanBuild<br><sub>MathiasDecrock</sub> | Plan Hammer / Plan Totem / Blueprint Rune — планы и копирование построек ([HOWTO](HOWTO.md#крафт-и-стройка)) |
| Quick_Stack_Store_Sort_Trash_Restock<br><sub>Goldenrevolver</sub> | Только **Sort** (сверху вниз) и **Trash**; stack/restock/store-all выкл ([HOWTO](HOWTO.md#инвентарь-и-сундуки), [Конфиги](#конфиги)) |
| Recycle_N_Reclaim<br><sub>Azumatt</sub> | Разбор у верстака; **Reclaim all** на сундуке возвращает материалы в игрока |
| SmarterContainers<br><sub>Roses</sub> | Умная раскладка + Unload в соседние сундуки (~14 м). Кнопка Unload — [Yanlo-ChestUnloadButton](#yanlo) |
| ValheimPlus_Grantapher_Temporary<br><sub>Grantapher</sub> | Большой QoL-пак: доп. настройки инвентаря, зданий, лодок и куча мелких правок ванили (наш профиль — в [Конфиги](#конфиги)) |
| WardIsLove<br><sub>Azumatt</sub> | **Thorward** + радиус на дом в GUI. Свой ward на доме ([Дополнительно](#дополнительно), [HOWTO](HOWTO.md#крафт-и-стройка)) |

---

## ❗ Обязательно — контент / мир
<a id="контент"></a>

Без этих модов — missing prefabs / нет кусков и предметов мира.

| Имя | Что делает |
|-----|------------|
| Atos_Arrows_JVL<br><sub>Digitalroot</sub> | Доп. типы стрел |
| BetterArchery<br><sub>ishid4</sub> | Улучшения лука и стрельбы |
| BoneAppetit<br><sub>RockerKitten</sub> | Новые блюда, ингредиенты и станции готовки |
| Clutter<br><sub>plumga</sub> | Декоративные build-куски |
| HoneyPlus<br><sub>OhhLoz</sub> | Расширенный мёд и ульи |
| InfinityTools<br><sub>Numenos</sub> | Инструменты не ломаются |
| InstantMonsterLootDrop<br><sub>cjayride</sub> | Лут с мобов сразу на землю |
| MoreGatesExtended<br><sub>shudnal</sub> | Доп. ворота и двери |
| OdinCampsite<br><sub>OdinPlus</sub> | Кемп / походные постройки |
| OdinHorse<br><sub>OdinPlus</sub> | Лошади / ездовые |
| OdinsHorsePen<br><sub>OdinPlus</sub> | Загон для лошадей |
| OdinShip<br><sub>Marlthon</sub> | Доп. корабли (War, Cargo, Merchant, каноэ…). Учитывает [Yanlo-ShipExplorationAll](#yanlo) |
| PlantEverything<br><sub>Advize</sub> | Больше растений/кустов/грибов для выращивания |
| PlantIt<br><sub>OdinPlus</sub> | Декор-растения лопатой (не сетка-ферма; сетка — [PlantEasily](#по-желанию)) |
| Seasonality<br><sub>RustyMods</sub> | Сезоны (окружение/погода); HD — optional Willybach |
| TreesReborn<br><sub>TastyChickenLegs</sub> | Срубленные деревья отрастают заново |
| Valharvest<br><sub>Frenvius</sub> | Новые овощи и рецепты |
| Venture_Terrain_Reset<br><sub>VentureValheim</sub> | Сброс/правка террейна |
| XPortal<br><sub>SpikeHimself</sub> | Один портал с выпадающим меню вместо кучи отдельных порталов |

---

## ❗ Обязательно — Yanlo (не через Online)
<a id="yanlo"></a>

Скачай zip с [Latest Release](https://github.com/yanlogan/valheim/releases/latest) → папки в `plugins/`:

| Имя (папка) | Версия | Что делает |
|-------------|--------|------------|
| Yanlo-ChestUnloadButton<br><sub>Yanlo</sub> | **1.3.0** | Кнопка **Unload под** Take All; leftovers eligible → открытый сундук. Нужны [QSS](#инвентарь) + [SC Unload](#инвентарь) ([HOWTO](HOWTO.md#инвентарь-и-сундуки)) |
| Yanlo-CraftyBoxesDrawerFix<br><sub>Yanlo</sub> | **1.1.4** | Крафт видит материалы в настенных drawers (без патча часто `0/N`). Только клиент; нужен [AzuCraftyBoxes](#инвентарь) ([HOWTO](HOWTO.md#крафт-и-стройка)) |
| Yanlo-PortalWardFix<br><sub>Yanlo</sub> | **1.0.1** | Фикс WiL: портал «сквозь» / нет телепорта при ложном CheckIn + пустом INSIDE. **Нужен на клиенте** вместе с [WardIsLove](#инвентарь) ([HOWTO](HOWTO.md#крафт-и-стройка)) |
| Yanlo-ShipExplorationAll<br><sub>Yanlo</sub> | **1.1.0** | Больший радиус карты на ванили + OdinShip. Вместо GemHunter [ShipExploration](#удалить) ([HOWTO](HOWTO.md#карта-и-корабли)) |

---

## По желанию
<a id="по-желанию"></a>

| Имя | Что делает |
|-----|------------|
| AAA_Crafting<br><sub>Azumatt</sub> | Удобнее UI крафта + Recipe Tracker ([HOWTO](HOWTO.md#ui--qol-если-поставил-optional)) |
| AutoMapPins<br><sub>abfielder</sub> | Автопины на карте по ресурсам/местам |
| BetterAutoRun<br><sub>nearbear</sub> | Улучшенный autorun |
| BetterSounds<br><sub>Wiandar</sub> | Замена SFX (**male**); после Install — `CustomAudio.zip`; не вместе с Female |
| CraftGuard<br><sub>jg224</sub> | Удобная сетка кусков в молотке по назначению (`OrganizeRecipes=false` с AAA) |
| Expand_World_Music<br><sub>JereKuusela</sub> | Движок кастомной музыки; нужен для Forteca |
| FenceSnap<br><sub>MSchmoecker</sub> | Snap заборов при стройке |
| Forteca_Soundtrack<br><sub>BlackViking</sub> | Треки Forteca в меню (через EWM) |
| ImprovedBuildHud<br><sub>RandyKnapp</sub> | В требованиях куска — сколько материала у тебя и сколько раз можно построить |
| ItemCompare<br><sub>Azumatt</sub> | Сравнение статов двух предметов |
| MyLittleUI<br><sub>shudnal</sub> | Пачка UI-мелочей: таймеры станций, имена сундуков, баффы, прогноз погоды и т.п. |
| PlantEasily<br><sub>Advize</sub> | Посадка/сбор сеткой, snap, auto-replant. Декор лопатой — [PlantIt](#контент) |
| QuickTeleport<br><sub>OdinPlus</sub> | Короче время телепорта (ванильный каст быстрее) |
| VNEI<br><sub>MSchmoecker</sub> | Поиск предметов и рецептов |
| Willybachs_HD_Seasonality<br><sub>Willybach</sub> | HD-текстуры для Seasonality |

---

## Конфиги
<a id="конфиги"></a>

Путь: `%AppData%\r2modmanPlus-local\Valheim\profiles\<профиль>\BepInEx\config\`

Исправь **только эти строчки**, если тебе нужно сохранить свои остальные настройки этого мода. Если не нужно — возьми готовый файл из [`cfg/`](../cfg/) (скопируй целиком поверх своего).

Не затирай `*Keybind*` / свои бинды, если правишь вручную. `UseTopDownLogicForEverything` **не** приходит с сервера — выставь у себя (или возьми готовый QSS cfg).

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
[GameClock]
enabled = false
; itemsFloatInWater = false  (плавучесть всего лута — выкл; Venture_Floating_Items не ставить)

; Azumatt.AzuAntiArthriticCrafting.cfg  (AAA_Crafting — если ставишь)
[5 - Recipe Tracker (Position)]
Recipe Tracker Position = {"x":200.0,"y":200.0}
[5 - Recipe Tracker (Sizes)]
Recipe Tracker Panel Scale = {"x":0.5,"y":0.5}
Recipe Tracker Req Name Max = 15

; com.inventoryux.valheim.cfg  (CraftGuard — если ставишь с AAA)
[CraftingUI]
OrganizeRecipes = false
; Hammer OrganizeCrafting / OrganizeBuilding / OrganizeHeavyBuilding / OrganizeFurniture = true
```

---

## Дополнительно
<a id="дополнительно"></a>

- **WardIsLove:** свой Thorward на доме, Ward Range = число со знака (GUI «%» = шкала 0–100); после рестарта сервера просто перезайди. Портал «сквозь» → **Yanlo-PortalWardFix** на клиенте.
- Не находится в Online → смотри автора под именем; иногда несколько пакетов с похожим названием.
