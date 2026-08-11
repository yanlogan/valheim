# Стек модов (чеклист)

Last updated: 2026-08-11  
Менеджер: **r2modman** → Valheim  
Yanlo zip: [Latest Release](https://github.com/yanlogan/valheim/releases/latest)  
Геймплей: [HOWTO.md](HOWTO.md)

В таблицах: **Имя** — что вбить в Online → поиск (copy-paste); под ним мелким шрифтом — **автор**. Внутри каждой таблицы — **по алфавиту**. Версии — только где важно совпасть с хостом. Подробнее про кнопки/хоткеи — [HOWTO](HOWTO.md).

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
| AzuAutoStore<br><sub>Azumatt</sub> | Сам раскидывает лут из инвентаря по подходящим сундукам рядом | Вместе с ItemDrawers Take Stack может **съедать остаток** стака — у нас не используем |
| ConditionalConfigSync<br><sub>shudnal</sub> | Условная синхронизация конфигов между клиентом и сервером | Сирота в нашем паке: никто на него не ссылается, пользы нет |
| NoBuildRestriction<br><sub>BlackViking</sub> | Снимает ванильные лимиты стройки / зоны | Не нужен: играем с обычными лимитами |
| ShipExploration<br><sub>GemHunter1</sub> | Увеличивает радиус открытия карты, пока ты на корабле | Заменён нашим **Yanlo-ShipExplorationAll** (ваниль + все OdinShip); два мода вместе не ставить |
| TimedTorchesStayLit<br><sub>TastyChickenLegs</sub> | Факелы/костры на сервере не прогорают по таймеру | Живёт **только на dedicated**; на клиенте лишний |
| TrashItems<br><sub>virtuaCode</sub> | Отдельный мод с корзиной / удалением предметов | Trash уже есть в **QSS**; второй Trash только путает |

---

## ❗ Обязательно — зависимости

| Имя | Что делает |
|-----|------------|
| BepInExPack_Valheim<br><sub>denikson</sub> | Базовый загрузчик модов. Без него ничего из списка ниже не поднимется; в r2modman обычно уже стоит с профилем |
| HookGenPatcher<br><sub>ValheimModding</sub> | При старте игры генерирует/обновляет MMHOOK — хуки, на которых сидят многие моды |
| JsonDotNET<br><sub>ValheimModding</sub> | Библиотека JSON; нужна части модов как зависимость, сама по себе ничего в геймплее не меняет |
| Jotunn<br><sub>ValheimModding</sub> | Фреймворк для контент-модов (предметы, куски, рецепты). Нужен PlanBuild, ImpactfulSkills, BoneAppetit и куче других |
| MMHOOK<br><sub>—</sub> | Сгенерированные Harmony-хуки. Часто появляется сам после HookGenPatcher; вручную обычно не ищут |
| YamlDotNet<br><sub>ValheimModding</sub> | Библиотека YAML (конфиги/данные модов). Сама UI не даёт |

---

## ❗ Обязательно — инвентарь / крафт / wards

| Имя | Что делает |
|-----|------------|
| AzuCraftyBoxes<br><sub>Azumatt</sub> | Крафт и стройка тянут ресурсы из **соседних сундуков** (у нас до ~50 м); в HUD видно «сколько есть с учётом сундуков». Без мода той же версии сервер **кикает**. Версия **1.8.15**. V+ CraftFromChest у нас выкл — иначе double-consume |
| AzuExtendedPlayerInventory<br><sub>Azumatt</sub> | Дополнительные ряды инвентаря и слоты экипа/быстрого доступа. Число рядов стыкуется с ValheimPlus |
| Better_Cartography_Table<br><sub>nbusseneau</sub> | Нормальный шаринг **пинов и исследованной карты** через картографический стол. Пины по умолчанию private; public — Shift+клик у стола (см. HOWTO) |
| ImpactfulSkills<br><sub>MidnightMods</sub> | Скиллы реально бустят дроп/удобство (рубка, майнинг, фарм, оружие, sneak/run…). Новые скиллы: **Voyager**, **Hauling**, **Animal Whisper**. Нужен у всех + на сервере |
| ItemDrawers<br><sub>makail</sub> | Настенный ящик под **один тип** предмета, до **9999** шт. Interact / Alt / Shift — забрать стак, 1 шт. или закинуть всё того типа; с пола рядом подбирает сам |
| MultiUserChest<br><sub>MSchmoecker</sub> | Несколько игроков могут одновременно открыть один и тот же сундук без классического «занято» |
| Official_BepInEx_ConfigurationManager<br><sub>Azumatt</sub> | Окно настроек модов по **F1** (не заходя в файлы cfg). Удобно крутить свои хоткеи |
| PlanBuild<br><sub>MathiasDecrock</sub> | **Plan Hammer** — синие планы построек без ресурсов; **Plan Totem** — сдача материалов рядом; **Blueprint Rune** — копировать/вставлять здания. Версия **0.18.4**. Без мода на клиенте планы не видно; с модом на клиенте сервер без PlanBuild не пустит |
| Quick_Stack_Store_Sort_Trash_Restock<br><sub>Goldenrevolver</sub> | У нас только **Sort** (клавиша/кнопка, пакует сверху вниз) и **Trash** (корзина). Quick Stack / Restock / Store All **выключены** — иначе были пропажи лута с EPI |
| Recycle_N_Reclaim<br><sub>Azumatt</sub> | Разбор вещей у верстака обратно в материалы; на сундуке зелёная **Reclaim all** — вернуть ресурсы из содержимого сундука себе (это не Sort/Unload) |
| SmarterContainers<br><sub>Roses</sub> | Умная раскладка по сундукам (Ctrl+клик и группы) + логика **Unload**: eligible-лут в соседние релевантные сундуки в радиусе ~**14 м**. Кнопку Unload рисует Yanlo |
| ValheimPlus_Grantapher_Temporary<br><sub>Grantapher</sub> | Пачка QoL/серверных флагов. У нас важны ряды инвентаря с EPI и **StructuralIntegrity** (здания/лодки крепче). **CraftFromChest = false** — крафт из сундуков только через CraftyBoxes |
| WardIsLove<br><sub>Azumatt</sub> | Вместо ванильного ward — **Thorward** с GUI: свой радиус на дом, ACL для CraftyBoxes. Версия **3.7.2** у всех одинаковая. Свой ward на своём доме (см. Дополнительно) |

---

## ❗ Обязательно — контент / мир

Без этих модов — missing prefabs / нет кусков и предметов мира.

| Имя | Что делает |
|-----|------------|
| Atos_Arrows_JVL<br><sub>Digitalroot</sub> | Набор дополнительных типов стрел (урон/эффекты). Без мода стрелы с сервера будут «розовыми»/пропадут |
| BetterArchery<br><sub>ishid4</sub> | Улучшения лука и стрельбы (прицел, поведение лука — по настройкам мода) |
| BoneAppetit<br><sub>RockerKitten</sub> | Новые блюда, ингредиенты и станции готовки. Контент: без мода рецептов/префабов нет |
| Clutter<br><sub>plumga</sub> | Куча декоративных build-кусков (мелочёвка для базы) |
| HoneyPlus<br><sub>OhhLoz</sub> | Расширенная система мёда/ульев (больше вариантов, чем ваниль) |
| InfinityTools<br><sub>Numenos</sub> | Инструменты не ломаются (в нашем сетапе так удобнее фармить/строить) |
| InstantMonsterLootDrop<br><sub>cjayride</sub> | Лут с убитых мобов сразу падает на землю, не нужно обыскивать труп |
| MoreGatesExtended<br><sub>shudnal</sub> | Дополнительные ворота, двери и похожие проёмы для баз |
| OdinCampsite<br><sub>OdinPlus</sub> | Походный/кемп-контент (палатки и связанное из пакета OdinPlus) |
| OdinHorse<br><sub>OdinPlus</sub> | Лошади / ездовые существа OdinPlus |
| OdinsHorsePen<br><sub>OdinPlus</sub> | Загон и постройки под лошадей |
| OdinShip<br><sub>Marlthon</sub> | Новые корабли (War, Cargo, Merchant, каноэ и т.д.). Yanlo-ShipExplorationAll как раз учитывает их |
| PlantEverything<br><sub>Advize</sub> | Сильно расширяет список того, что можно сажать/выращивать (кусты, грибы, цветы и т.п.) |
| PlantIt<br><sub>OdinPlus</sub> | **Контент:** декоративные растения, ставишь специальной лопатой. Это не сетка-ферма — для фермы сеткой есть optional PlantEasily |
| Seasonality<br><sub>RustyMods</sub> | Сезоны: меняется окружение/погода по циклу. Часто в паре с HD-текстурами (optional Willybach) |
| TreesReborn<br><sub>TastyChickenLegs</sub> | Другие модели/поведение деревьев (визуал и валка отличаются от ванили) |
| Valharvest<br><sub>Frenvius</sub> | Доп. фермерский контент (культуры/связанные предметы) |
| Venture_Terrain_Reset<br><sub>VentureValheim</sub> | Инструменты сброса/правки террейна (выровнять испорченную землю и т.п.) |
| XPortal<br><sub>SpikeHimself</sub> | Именованные порталы между базами (удобный fast-travel по своим точкам) |

---

## ❗ Обязательно — Yanlo (не через Online)

Скачай zip с [Latest Release](https://github.com/yanlogan/valheim/releases/latest) → папки в `plugins/`:

| Имя (папка) | Версия | Что делает |
|-------------|--------|------------|
| Yanlo-ChestUnloadButton<br><sub>Yanlo</sub> | **1.3.0** | Рисует кнопку **Unload под** Take All (чтобы не перекрывать длинные имена сундуков). После раскладки SC то, что не нашло соседний сундук из eligible-групп, кидает в **открытый** сундук. Нужны QSS + SC Unload; без них папку удали |
| Yanlo-ShipExplorationAll<br><sub>Yanlo</sub> | **1.1.0** | Больший радиус тумана карты на Raft/Karve/Longship **и** на кораблях OdinShip 0.7.6. Заменяет GemHunter ShipExploration — его не ставить рядом |

---

## По желанию

| Имя | Что делает |
|-----|------------|
| AAA_Crafting<br><sub>Azumatt</sub> | Удобнее окно крафта + **Recipe Tracker** (пин рецепта на экран: сколько материалов не хватает). Хост: компактная панель, toggle часто PageUp |
| AutoMapPins<br><sub>abfielder</sub> | Сам ставит пины на карте по интересным местам/ресурсам (меньше ручной разметки) |
| BetterAutoRun<br><sub>nearbear</sub> | Удобнее autorun: меньше случайных сбросов бега, чем в ванили |
| BetterSounds<br><sub>Wiandar</sub> | Замена SFX/амбиента (**male**-пакет). После Install нужно распаковать `CustomAudio.zip` в папку мода. Не ставить вместе с Female |
| CraftGuard<br><sub>jg224</sub> | В молотке режимы Default / **Mod View** — куски модов по категориям. Организацию станций крафта у нас выкл (`OrganizeRecipes=false`), чтобы не драться с AAA |
| Expand_World_Music<br><sub>JereKuusela</sub> | Движок кастомной музыки (меню/мир). Нужен, если ставишь Forteca |
| FenceSnap<br><sub>MSchmoecker</sub> | Заборы и похожие куски «прилипают» ровно при стройке |
| Forteca_Soundtrack<br><sub>BlackViking</sub> | Треки Forteca в главном меню через Expand World Music |
| ImprovedBuildHud<br><sub>RandyKnapp</sub> | Понятнее HUD при стройке (что можно поставить / требования) |
| ItemCompare<br><sub>Azumatt</sub> | В UI сравнения видно статы двух предметов рядом |
| MyLittleUI<br><sub>shudnal</sub> | Набор мелких UI-удобств (читаемость/компактность интерфейса) |
| PlantEasily<br><sub>Advize</sub> | **QoL фермы:** сажать сеткой (ряды/столбцы), snap, массовый сбор, опционально auto-replant. Работает с ванилью и PlantEverything. Не добавляет новые префабы — для декора лопатой см. PlantIt |
| QuickTeleport<br><sub>OdinPlus</sub> | Быстрый телепорт по хоткею на сохранённые точки (client-only) |
| VNEI<br><sub>MSchmoecker</sub> | Отдельное окно: поиск предметов, рецептов, где крафтится / что дропает |
| Willybachs_HD_Seasonality<br><sub>Willybach</sub> | HD-текстуры под Seasonality (красивее сезоны). Тяжёлые ассеты, только клиент |

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
