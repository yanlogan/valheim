# Friends cycle — PENDING

Status: **open** · started: **2026-08-09**  
Стек: [`docs/STACK.md`](../docs/STACK.md) · Геймплей: [`docs/HOWTO.md`](../docs/HOWTO.md) · Конфиги: [`cfg/`](../cfg/)

## Ссылки

- **Дельта (этот файл):** https://github.com/yanlogan/valheim/blob/main/changelogs/PENDING.md
- **Release + Yanlo zip:** https://github.com/yanlogan/valheim/releases/tag/cycle-2026-08-18
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

**Yanlo** — zip [`YanloMods-cycle-2026-08-18.zip`](https://github.com/yanlogan/valheim/releases/download/cycle-2026-08-18/YanloMods-cycle-2026-08-18.zip) → `BepInEx/plugins/` (полный набор `Yanlo-*`). **Новое в этом бампе:**

- **Yanlo-EpiTombFit** **1.3.0** <sub>от Yanlo</sub> — **клиент + dedicated.** Чинит пропадание вещей из **быстрых слотов (Z/X/C)** и **слотов экипировки** (броня, пояс, wishbone, амулет), если умер **далеко от базы** и вернулся **через портал**: могила могла сама опустеть, или лут после **E** не возвращался на место. Нужен AzuEPI.
- **Yanlo-CraftyBoxesDrawerFix** **1.1.6** <sub>от Yanlo</sub> — без него крафт часто пишет `0/N` на материалы в **настенных drawers** (в сундуках всё ок). **1.1.6:** мельница / прялка / печь теперь тоже тянут из drawers (**Shift+E**); раньше при сундуке работало, при drawer — нет (двор без верстака). **1.1.5:** крафт/reclaim пачкой больше не уезжает на следующий рецепт.
- **Yanlo-PortalWardFix** **1.1.0** <sub>от Yanlo</sub> — **обязательно на клиенте** с WardIsLove (на dedicated тоже скопируй). Чинит портал «насквозь» **и** сундуки после портала: при включённых вардах друзья не могли открыть **свои** сундуки, пока не отбегут из зоны (у хоста всё ок). Перезапиши папку `Yanlo-PortalWardFix`.

ChestUnload / ShipExploration в zip **без изменений**.

**По желанию**

- **AAA_Crafting** <sub>от Azumatt</sub>
- **AutoMapPins** <sub>от abfielder</sub> — yaml в [`cfg/`](../cfg/)
- **BetterSounds** <sub>от Wiandar</sub> (male + `CustomAudio.zip`)
- **CraftGuard** <sub>от jg224</sub>
- **Expand_World_Music** <sub>от JereKuusela</sub>
- **Forteca_Soundtrack** <sub>от BlackViking</sub>
- **VNEI** <sub>от MSchmoecker</sub>

### Удалено

- GemHunter ShipExploration → Yanlo
- NoBuildRestriction
- **Asocial_Cartography** — Disable/Uninstall (вместо него Better_Cartography_Table)
- **Venture_Floating_Items** — Disable/Uninstall (плавающие предметы; у нас выкл)
- **TimedTorchesStayLit** / **Seasonality_Fix** — с клиента убрать (только на **dedicated**)
- **ConditionalConfigSync** — сирота, не нужен
- Yanlo-QSSSortButtonOffset (старый UI-патч; не нужен)
- Не ставить: **AzuAutoStore**, **TrashItems** (см. [Удалить в STACK](../docs/STACK.md#удалить))

В **ValheimPlus** (не отдельный мод): `[GameClock] enabled = false` — часы на экране выкл.

### Конфиг

- **Sort restored** (speculative disable reverted). `DisplaySortButtons=Both`; `mergeWithExistingStacks=true`. Отладочный Yanlo-EpiDeathDiag снят — вместо него **Yanlo-EpiTombFit** в Release.
- **ValheimPlus** `[Furnace]` (blast furnace): `maximumOre = 100` (было 50; уголь уже 100). `autoDeposit = true`. Уже стоящие печи — перестроить или рестарт dedicated.
- **AutoMapPins** (optional, только клиент): yaml в [`cfg/FixItFelix.AutoMapPins.categories.vanilla.yaml`](../cfg/FixItFelix.AutoMapPins.categories.vanilla.yaml) — радар: медь, морковь, ячмень/лён/molten/репа/tar/чертополох, живые гнёзда; крипты+суртлинги навсегда. Скопируй файл, если ставишь AMP.


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
[Furnace]
maximumOre = 100
maximumCoal = 100
autoDeposit = true
[StructuralIntegrity]
enabled = true
[GameClock]
enabled = false
; itemsFloatInWater = false  (плавучесть всего лута — выкл; Venture_Floating_Items не ставить)

; Azumatt.AzuAntiArthriticCrafting.cfg  (AAA_Crafting — если ставишь)
[5 - Recipe Tracker (Position)]
Recipe Tracker Position = {"x":200.0,"y":200.0}
[5 - Recipe Tracker (Sizes)]
Recipe Tracker Panel Scale = {"x":0.5,"y":0.5}
Recipe Tracker Req Name Max = 15

; com.inventoryux.valheim.cfg  (CraftGuard — если ставишь с AAA)
[CraftingUI]
OrganizeRecipes = false
; Hammer OrganizeCrafting / OrganizeBuilding / OrganizeHeavyBuilding / OrganizeFurniture = true

; FixItFelix.AutoMapPins.categories.vanilla.yaml  (AutoMapPins — если ставишь)
; целиком файл из cfg/
```

### Как пользоваться

См. [HOWTO](../docs/HOWTO.md) (Sort / Unload / Wards / PlanBuild / Cartography / AAA tracker / …).

---

## Закрытие цикла

Только когда все друзья забрали апдейт («синк с друзьями»):

1. Этот файл → `changelogs/YYYY-MM-DD_slug.md`
2. `.\scripts\release.ps1` при новом zip/notes (или `gh release edit` для правок notes)
3. Новый PENDING из `_PENDING_TEMPLATE.md`
