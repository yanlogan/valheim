# cycle-2026-08-18

**С чего начать:**
- [Список модов](https://github.com/yanlogan/valheim/blob/main/docs/STACK.md)
- [Как играть](https://github.com/yanlogan/valheim/blob/main/docs/HOWTO.md)
- [Готовые настройки](https://github.com/yanlogan/valheim/tree/main/cfg)

## Что нового

**Yanlo-PortalWardFix 1.1.0** — **на клиенте** (хост копирует и на dedicated). После портала при включённых вардах друзья снова открывают **свои** сундуки; портал «насквозь» тоже. Перезапиши папку `Yanlo-PortalWardFix`.

**Yanlo-EpiTombFit 1.3.0** — **на клиенте и на dedicated** (не только у себя в r2modman).

Чинит пропадание вещей из **быстрых слотов (Z / X / C)** и из **слотов экипировки** (броня, пояс, wishbone, амулет и т.п.), если ты **умер далеко от базы** и потом **вернулся через портал** к могиле: раньше могила могла **сама опустеть**, или после забора лут **не возвращался на место** (оказывался в хотбаре или пропадал). Теперь вещи должны оставаться в могиле и возвращаться **туда же**, откуда лежали.

Нужен **AzuExtendedPlayerInventory** (как и весь наш стек).

**Yanlo-CraftyBoxesDrawerFix 1.1.6** — мельница / прялка / печь тянут из **ItemDrawers** (Shift+E), не только из сундуков. Остальные **Yanlo-*** без изменений (ChestUnload, ShipExploration).

Zip [`YanloMods-cycle-2026-08-18.zip`](https://github.com/yanlogan/valheim/releases/download/cycle-2026-08-18/YanloMods-cycle-2026-08-18.zip) → распаковать папки `Yanlo-*` в `BepInEx/plugins/`. **Хост:** скопируй **Yanlo-EpiTombFit** и **Yanlo-PortalWardFix** ещё и на dedicated server.
