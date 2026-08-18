# Discord (черновик, ≤1800–2000 символов)

Стек / HOWTO: https://github.com/yanlogan/valheim
cfg: https://github.com/yanlogan/valheim/tree/main/cfg
Release: https://github.com/yanlogan/valheim/releases/tag/cycle-2026-08-18

**Yanlo-EpiTombFit 1.3.0** — новый zip. Поставить **и на клиент, и на сервер** (хост копирует на dedicated).

Чинит: если умер **далеко**, вернулся **порталом** к могиле — вещи из **быстрых слотов Z/X/C** и **слотов экипировки** (броня, пояс, wishbone, амулет) больше не должны пропадать и должны вернуться **на место**, а не в хотбар.

**Yanlo-CraftyBoxesDrawerFix 1.1.6** — мельница / прялка теперь грузят barley/flax из **drawers** (Shift+E), не только из сундука. Перезапиши папку `Yanlo-CraftyBoxesDrawerFix` из zip.

Zip → `BepInEx/plugins/` (все папки `Yanlo-*`). Остальные Yanlo в zip без изменений.

**V+ blast furnace:** `maximumOre = 100` в `valheim_plus.cfg` (`[Furnace]`). Обновить cfg из [`cfg/`](https://github.com/yanlogan/valheim/tree/main/cfg) или ключи из PENDING. Рестарт dedicated; старые печи — перестроить.

Как пользоваться: https://github.com/yanlogan/valheim/blob/main/docs/HOWTO.md
