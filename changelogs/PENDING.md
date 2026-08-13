# Friends cycle — PENDING

Status: **open** · started: **2026-08-09**  
Стек: [`docs/STACK.md`](../docs/STACK.md) · Геймплей: [`docs/HOWTO.md`](../docs/HOWTO.md) · Конфиги: [`cfg/`](../cfg/)

## Ссылки

- **Дельта (этот файл):** https://github.com/yanlogan/valheim/blob/main/changelogs/PENDING.md
- **Release + Yanlo zip:** https://github.com/yanlogan/valheim/releases/tag/cycle-2026-08-13
- **Стек / HOWTO / cfg:** [STACK](https://github.com/yanlogan/valheim/blob/main/docs/STACK.md) · [HOWTO](https://github.com/yanlogan/valheim/blob/main/docs/HOWTO.md) · [cfg/](https://github.com/yanlogan/valheim/tree/main/cfg)

> Baseline zip уже на Release; цикл **open**, пока не все друзья забрали апдейт. Полный стек — в STACK; здесь дельта для друзей.

---

## Что изменилось

### Добавлено

- **AzuCraftyBoxes** <sub>от Azumatt</sub>
- **Better_Cartography_Table** <sub>от nbusseneau</sub>
- **ImpactfulSkills** **0.12.0** <sub>от MidnightMods</sub> (обновить с 0.11.x — skill rates для мод-скиллов / AOE mining toggle)
- **MultiUserChest** <sub>от MSchmoecker</sub>
- **PlanBuild** <sub>от MathiasDecrock</sub>
- **WardIsLove** <sub>от Azumatt</sub> (сломать поставленный у дома Ward и выставить ему указанный на табличке радиус в GUI)

**Yanlo** — zip [`YanloMods-cycle-2026-08-13.zip`](https://github.com/yanlogan/valheim/releases/download/cycle-2026-08-13/YanloMods-cycle-2026-08-13.zip) → `BepInEx/plugins/` (полный набор `Yanlo-*`). **Новое в этом бампе только:**

- **Yanlo-CraftyBoxesDrawerFix** **1.1.3** <sub>от Yanlo</sub> — drawers в крафте/AAA Max (нужен AzuCraftyBoxes)

ChestUnload / ShipExploration в zip **без изменений** (как в 08-11).

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

; Azumatt.AzuAntiArthriticCrafting.cfg  (AAA_Crafting — если ставишь)
[5 - Recipe Tracker (Position)]
Recipe Tracker Position = {"x":200.0,"y":200.0}
[5 - Recipe Tracker (Sizes)]
Recipe Tracker Panel Scale = {"x":0.5,"y":0.5}
Recipe Tracker Item Name Max = 15

; com.inventoryux.valheim.cfg  (CraftGuard — если ставишь с AAA)
[CraftingUI]
OrganizeRecipes = false
; Hammer OrganizeCrafting / OrganizeBuilding / OrganizeHeavyBuilding / OrganizeFurniture = true
```

### Как пользоваться

См. [HOWTO](../docs/HOWTO.md) (Sort / Unload / Wards / PlanBuild / Cartography / AAA tracker / …).

---

## Закрытие цикла

Только когда все друзья забрали апдейт («синк с друзьями»):

1. Этот файл → `changelogs/YYYY-MM-DD_slug.md`
2. `.\scripts\release.ps1` при новом zip/notes (или `gh release edit` для правок notes)
3. Новый PENDING из `_PENDING_TEMPLATE.md`
