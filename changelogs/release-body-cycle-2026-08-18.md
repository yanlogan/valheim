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

- Файл [`FixItFelix.AutoMapPins.categories.vanilla.yaml`](https://github.com/yanlogan/valheim/blob/main/cfg/FixItFelix.AutoMapPins.categories.vanilla.yaml) → свой `BepInEx/config/`
- Теперь на карте будут отмечаться только ресурсы **рядом и только несобранные**: медь, дикие семена (морковь/репа), ячмень, лён, ядра суртлингов, дёготь; спавны (кроме суртлингов, это как вечный огонь). Семена лука не пинятся, они только находятся в сундуках в горах. 
- Крипты / данжи / суртлинг-спавны не удаляются
- Если карта уже засрана старыми пинами от мода: сохраниться → выйти в меню → зайти (иногда два раза)

---

## Изменено на сервере (делать ничего не нужно)

- Увеличено количество перерабатываемой руды в плавильне чёрного металла до 100

