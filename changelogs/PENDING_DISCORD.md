# Discord (черновик, ≤1800–2000 символов)

Стек / HOWTO: https://github.com/yanlogan/valheim
cfg: https://github.com/yanlogan/valheim/tree/main/cfg
Release: https://github.com/yanlogan/valheim/releases/tag/cycle-2026-08-18

**Yanlo-PortalWardFix 1.1.0** — новый zip. Поставь на **клиент** (хост ещё на dedicated). Перезапиши папку `Yanlo-PortalWardFix`.

Чинит: после портала при включённых вардах друзья не могли открыть **свои** сундуки, пока не отбегут из зоны. Портал «насквозь» тоже.

**Yanlo-EpiTombFit 1.3.0** — **клиент + dedicated.** Смерть далеко + портал к могиле: QS Z/X/C и экипировка не должны пропадать.

**Yanlo-CraftyBoxesDrawerFix 1.1.6** — мельница / прялка / печь тянут из drawers (Shift+E).

Zip → `BepInEx/plugins/` (все папки `Yanlo-*`).

**V+ blast furnace:** `maximumOre = 100` в `valheim_plus.cfg` (`[Furnace]`). cfg из репо или ключи из PENDING. Рестарт dedicated; старые печи — перестроить.

Как пользоваться: https://github.com/yanlogan/valheim/blob/main/docs/HOWTO.md
