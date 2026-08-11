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

- **AzuCraftyBoxes** <sub>от Azumatt</sub>
- **AzuExtendedPlayerInventory** <sub>от Azumatt</sub>
- **Better_Cartography_Table** <sub>от nbusseneau</sub>
- **ImpactfulSkills** <sub>от MidnightMods</sub>
- **ItemDrawers** <sub>от makail</sub>
- **MultiUserChest** <sub>от MSchmoecker</sub>
- **Official_BepInEx_ConfigurationManager** <sub>от Azumatt</sub>
- **PlanBuild** <sub>от MathiasDecrock</sub>
- **Quick_Stack_Store_Sort_Trash_Restock** <sub>от Goldenrevolver</sub>
- **Recycle_N_Reclaim** <sub>от Azumatt</sub>
- **SmarterContainers** <sub>от Roses</sub>
- **ValheimPlus_Grantapher_Temporary** <sub>от Grantapher</sub>
- **WardIsLove** <sub>от Azumatt</sub>

**Обязательно — контент / мир**

- **Atos_Arrows_JVL** <sub>от Digitalroot</sub>
- **BetterArchery** <sub>от ishid4</sub>
- **BoneAppetit** <sub>от RockerKitten</sub>
- **Clutter** <sub>от plumga</sub>
- **HoneyPlus** <sub>от OhhLoz</sub>
- **InfinityTools** <sub>от Numenos</sub>
- **InstantMonsterLootDrop** <sub>от cjayride</sub>
- **MoreGatesExtended** <sub>от shudnal</sub>
- **OdinCampsite** <sub>от OdinPlus</sub>
- **OdinHorse** <sub>от OdinPlus</sub>
- **OdinsHorsePen** <sub>от OdinPlus</sub>
- **OdinShip** <sub>от Marlthon</sub>
- **PlantEverything** <sub>от Advize</sub>
- **PlantIt** <sub>от OdinPlus</sub>
- **Seasonality** <sub>от RustyMods</sub>
- **TreesReborn** <sub>от TastyChickenLegs</sub>
- **Valharvest** <sub>от Frenvius</sub>
- **Venture_Terrain_Reset** <sub>от VentureValheim</sub>
- **XPortal** <sub>от SpikeHimself</sub>

**Yanlo**

- **Yanlo-ChestUnloadButton** **1.3.0** <sub>от Yanlo</sub>
- **Yanlo-ShipExplorationAll** **1.1.0** <sub>от Yanlo</sub>
- WardIsLove: свой Thorward на доме; на сервере `Ward Control=true`

**По желанию**

- **AAA_Crafting** <sub>от Azumatt</sub>
- **AutoMapPins** <sub>от abfielder</sub>
- **BetterAutoRun** <sub>от nearbear</sub>
- **BetterSounds** <sub>от Wiandar</sub> (male + `CustomAudio.zip`)
- **CraftGuard** <sub>от jg224</sub>
- **Expand_World_Music** <sub>от JereKuusela</sub>
- **FenceSnap** <sub>от MSchmoecker</sub>
- **Forteca_Soundtrack** <sub>от BlackViking</sub>
- **ImprovedBuildHud** <sub>от RandyKnapp</sub>
- **ItemCompare** <sub>от Azumatt</sub>
- **MyLittleUI** <sub>от shudnal</sub>
- **PlantEasily** <sub>от Advize</sub>
- **QuickTeleport** <sub>от OdinPlus</sub>
- **VNEI** <sub>от MSchmoecker</sub>
- **Willybachs_HD_Seasonality** <sub>от Willybach</sub>

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
