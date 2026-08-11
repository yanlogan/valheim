# cycle-2026-08-11

Baseline Yanlo zip уже здесь. Цикл **ещё open** (не все друзья забрали) — живая дельта: [PENDING](https://github.com/yanlogan/valheim/blob/main/changelogs/PENDING.md).

**Онбординг (канон):**
- [Стек модов](https://github.com/yanlogan/valheim/blob/main/docs/STACK.md)
- [Как играть](https://github.com/yanlogan/valheim/blob/main/docs/HOWTO.md)
- [Готовые cfg](https://github.com/yanlogan/valheim/tree/main/cfg) · или ключи ниже / в [STACK → Конфиги](https://github.com/yanlogan/valheim/blob/main/docs/STACK.md#конфиги)

**Yanlo:** распакуй `YanloMods-cycle-2026-08-11.zip` → `BepInEx/plugins/` (папки `Yanlo-*`).

## Что изменилось

### Добавлено

- **AzuCraftyBoxes** <sub>от Azumatt</sub>
- **Better_Cartography_Table** <sub>от nbusseneau</sub>
- **ImpactfulSkills** <sub>от MidnightMods</sub>
- **MultiUserChest** <sub>от MSchmoecker</sub>
- **PlanBuild** <sub>от MathiasDecrock</sub>
- **WardIsLove** <sub>от Azumatt</sub> (сломать поставленный у дома Ward и выставить ему указанный на табличке радиус в GUI)
- **Yanlo-ChestUnloadButton** <sub>от Yanlo</sub>
- **Yanlo-ShipExplorationAll** **1.1.0** <sub>от Yanlo</sub>

**По желанию**

- **AAA_Crafting** <sub>от Azumatt</sub>
- **BetterSounds** <sub>от Wiandar</sub> (male + `CustomAudio.zip`)
- **CraftGuard** <sub>от jg224</sub>
- **Expand_World_Music** <sub>от JereKuusela</sub>
- **Forteca_Soundtrack** <sub>от BlackViking</sub>
- **VNEI** <sub>от MSchmoecker</sub>

### Удалено

- GemHunter ShipExploration → Yanlo
- NoBuildRestriction

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
