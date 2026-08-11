# cycle-2026-08-11

Baseline Yanlo zip уже здесь. Цикл **ещё open** (не все друзья забрали) — живая дельта: [PENDING](https://github.com/yanlogan/valheim/blob/main/changelogs/PENDING.md).

**Онбординг (канон):**
- [Стек модов](https://github.com/yanlogan/valheim/blob/main/docs/STACK.md)
- [Как играть](https://github.com/yanlogan/valheim/blob/main/docs/HOWTO.md)
- [Готовые cfg](https://github.com/yanlogan/valheim/tree/main/cfg) · или ключи в [STACK → Конфиги](https://github.com/yanlogan/valheim/blob/main/docs/STACK.md#конфиги)

**Yanlo:** распакуй `YanloMods-cycle-2026-08-11.zip` → `BepInEx/plugins/` (папки `Yanlo-*`).

## What's Changed

### Added
- Базовый стек сервера — см. STACK (инвентарь/крафт + контент)
- Yanlo-ChestUnloadButton 1.3.0, Yanlo-ShipExplorationAll 1.1.0
- WardIsLove: свой Thorward на доме (на сервере `Ward Control=true`)

### Removed
- GemHunter ShipExploration → Yanlo
- NoBuildRestriction
- Yanlo-QSSSortButtonOffset
- Не ставить: AzuAutoStore, TrashItems (см. Удалить в STACK)

### Config
Не копируй ini отсюда. Бери [`cfg/`](https://github.com/yanlogan/valheim/tree/main/cfg) целиком или правь ключи в STACK → Конфиги.  
Коротко: CraftyBoxes ~50 м · SC Unload ~14 м · V+ CraftFromChest off · QSS Sort+Trash + `UseTopDownLogicForEverything=true` на каждом клиенте.

### UX
[HOWTO](https://github.com/yanlogan/valheim/blob/main/docs/HOWTO.md)
