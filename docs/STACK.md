# Стек модов (чеклист)

Last updated: 2026-08-11  
Менеджер: **r2modman** → Valheim  
Yanlo zip: [Latest Release](https://github.com/yanlogan/valheim/releases/latest)  
Геймплей: [HOWTO.md](HOWTO.md)

В таблицах: **Имя** — что вбить в Online → поиск (copy-paste); под ним мелким шрифтом — **автор**. Внутри каждой таблицы — **по алфавиту**. Версии — только где важно совпасть с хостом. Подробнее про кнопки/хоткеи — [Как играть (HOWTO)](HOWTO.md).

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
<a id="удалить"></a>

| Имя | Что делает | Почему убрать |
|-----|------------|---------------|
| AzuAutoStore<br><sub>Azumatt</sub> | Автоскладирование лута из инвентаря в соседние сундуки | С ItemDrawers может **съедать остаток** стака |
| ConditionalConfigSync<br><sub>shudnal</sub> | Условный sync конфигов клиент↔сервер | Сирота, не используется |
| NoBuildRestriction<br><sub>BlackViking</sub> | Снимает ванильные лимиты стройки | Не используем |
| ShipExploration<br><sub>GemHunter1</sub> | Больший радиус карты на корабле | Заменён [Yanlo-ShipExplorationAll](#yanlo); вместе не ставить |
| TimedTorchesStayLit<br><sub>TastyChickenLegs</sub> | Факелы/костры не прогорают по таймеру | Только на **dedicated** |
| TrashItems<br><sub>virtuaCode</sub> | Отдельный Trash / удаление предметов | Trash уже в **QSS** |

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
| AzuCraftyBoxes<br><sub>Azumatt</sub> | Крафт/стройка и счётчики HUD из соседних сундуков (~50 м). Версия **1.8.15**; без мода сервер кикает. V+ CraftFromChest выкл |
| AzuExtendedPlayerInventory<br><sub>Azumatt</sub> | Доп. ряды инвентаря и слоты экипа (стыкуется с ValheimPlus) |
| Better_Cartography_Table<br><sub>nbusseneau</sub> | Шаринг пинов и эксплора через картографический стол; public — Shift+клик у стола ([HOWTO](HOWTO.md#карта-и-корабли)) |
| ImpactfulSkills<br><sub>MidnightMods</sub> | Бонусы от скиллов + **Voyager** / **Hauling** / **Animal Whisper**. Нужен у всех + на сервере |
| ItemDrawers<br><sub>makail</sub> | Настенный ящик на 1 тип, до **9999**; E / Alt+E / Shift+E, подбор с пола ([HOWTO](HOWTO.md#инвентарь-и-сундуки)) |
| MultiUserChest<br><sub>MSchmoecker</sub> | Несколько игроков открывают один сундук одновременно |
| Official_BepInEx_ConfigurationManager<br><sub>Azumatt</sub> | Настройки модов по **F1** |
| PlanBuild<br><sub>MathiasDecrock</sub> | Plan Hammer / Plan Totem / Blueprint Rune — планы и копирование построек. Версия **0.18.4** ([HOWTO](HOWTO.md#крафт-и-стройка)) |
| Quick_Stack_Store_Sort_Trash_Restock<br><sub>Goldenrevolver</sub> | Только **Sort** (сверху вниз) и **Trash**; stack/restock/store-all выкл ([HOWTO](HOWTO.md#инвентарь-и-сундуки), [Конфиги](#конфиги)) |
| Recycle_N_Reclaim<br><sub>Azumatt</sub> | Разбор у верстака; **Reclaim all** на сундуке возвращает материалы в игрока |
| SmarterContainers<br><sub>Roses</sub> | Умная раскладка + Unload в соседние сундуки (~14 м). Кнопка Unload — [Yanlo-ChestUnloadButton](#yanlo) |
| ValheimPlus_Grantapher_Temporary<br><sub>Grantapher</sub> | Ряды инвентаря с EPI + **StructuralIntegrity**. **CraftFromChest = false** (крафт из сундуков через CraftyBoxes) |
| WardIsLove<br><sub>Azumatt</sub> | **Thorward** + радиус на дом в GUI; версия **3.7.2**. Свой ward на доме ([Дополнительно](#дополнительно), [HOWTO](HOWTO.md#крафт-и-стройка)) |

---

## ❗ Обязательно — контент / мир
<a id="контент"></a>

Без этих модов — missing prefabs / нет кусков и предметов мира.

| Имя | Что делает |
|-----|------------|
| Atos_Arrows_JVL<br><sub>Digitalroot</sub> | Доп. типы стрел; без мода — missing prefabs |
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
| TreesReborn<br><sub>TastyChickenLegs</sub> | Другие модели деревьев |
| Valharvest<br><sub>Frenvius</sub> | Доп. фермерский контент |
| Venture_Terrain_Reset<br><sub>VentureValheim</sub> | Сброс/правка террейна |
| XPortal<br><sub>SpikeHimself</sub> | Именованные порталы между базами |

---

## ❗ Обязательно — Yanlo (не через Online)
<a id="yanlo"></a>

Скачай zip с [Latest Release](https://github.com/yanlogan/valheim/releases/latest) → папки в `plugins/`:

| Имя (папка) | Версия | Что делает |
|-------------|--------|------------|
| Yanlo-ChestUnloadButton<br><sub>Yanlo</sub> | **1.3.0** | Кнопка **Unload под** Take All; leftovers eligible → открытый сундук. Нужны [QSS](#инвентарь) + [SC Unload](#инвентарь) ([HOWTO](HOWTO.md#инвентарь-и-сундуки)) |
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
| CraftGuard<br><sub>jg224</sub> | Hammer Default / Mod View; `OrganizeRecipes=false` с AAA |
| Expand_World_Music<br><sub>JereKuusela</sub> | Движок кастомной музыки; нужен для Forteca |
| FenceSnap<br><sub>MSchmoecker</sub> | Snap заборов при стройке |
| Forteca_Soundtrack<br><sub>BlackViking</sub> | Треки Forteca в меню (через EWM) |
| ImprovedBuildHud<br><sub>RandyKnapp</sub> | Удобнее HUD стройки |
| ItemCompare<br><sub>Azumatt</sub> | Сравнение статов двух предметов |
| MyLittleUI<br><sub>shudnal</sub> | Мелкие UI-удобства |
| PlantEasily<br><sub>Advize</sub> | Посадка/сбор сеткой, snap, auto-replant. Декор лопатой — [PlantIt](#контент) |
| QuickTeleport<br><sub>OdinPlus</sub> | Телепорт по хоткею на сохранённые точки |
| VNEI<br><sub>MSchmoecker</sub> | Поиск предметов и рецептов |
| Willybachs_HD_Seasonality<br><sub>Willybach</sub> | HD-текстуры для Seasonality |

---

## Конфиги
<a id="конфиги"></a>

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
<a id="дополнительно"></a>

- **WardIsLove:** свой Thorward на доме, Ward Range = число со знака; после рестарта сервера просто перезайди.
- Не находится в Online → смотри автора под именем; иногда несколько пакетов с похожим названием.
