# cycle-2026-08-16

**С чего начать:**
- [Список модов](https://github.com/yanlogan/valheim/blob/main/docs/STACK.md) — что ставить и что убрать
- [Как играть](https://github.com/yanlogan/valheim/blob/main/docs/HOWTO.md) — кнопки и фичи
- [Готовые настройки](https://github.com/yanlogan/valheim/tree/main/cfg)

## Что нового

**Yanlo-PortalWardFix** **1.0.1** — **обязательно на клиенте** вместе с WardIsLove. Чинит неработающий портал с прохождением насквозь (баг WardIsLove: радиус последнего инициализированного варда ломает считывание близости портала).

**Yanlo-CraftyBoxesDrawerFix** **1.1.5**: AAA multi-craft/reclaim не прыгает на следующий рецепт; чинит просадку FPS в инвентаре и на крафт-станциях.

Zip [`YanloMods-cycle-2026-08-16.zip`](https://github.com/yanlogan/valheim/releases/download/cycle-2026-08-16/YanloMods-cycle-2026-08-16.zip) → `BepInEx/plugins/` (ChestUnload / ShipExploration без изменений).

**Config (EPI death):** QSS `DisplaySortButtons = Disabled` (Trash only; Sort off); V+ `[Inventory] mergeWithExistingStacks = false` — чтобы quick slots AzuEPI не терялись при смерти. Готовые файлы в [`cfg/`](https://github.com/yanlogan/valheim/tree/main/cfg).
