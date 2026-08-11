# Friends cycle — PENDING

Status: **open** · started: **2026-08-09**  
Профиль r2modman: **`Valheim_Client`**  
Discord: [`PENDING_DISCORD.md`](PENDING_DISCORD.md) · стек: [`CLIENT_STACK.md`](../CLIENT_STACK.md)

## Ссылки

Заполняются при `release.ps1` / закрытии цикла (пока open — канон = этот файл):

- **Full notes:** https://github.com/yanlogan/valheim/blob/master/changelogs/PENDING.md
- **Release:** _(после синка)_ https://github.com/yanlogan/valheim/releases/latest
- **Yanlo zip:** asset `YanloMods-cycle-….zip` на Release
- **Client stack:** https://github.com/yanlogan/valheim/blob/master/CLIENT_STACK.md

---

## What's Changed

### Added

- Стек инвентаря/крафта как у хоста (AzuEPI, QSS Sort+Trash, SmarterContainers, ItemDrawers, AzuCraftyBoxes 1.8.15, ValheimPlus, WardIsLove 3.7.2, MUC, Recycle, ConfigManager, ImpactfulSkills, BoneAppetit, Better Cartography, PlanBuild 0.18.4 + контент-моды)
- **Yanlo-ChestUnloadButton** 1.3.0 — Unload под Take All + leftovers → открытый сундук
- **Yanlo-ShipExplorationAll** 1.1.0 — радиус карты ваниль + OdinShip
- WardIsLove: свой Thorward на доме; на сервере `Ward Control=true`

### Removed

- GemHunter ShipExploration (→ Yanlo)
- NoBuildRestriction
- устаревший Yanlo-QSSSortButtonOffset
- (друзья никогда не ставили) AzuAutoStore / TrashItems — не ставить

### Config

- CraftyBoxes `Container Range=50`; V+ `CraftFromChest=false`; SC `range=14` + Unload groups
- QSS: Sort+Trash only; `UseTopDownLogicForEverything=true` (**на каждом клиенте**)
- V+ `StructuralIntegrity enabled=true`

### Yanlo

- ChestUnloadButton **1.3.0** / ShipExplorationAll **1.1.0** — zip с Release (после публикации цикла)

---

## Важные изменения (детали)

### Стек инвентаря / крафта (как у хоста)

Нужны у всех + на dedicated (где указано):

| Мод | Зачем |
|-----|--------|
| **AzuEPI** | доп. ряды / экип |
| **QSS** (`Quick_Stack_Store_Sort_Trash_Restock`) | только **Sort + Trash** |
| **SmarterContainers** | умная раскладка + Unload |
| **makail-ItemDrawers** | настенные ящики |
| **Azumatt-AzuCraftyBoxes** **1.8.15** | крафт/стройка + HUD из сундуков; без мода сервер кикает |
| **ValheimPlus** | ряды с EPI / StructuralIntegrity; **CraftFromChest выкл** |
| **WardIsLove** **3.7.2** | Thorward + радиус на дом (GUI) |
| **MultiUserChest** | несколько игроков в одном сундуке |
| **Recycle_N_Reclaim** | разбор у верстака |
| **ConfigurationManager** | F1 |
| **ImpactfulSkills** | бонусы + Voyager / Hauling / Animal Whisper |
| **BoneAppetit** | еда/рецепты |
| **Better_Cartography_Table** | шаринг пинов у стола |
| **PlanBuild** **0.18.4** | Plan Hammer / Blueprint Rune / Plan Totem |
| Контент как у хоста | PlantEverything, Seasonality, OdinShip, … |

### WardIsLove

- Версия **одна** у всех + dedicated.
- На сервере: **`Ward Control = true`** — limited GUI владельцу (ServerSync; перезайди).
- Сломать host Thorward у своего дома → поставить **свой** → **Ward Range** = число со знака.

### Радиусы сундуков

```ini
; Azumatt.AzuCraftyBoxes.cfg
[2 - CraftyBoxes]
Container Range = 50

; valheim_plus.cfg
[CraftFromChest]
enabled = false

; flueno.SmartContainers.cfg
[General]
range = 14
```

### Yanlo (zip с Release, не Thunderstore)

| Пакет | Версия | Действие |
|-------|--------|----------|
| **Yanlo-ChestUnloadButton** | **1.3.0** | Папка в `plugins/`. Unload **под** Take All. Cfg: `Placement=Below`. Старый QSSSortButtonOffset — удалить. |
| **Yanlo-ShipExplorationAll** | **1.1.0** | Папка в `plugins/`. Не ставить GemHunter. |
| **GemHunter1-ShipExploration** | — | Удалить. |

### QSS / SC / V+

```ini
; QSS — Sort+Trash only
UseTopDownLogicForEverything = true
DisplayQuickStackButtons = Disabled
HideBaseGamePlaceStacksButton = true
DisplayRestockButtons = Disabled
DisplayStoreAllButton = false
NeverMoveTakeAllButton = true
DisplaySortButtons = Both
DisplayTrashCanUI = true
AutoSort = Never

; SC Unload
[Unload]
enabled = true
nativeButton = false
consumableFiltering = false
groupsList = valuables,ore,wood,mushrooms,berries,vegetables
```

`UseTopDownLogicForEverything` **не** с сервера — у каждого клиента `true`.  
V+ `[StructuralIntegrity] enabled = true`.

### Прочее

- Удалить **NoBuildRestriction**.
- Лог `Failed to deserialize Azumatt.AzuCraftyBoxes.yml` → удали `.yml` из config (не `.cfg`).

---

## По желанию (QoL)

AAA_Crafting, ItemCompare, VNEI, BetterSounds (male)+CustomAudio.zip, EWM+Forteca, CraftGuard (`OrganizeRecipes=false`). См. [`CLIENT_STACK.md`](../CLIENT_STACK.md).

---

## Как пользоваться

- **Sort** `O` — сверху вниз; Place Stacks скрыт.
- **Trash** — корзина / `Delete`.
- **Unload** — под Take All; ~14 м + leftovers eligible → открытый сундук. Dump-сундук **>14 м** от домов.
- **ItemDrawers** — `E` / `Alt+E` / `Shift+E`; пол рядом подбирает.
- **Cartography** — public: у стола `LeftShift`+клик.
- **AAA tracker** — клик иконки добавить; Ctrl убрать; `PageUp` toggle.
- **CraftyBoxes** ~50 м; пауза `O+LeftAlt`.
- **WardIsLove** — свой Thorward, Range со знака.
- **PlanBuild** — Plan Hammer / Totem / Blueprint Rune; undo `bp.undo`.

---

## Закрытие цикла

1. Этот файл → `changelogs/YYYY-MM-DD_slug.md` (обновить блок **Ссылки** на blob + tag).
2. `.\scripts\release.ps1 -Tag cycle-YYYY-MM-DD -NotesFile .\changelogs\YYYY-MM-DD_slug.md`
3. Новый пустой PENDING + Discord; в Discord — ссылки на Release и полный MD.
