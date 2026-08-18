# Discord (черновик, ≤1800–2000 символов)

Стек / HOWTO: https://github.com/yanlogan/valheim
cfg: https://github.com/yanlogan/valheim/tree/main/cfg
Release: https://github.com/yanlogan/valheim/releases/tag/cycle-2026-08-18

**Yanlo zip** → `BepInEx/plugins/` (все папки `Yanlo-*`). Хост: EpiTombFit + PortalWardFix ещё на dedicated.

- **PortalWardFix 1.1.0** (клиент) — портал «насквозь» + свои сундуки после портала при вардах
- **EpiTombFit 1.3.0** (клиент + dedicated) — смерть далеко + портал: QS / экипировка не пропадают
- **CraftyBoxesDrawerFix 1.1.6** — мельница / прялка / печь из drawers (Shift+E)

**Конфиги** из cfg/ (или ключи из STACK):

- **Blast furnace:** V+ `[Furnace] maximumOre = 100`. Рестарт dedicated; старые печи перестроить
- **AutoMapPins** (если ставишь): `FixItFelix.AutoMapPins.categories.vanilla.yaml` в `BepInEx/config/`

Как пользоваться: https://github.com/yanlogan/valheim/blob/main/docs/HOWTO.md
