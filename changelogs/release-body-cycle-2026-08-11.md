# cycle-2026-08-11

Baseline Yanlo zip уже здесь. Цикл **ещё open** (не все друзья забрали) — живая дельта: [PENDING](https://github.com/yanlogan/valheim/blob/main/changelogs/PENDING.md).

**Онбординг (канон):**
- [Стек модов](https://github.com/yanlogan/valheim/blob/main/docs/STACK.md)
- [Как играть](https://github.com/yanlogan/valheim/blob/main/docs/HOWTO.md)
- [Готовые cfg](https://github.com/yanlogan/valheim/tree/main/cfg) · или ключи ниже / в [STACK → Конфиги](https://github.com/yanlogan/valheim/blob/main/docs/STACK.md#конфиги)

**Yanlo:** распакуй `YanloMods-cycle-2026-08-11.zip` → `BepInEx/plugins/` (папки `Yanlo-*`).

## Что изменилось

### Добавлено

**Обязательно — инвентарь / крафт / wards**

- AzuCraftyBoxes — Azumatt
- AzuExtendedPlayerInventory — Azumatt
- Better_Cartography_Table — nbusseneau
- ImpactfulSkills — MidnightMods
- ItemDrawers — makail
- MultiUserChest — MSchmoecker
- Official_BepInEx_ConfigurationManager — Azumatt
- PlanBuild — MathiasDecrock
- Quick_Stack_Store_Sort_Trash_Restock — Goldenrevolver
- Recycle_N_Reclaim — Azumatt
- SmarterContainers — Roses
- ValheimPlus_Grantapher_Temporary — Grantapher
- WardIsLove — Azumatt

**Обязательно — контент / мир**

- Atos_Arrows_JVL — Digitalroot
- BetterArchery — ishid4
- BoneAppetit — RockerKitten
- Clutter — plumga
- HoneyPlus — OhhLoz
- InfinityTools — Numenos
- InstantMonsterLootDrop — cjayride
- MoreGatesExtended — shudnal
- OdinCampsite — OdinPlus
- OdinHorse — OdinPlus
- OdinsHorsePen — OdinPlus
- OdinShip — Marlthon
- PlantEverything — Advize
- PlantIt — OdinPlus
- Seasonality — RustyMods
- TreesReborn — TastyChickenLegs
- Valharvest — Frenvius
- Venture_Terrain_Reset — VentureValheim
- XPortal — SpikeHimself

**Yanlo**

- Yanlo-ChestUnloadButton **1.3.0** — Yanlo
- Yanlo-ShipExplorationAll **1.1.0** — Yanlo
- WardIsLove: свой Thorward на доме; на сервере `Ward Control=true`

**По желанию**

- AAA_Crafting — Azumatt
- AutoMapPins — abfielder
- BetterAutoRun — nearbear
- BetterSounds — Wiandar (male + `CustomAudio.zip`)
- CraftGuard — jg224
- Expand_World_Music — JereKuusela
- FenceSnap — MSchmoecker
- Forteca_Soundtrack — BlackViking
- ImprovedBuildHud — RandyKnapp
- ItemCompare — Azumatt
- MyLittleUI — shudnal
- PlantEasily — Advize
- QuickTeleport — OdinPlus
- VNEI — MSchmoecker
- Willybachs_HD_Seasonality — Willybach

### Удалено

- GemHunter ShipExploration → Yanlo
- NoBuildRestriction
- Yanlo-QSSSortButtonOffset
- Не ставить: AzuAutoStore, TrashItems (см. Удалить в STACK)

### Конфиг

Готовые файлы: [`cfg/`](https://github.com/yanlogan/valheim/tree/main/cfg). Или только эти ключи:

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
; UseTopDownLogicForEverything НЕ с сервера — у каждого клиента

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

### Как пользоваться

[HOWTO](https://github.com/yanlogan/valheim/blob/main/docs/HOWTO.md)
