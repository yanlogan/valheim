# Friends cycle — PENDING

Status: **open** · started: **2026-08-09**  
Стек: [`docs/STACK.md`](../docs/STACK.md) · Геймплей: [`docs/HOWTO.md`](../docs/HOWTO.md) · Конфиги: [`cfg/`](../cfg/)

## Ссылки

- **Дельта (этот файл):** https://github.com/yanlogan/valheim/blob/main/changelogs/PENDING.md
- **Release + Yanlo zip:** https://github.com/yanlogan/valheim/releases/tag/cycle-2026-08-11
- **Стек / HOWTO / cfg:** [STACK](https://github.com/yanlogan/valheim/blob/main/docs/STACK.md) · [HOWTO](https://github.com/yanlogan/valheim/blob/main/docs/HOWTO.md) · [cfg/](https://github.com/yanlogan/valheim/tree/main/cfg)

> Baseline zip уже на Release; цикл **open**, пока не все друзья забрали апдейт. Описания «что делает» — в STACK; готовые файлы — в `cfg/`.

---

## Что изменилось

### Добавлено

**Обязательно — инвентарь / крафт / wards** ([STACK](../docs/STACK.md#инвентарь)):

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

**Обязательно — контент / мир** ([STACK](../docs/STACK.md#контент)):

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

**Yanlo** ([zip](https://github.com/yanlogan/valheim/releases/download/cycle-2026-08-11/YanloMods-cycle-2026-08-11.zip)):

- Yanlo-ChestUnloadButton **1.3.0** — Yanlo
- Yanlo-ShipExplorationAll **1.1.0** — Yanlo
- WardIsLove: свой Thorward на доме; на сервере `Ward Control=true`

**По желанию** ([STACK](../docs/STACK.md#по-желанию)):

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

Готовые файлы: [`cfg/`](../cfg/). Или только эти ключи ([STACK → Конфиги](../docs/STACK.md#конфиги)), если свои бинды надо сохранить:

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

### Yanlo

- ChestUnloadButton **1.3.0** / ShipExplorationAll **1.1.0** — [скачать zip](https://github.com/yanlogan/valheim/releases/download/cycle-2026-08-11/YanloMods-cycle-2026-08-11.zip) → `plugins/Yanlo-*`

### Как пользоваться

См. [HOWTO](../docs/HOWTO.md) (Sort / Unload / Wards / PlanBuild / Cartography / AAA tracker / …).

---

## Закрытие цикла

Только когда все друзья забрали апдейт («синк с друзьями»):

1. Этот файл → `changelogs/YYYY-MM-DD_slug.md`
2. `.\scripts\release.ps1` при новом zip/notes (или `gh release edit` для правок notes)
3. Новый PENDING из `_PENDING_TEMPLATE.md`
