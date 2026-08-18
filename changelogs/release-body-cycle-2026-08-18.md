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

### Yanlo-CraftyBoxesDrawerFix 1.1.6

- Мельница, прялка и печь теперь тянут ресы при взаимодействии не только из сундуков/инвентаря, но и из ящиков

## Конфиги

### Если установлен мод AutoMapPins

- Файл `FixItFelix.AutoMapPins.categories.vanilla.yaml` → в свой `BepInEx/config/`
- Файлик от спама автопинами на карте
- Ресурсы только **рядом и только несобранные**: медь, дикие семена (морковь/репа), ячмень, лён, ядра суртлинга, дёготь; неразрушенные спавны. Семена лука не пинятся — они только в сундуках в горах
- Данжи, пещеры и суртлинг-спавны остаются навсегда

---

## Изменено на сервере (делать ничего не нужно)

- Увеличено количество перерабатываемой руды в плавильне чёрного металла до 100
