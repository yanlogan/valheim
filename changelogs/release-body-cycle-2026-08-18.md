# cycle-2026-08-18

**С чего начать:**
- [Список модов](https://github.com/yanlogan/valheim/blob/main/docs/STACK.md)
- [Как играть](https://github.com/yanlogan/valheim/blob/main/docs/HOWTO.md)
- [Готовые настройки](https://github.com/yanlogan/valheim/tree/main/cfg)

[`YanloMods-cycle-2026-08-18.zip`](https://github.com/yanlogan/valheim/releases/download/cycle-2026-08-18/YanloMods-cycle-2026-08-18.zip) → распаковать папки `Yanlo-*` в `BepInEx/plugins/`.

### Yanlo-PortalWardFix 1.1.0

- Нужен, если установлен WardIsLove
- Чинит неработающий телепорт с базы
- Чинит доступ в свои же сундуки после захода на сервер / возвращения на базу через портал

### Yanlo-EpiTombFit 1.3.0

- Нужен, если установлен AzuExtendedPlayerInventory
- Чинит пропадание экипировки и быстрых слотов при смерти вне базы после телепорта

### Yanlo-CraftyBoxesDrawerFix 1.1.7

- Мельница, прялка и печь теперь тянут ресы при взаимодействии не только из сундуков/инвентаря, но и из ящиков
- Если крафт / Shift+E опустошил ящик — тип предмета остаётся (не сбрасывается как Alt+E)

## Конфиги

### Если установлен мод AutoMapPins

- Файл [`FixItFelix.AutoMapPins.categories.vanilla.yaml`](https://github.com/yanlogan/valheim/blob/main/cfg/FixItFelix.AutoMapPins.categories.vanilla.yaml) → свой `BepInEx/config/` (есть и в Assets релиза)
- Ресурсы только **рядом и несобранные**: медь, дикие семена (морковь/репа), ячмень, лён, molten core, tar; живые гнёзда. Лук-семена — сундуки в горах (не пиним). Чертополох выкл
- Крипты / данжи / суртлинг-фермы — навсегда
- Если карта уже засрана старыми AMP-пинами: сохраниться → в меню → зайти (иногда два раза). Не пиши `amp clear_pins`

---

## Изменено на сервере (делать ничего не нужно)

- Увеличено количество перерабатываемой руды в плавильне чёрного металла до 100
