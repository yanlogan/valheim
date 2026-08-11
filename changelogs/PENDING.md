# Friends cycle — PENDING

Status: **open** · started: **2026-08-09**  
Стек: [`docs/STACK.md`](../docs/STACK.md) · Геймплей: [`docs/HOWTO.md`](../docs/HOWTO.md)

## Ссылки

- **Full notes (этот файл):** https://github.com/yanlogan/valheim/blob/main/changelogs/PENDING.md
- **Release:** https://github.com/yanlogan/valheim/releases/tag/cycle-2026-08-11
- **Yanlo zip:** https://github.com/yanlogan/valheim/releases/download/cycle-2026-08-11/YanloMods-cycle-2026-08-11.zip
- **Стек / HOWTO:** https://github.com/yanlogan/valheim/blob/main/docs/STACK.md · [HOWTO](https://github.com/yanlogan/valheim/blob/main/docs/HOWTO.md)

> Baseline Release уже есть; цикл **open** до «синк с друзьями». Полный онбординг — в STACK/HOWTO, здесь только дельта.

---

## What's Changed

### Added

- Базовый стек сервера (см. [STACK](../docs/STACK.md)): инвентарь/крафт (AzuEPI, QSS Sort+Trash, SC, Drawers, CraftyBoxes **1.8.15**, V+, WardIsLove **3.7.2**, MUC, Recycle, ConfigManager, ImpactfulSkills, PlanBuild **0.18.4**, Cartography) + полный контент-набор
- **Yanlo-ChestUnloadButton** 1.3.0, **Yanlo-ShipExplorationAll** 1.1.0 (zip с Release)
- WardIsLove: свой Thorward на доме; на сервере `Ward Control=true`

### Removed

- GemHunter ShipExploration → Yanlo
- NoBuildRestriction
- Yanlo-QSSSortButtonOffset
- Не ставить: AzuAutoStore, TrashItems (см. Удалить в STACK)

### Config

- CraftyBoxes `Container Range=50`; V+ `CraftFromChest=false` + `StructuralIntegrity=true`
- SC `range=14` + Unload groups; QSS Sort+Trash; **`UseTopDownLogicForEverything=true` на каждом клиенте**

### Yanlo

- ChestUnloadButton **1.3.0** / ShipExplorationAll **1.1.0** — [скачать zip](https://github.com/yanlogan/valheim/releases/download/cycle-2026-08-11/YanloMods-cycle-2026-08-11.zip)

### UX

Новое для друзей в этом цикле — см. [HOWTO](../docs/HOWTO.md) (Sort / Unload / Wards / PlanBuild / Cartography / …).

---

## Закрытие цикла

1. Этот файл → `changelogs/YYYY-MM-DD_slug.md` (обновить Ссылки на blob архива + tag).
2. `.\scripts\release.ps1 -Tag cycle-YYYY-MM-DD -NotesFile …` (если нужен новый zip/notes).
3. Новый PENDING из `_PENDING_TEMPLATE.md`.
