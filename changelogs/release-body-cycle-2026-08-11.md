# cycle-2026-08-11

Baseline Yanlo zip уже здесь. Цикл **ещё open** (не все друзья забрали) — живая дельта: [PENDING](https://github.com/yanlogan/valheim/blob/main/changelogs/PENDING.md).

**Онбординг (канон):**
- [Стек модов](https://github.com/yanlogan/valheim/blob/main/docs/STACK.md)
- [Как играть](https://github.com/yanlogan/valheim/blob/main/docs/HOWTO.md)
- [Готовые cfg](https://github.com/yanlogan/valheim/tree/main/cfg) · или ключи в [STACK → Конфиги](https://github.com/yanlogan/valheim/blob/main/docs/STACK.md#конфиги)

**Yanlo:** распакуй `YanloMods-cycle-2026-08-11.zip` → `BepInEx/plugins/` (папки `Yanlo-*`).

## What's Changed

### Added

**Обязательно — инвентарь / крафт / wards** ([STACK](https://github.com/yanlogan/valheim/blob/main/docs/STACK.md#инвентарь)):
AzuCraftyBoxes, AzuEPI, Better_Cartography_Table, ImpactfulSkills, ItemDrawers, MultiUserChest, ConfigurationManager, PlanBuild, QSS, Recycle_N_Reclaim, SmarterContainers, ValheimPlus_Grantapher_Temporary, WardIsLove

**Обязательно — контент** ([STACK](https://github.com/yanlogan/valheim/blob/main/docs/STACK.md#контент)):
Atos_Arrows_JVL, BetterArchery, BoneAppetit, Clutter, HoneyPlus, InfinityTools, InstantMonsterLootDrop, MoreGatesExtended, OdinCampsite, OdinHorse, OdinsHorsePen, OdinShip, PlantEverything, PlantIt, Seasonality, TreesReborn, Valharvest, Venture_Terrain_Reset, XPortal

**Yanlo:** ChestUnloadButton 1.3.0, ShipExplorationAll 1.1.0 · WardIsLove: свой Thorward (`Ward Control=true` на сервере)

**По желанию** ([STACK](https://github.com/yanlogan/valheim/blob/main/docs/STACK.md#по-желанию)):
AAA_Crafting, ItemCompare, VNEI, CraftGuard, BetterSounds (male)+CustomAudio, EWM+Forteca, ImprovedBuildHud, MyLittleUI, PlantEasily, QuickTeleport, FenceSnap, AutoMapPins, BetterAutoRun, Willybach HD Seasonality

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
