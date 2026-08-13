# Как играть (шпаргалка)

Стабильная страница. Дельты цикла — в [CHANGES](CHANGES.md) / [Latest Release](https://github.com/yanlogan/valheim/releases/latest).

## Инвентарь и сундуки

- **Sort** — клавиша `O` и кнопка Sort на сундуке/инвентаре; пакует стаки **сверху вниз**. Ванильный Place Stacks скрыт.
- **Trash** — корзина в UI + `Delete` (хотбар не трогает, если cfg как у хоста).
- **Unload** — кнопка **под** Take All (`Yanlo-ChestUnloadButton`): eligible (valuables / руда / дерево / грибы / ягоды / овощи + seeds) → соседние сундуки ~**14 м**; остаток eligible → **открытый** сундук. Еду со станций не трогает. Общий dump-сундук после вылазки — **дальше 14 м от домов**.
- **ItemDrawers** — один тип предмета, до **9999**:
  - предмет с хотбара → пустой drawer (задать тип);
  - `E` — стак; `LeftAlt+E` — 1; `LeftShift+E` — все того типа из инвентаря;
  - `LeftAlt+E` при 0 — сброс типа;
  - лут на пол рядом подбирается сам (~15 м).
- **MultiUserChest** — несколько человек могут открыть один сундук.
- **Recycle / Reclaim** — разбор у верстака; зелёная **Reclaim all** на сундуке — не путать с Sort/Unload.

## Крафт и стройка

- **AzuCraftyBoxes** — крафт/стройка тянут из сундуков **и ItemDrawers** до ~**50 м**; HUD учитывает их. Пауза pull: обычно `O+LeftAlt` (свой хоткей в F1). V+ CraftFromChest у нас **выкл**. Нужен **Yanlo-CraftyBoxesDrawerFix** (из zip), иначе drawers в 1.8.15 часто `0/N`.
- **WardIsLove** — крафтится **Thorward** (ванильный ward не крафтится). У своего дома: сломай ward, который поставил хост → поставь **свой** на то же место → Interact → **Ward Range** = число на **знаке рядом**. На общую зону (плавильни) ward не ставим.
- **PlanBuild**:
  - **Plan Hammer** (1 дерево) — синие планы без ресурсов; потом материалы / **Plan Totem** / обычный Hammer.
  - **Skuld Crystal** — убрать синий шейдер.
  - **Blueprint Rune** — копирование построек (Alt/Ctrl/Shift selection; Scroll поворот). Marketplace GUI часто на **End**.
  - У обычных игроков на сервере выкл direct-build без материалов и terrain-tools. Undo: консоль `bp.undo` / `bp.redo`.
- **CraftGuard** (optional) — в молотке Default / Mod View.

## Карта и корабли

- **Better Cartography Table** — пины по умолчанию **private**. Public (шарится через стол): подойти к столу, открыть карту **со стола**, `LeftShift`+клик по своему пину.
- **Yanlo-ShipExplorationAll** — больший радиус тумана на Raft/Karve/Longship и кораблях OdinShip.

## Скиллы и еда

- **ImpactfulSkills** — бонусы от скиллов + **Voyager** / **Hauling** / **Animal Whisper** в меню персонажа.
- **BoneAppetit** — новые блюда и станции (крафт как обычная еда).

## UI / QoL (если поставил optional)

- **AAA Recipe Tracker** — клик по иконке рецепта = добавить в панель; `LeftControl`+иконка = убрать; **`PageUp`** = показать/скрыть (у хоста так; сверь F1).
- **Configuration Manager** — `F1`.
- **VNEI** / **ItemCompare** — поиск и сравнение предметов.

Подробный список модов: [STACK.md](STACK.md).
