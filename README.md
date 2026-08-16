# Valheim — модпак нашего сервера

Инструкции и список модов для игры у нас: что поставить в **r2modman**, какие настройки важны, как пользоваться. Кастомные плагины **Yanlo-*** — в [Releases](https://github.com/yanlogan/valheim/releases/latest).

**[Latest Release](https://github.com/yanlogan/valheim/releases/latest)** · **[Стек модов](docs/STACK.md)** · **[Как играть](docs/HOWTO.md)** · **[Что изменилось](docs/CHANGES.md)**

---

## Быстрый старт

1. Поставь [r2modman](https://thunderstore.io/package/ebkr/r2modman/) → игра **Valheim** → свой профиль.
2. По [`docs/STACK.md`](docs/STACK.md): поставь всё из **Обязательно**, пройди **Удалить**. Контент-моды — по полному списку (иначе missing prefabs).
3. Скачай **`YanloMods-….zip`** с [последнего релиза](https://github.com/yanlogan/valheim/releases/latest) → распакуй папки `Yanlo-*` в  
   `%AppData%\r2modmanPlus-local\Valheim\profiles\<твой_профиль>\BepInEx\plugins\`.
4. Конфиги: либо готовые файлы из [`cfg/`](cfg/) в `BepInEx/config/`, либо только ключи из [STACK → Конфиги](docs/STACK.md#конфиги) (**свои хоткеи не затирай**, если правишь вручную).
5. Раздел **По желанию** в STACK — по вкусу; глянь [Как играть](docs/HOWTO.md) и [Что изменилось](docs/CHANGES.md) / Discord от хоста.

Чужие моды — только через r2modman (Thunderstore). Полный export профиля (~1 ГБ) сюда не кладём.

---

## Требования

- Valheim (Steam)
- r2modman (или совместимый менеджер)
- Windows (пути ниже — `%AppData%\…`)

---

## FAQ

| Симптом | Что проверить / фикс |
|---------|----------------------|
| Кик / «mod mismatch» | Нет **AzuCraftyBoxes** / **PlanBuild** / **WardIsLove** (и др. ServerSync) **той же версии**, что у хоста; мод не Enable |
| Missing prefab / розовый куб | Нет контент-мода из [STACK](docs/STACK.md) (PlantEverything, OdinShip, BoneAppetit, …) |
| Нет кнопки **Unload** / карта на лодке как ваниль | Не распакован **Yanlo** zip (`ChestUnloadButton` / `ShipExplorationAll`) в `plugins/` |
| В drawers / у крафта **0/N**, хотя материалы в настенных ящиках есть; AAA Max врёт; AAA amount>1 прыгает на следующий рецепт | Нужен **Yanlo-CraftyBoxesDrawerFix** **1.1.5+** из zip (клиент) |
| Портал «прохожу насквозь» / нет телепорта (особенно с базы) | **Yanlo-PortalWardFix** из zip + **WardIsLove**. Баг WiL: чужой большой радиус ломает CheckIn |
| Sort пакует **снизу вверх** | `UseTopDownLogicForEverything = true` в QSS cfg (**на каждом клиенте**, с сервера не приходит) — или готовый файл из [`cfg/`](cfg/) |
| Крафт жрёт ресурсы **дважды** | V+ `[CraftFromChest] enabled = false` при CraftyBoxes ([STACK → Конфиги](docs/STACK.md#конфиги) / `cfg/`) |
| В логе `Failed to deserialize Azumatt.AzuCraftyBoxes.yml` | Удали этот **`.yml`** из `BepInEx/config/` (`.cfg` не трогай); мод создаст заново |
| **E** на ItemDrawer: взял стак, остаток **пропал** | Фикс в стеке Yanlo: **AzuAutoStore** в [Удалить](docs/STACK.md#удалить) — специально не ставим (иначе Take Stack может съесть остаток). Если уже стоит — выключи |
| Вещи **теряются при смерти** (с AzuEPI) | QSS только **Sort + Trash** — не включай Quick Stack / Restock / Store All (наш cfg так и сделан) |
| Unload выкидывает грибы/ягоды/овощи не туда | Так задумано (`groupsList`). Dump-сундук после вылазки — **>14 м** от домов. Favorites QSS на Unload **не** влияют |
| На корабле OdinShip (War/Merchant/каноэ…) туман как ваниль | Удали **GemHunter ShipExploration**; поставь **Yanlo-ShipExplorationAll** |
| Ward Range в GUI «N%» непонятен | Это не проценты: значение **N** на шкале 0–100 ≈ радиус **N** (число со знака у дома) |
| AAA и CraftGuard ломают UI станций | При обоих: `OrganizeRecipes = false` в `com.inventoryux.valheim.cfg` ([STACK → Конфиги](docs/STACK.md#конфиги) / `cfg/`) |
| BetterSounds — звуки ванильные | После Install распакуй **`CustomAudio.zip`** рядом с DLL. Не ставь Female вместе с male. На dedicated **не** копировать |

---

## Для хоста / разработка

Сборка Yanlo, `install-client`, релизы циклов: **[`docs/HOST.md`](docs/HOST.md)**.
