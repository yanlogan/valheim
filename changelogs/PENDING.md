# Friends cycle — PENDING

Status: **open** · started: **2026-08-09**  
Стек: [`docs/STACK.md`](../docs/STACK.md) · Геймплей: [`docs/HOWTO.md`](../docs/HOWTO.md) · Конфиги: [`cfg/`](../cfg/)

## Ссылки

- **Дельта (этот файл):** https://github.com/yanlogan/valheim/blob/main/changelogs/PENDING.md
- **Release + Yanlo zip:** https://github.com/yanlogan/valheim/releases/tag/cycle-2026-08-11
- **Стек / HOWTO / cfg:** [STACK](https://github.com/yanlogan/valheim/blob/main/docs/STACK.md) · [HOWTO](https://github.com/yanlogan/valheim/blob/main/docs/HOWTO.md) · [cfg/](https://github.com/yanlogan/valheim/tree/main/cfg)

> Baseline zip уже на Release; цикл **open**, пока не все друзья забрали апдейт. Описания «что делает» — в STACK; здесь имена дельты.

---

## What's Changed

### Added

**Обязательно — инвентарь / крафт / wards** (детали в [STACK](../docs/STACK.md#инвентарь)):

- AzuCraftyBoxes, AzuExtendedPlayerInventory (AzuEPI)
- Better_Cartography_Table, ImpactfulSkills, ItemDrawers
- MultiUserChest, Official_BepInEx_ConfigurationManager
- PlanBuild, Quick_Stack_Store_Sort_Trash_Restock (QSS)
- Recycle_N_Reclaim, SmarterContainers
- ValheimPlus_Grantapher_Temporary, WardIsLove

**Обязательно — контент / мир** (полный список в [STACK](../docs/STACK.md#контент)):

- Atos_Arrows_JVL, BetterArchery, BoneAppetit, Clutter, HoneyPlus
- InfinityTools, InstantMonsterLootDrop, MoreGatesExtended
- OdinCampsite, OdinHorse, OdinsHorsePen, OdinShip
- PlantEverything, PlantIt, Seasonality, TreesReborn, Valharvest
- Venture_Terrain_Reset, XPortal

**Yanlo** ([zip](https://github.com/yanlogan/valheim/releases/download/cycle-2026-08-11/YanloMods-cycle-2026-08-11.zip)):

- ChestUnloadButton **1.3.0**, ShipExplorationAll **1.1.0**
- WardIsLove: свой Thorward на доме; на сервере `Ward Control=true`

**По желанию** (в этом цикле; [STACK](../docs/STACK.md#по-желанию)):

- AAA_Crafting, ItemCompare, VNEI, CraftGuard
- BetterSounds (male) + CustomAudio.zip, Expand_World_Music + Forteca_Soundtrack
- ImprovedBuildHud, MyLittleUI, PlantEasily, QuickTeleport, FenceSnap
- AutoMapPins, BetterAutoRun, Willybachs_HD_Seasonality

### Removed

- GemHunter ShipExploration → Yanlo
- NoBuildRestriction
- Yanlo-QSSSortButtonOffset
- Не ставить: AzuAutoStore, TrashItems (см. Удалить в STACK)

### Config

Ключи / готовые файлы — [STACK → Конфиги](../docs/STACK.md#конфиги) и [`cfg/`](../cfg/) (QSS, SC, CraftyBoxes, V+).  
Коротко: CraftyBoxes 50 м · SC Unload ~14 м · V+ CraftFromChest off · QSS Sort+Trash + `UseTopDownLogicForEverything=true` (у каждого клиента).

### Yanlo

- ChestUnloadButton **1.3.0** / ShipExplorationAll **1.1.0** — [скачать zip](https://github.com/yanlogan/valheim/releases/download/cycle-2026-08-11/YanloMods-cycle-2026-08-11.zip) → `plugins/Yanlo-*`

### UX

См. [HOWTO](../docs/HOWTO.md) (Sort / Unload / Wards / PlanBuild / Cartography / AAA tracker / …).

---

## Закрытие цикла

Только когда все друзья забрали апдейт («синк с друзьями»):

1. Этот файл → `changelogs/YYYY-MM-DD_slug.md`
2. `.\scripts\release.ps1` при новом zip/notes (или `gh release edit` для правок notes)
3. Новый PENDING из `_PENDING_TEMPLATE.md`
