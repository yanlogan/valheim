# cycle-2026-08-18

**С чего начать:**
- [Список модов](https://github.com/yanlogan/valheim/blob/main/docs/STACK.md)
- [Как играть](https://github.com/yanlogan/valheim/blob/main/docs/HOWTO.md)
- [Готовые настройки](https://github.com/yanlogan/valheim/tree/main/cfg)

## Yanlo zip

[`YanloMods-cycle-2026-08-18.zip`](https://github.com/yanlogan/valheim/releases/download/cycle-2026-08-18/YanloMods-cycle-2026-08-18.zip) → распаковать папки `Yanlo-*` в `BepInEx/plugins/`.

**Хост:** скопируй **Yanlo-EpiTombFit** и **Yanlo-PortalWardFix** ещё и на dedicated.

### Yanlo-PortalWardFix 1.1.0

- Нужен **на клиенте** вместе с WardIsLove
- Чинит портал «насквозь»
- После портала друзья снова открывают **свои** сундуки (варды включены)
- Перезапиши папку `Yanlo-PortalWardFix`

### Yanlo-EpiTombFit 1.3.0

- **Клиент + dedicated** (не только r2modman)
- Смерть далеко от базы + возврат **порталом** к могиле
- Вещи из **QS (Z/X/C)** и **слотов экипировки** не должны пропадать / уезжать в хотбар
- Нужен AzuEPI

### Yanlo-CraftyBoxesDrawerFix 1.1.6

- Мельница / прялка / печь тянут из **ItemDrawers** (`Shift+E`), не только из сундуков

ChestUnload / ShipExploration — без изменений.

## Конфиги

Готовые файлы: [`cfg/`](https://github.com/yanlogan/valheim/tree/main/cfg)  
Или только ключи: [STACK → Конфиги](https://github.com/yanlogan/valheim/blob/main/docs/STACK.md#конфиги) (свои хоткеи не затирай).

### Blast furnace (ValheimPlus, dedicated)

- `[Furnace] maximumOre = 100` (было 50)
- Уголь уже 100, `autoDeposit = true`
- Рестарт dedicated; уже стоящие печи — **перестроить**

Файл: [`cfg/valheim_plus.cfg`](https://github.com/yanlogan/valheim/blob/main/cfg/valheim_plus.cfg)

### AutoMapPins (optional)

- Файл [`FixItFelix.AutoMapPins.categories.vanilla.yaml`](https://github.com/yanlogan/valheim/blob/main/cfg/FixItFelix.AutoMapPins.categories.vanilla.yaml) → `BepInEx/config/`
- Ресурсы только **рядом и несобранные**: медь, морковь, ячмень, лён, molten core, семена репы, tar, чертополох; живые гнёзда
- Крипты и суртлинг-фермы — навсегда
- Есть и в Assets этого релиза
