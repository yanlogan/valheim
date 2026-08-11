# Valheim — модпак нашего сервера

Инструкции и список модов для игры у нас: что поставить в **r2modman**, какие настройки важны, как пользоваться. Кастомные плагины **Yanlo-*** — в [Releases](https://github.com/yanlogan/valheim/releases/latest).

**[Latest Release](https://github.com/yanlogan/valheim/releases/latest)** · **[Стек модов](docs/STACK.md)** · **[Как играть](docs/HOWTO.md)** · **[Что изменилось](docs/CHANGES.md)**

---

## Быстрый старт

1. Поставь [r2modman](https://thunderstore.io/package/ebkr/r2modman/) → игра **Valheim** → свой профиль.
2. По [`docs/STACK.md`](docs/STACK.md): поставь всё из **Обязательно**, пройди **Удалить**. Контент-моды — по полному списку (иначе missing prefabs).
3. Скачай **`YanloMods-….zip`** с [последнего релиза](https://github.com/yanlogan/valheim/releases/latest) → распакуй папки `Yanlo-*` в  
   `%AppData%\r2modmanPlus-local\Valheim\profiles\<твой_профиль>\BepInEx\plugins\`.
4. Выставь ключи из [STACK → Конфиги](docs/STACK.md#конфиги) (**свои хоткеи не затирай**).
5. Раздел **По желанию** в STACK — по вкусу; глянь [Как играть](docs/HOWTO.md) и [Что изменилось](docs/CHANGES.md) / Discord от хоста.

Чужие моды — только через r2modman (Thunderstore). Полный export профиля (~1 ГБ) сюда не кладём.

---

## Требования

- Valheim (Steam)
- r2modman (или совместимый менеджер)
- Windows (пути ниже — `%AppData%\…`)

---

## FAQ

| Симптом | Что проверить |
|---------|----------------|
| Кик / «mod mismatch» | Нет **AzuCraftyBoxes** / **PlanBuild** / **WardIsLove** той же версии, что у хоста |
| Missing prefab / розовый куб | Нет контент-мода из [STACK](docs/STACK.md) (PlantEverything, OdinShip, BoneAppetit, …) |
| Нет кнопки Unload / карта на лодке как ваниль | Не распакован **Yanlo** zip в `plugins/` |
| Sort пакует снизу вверх | `UseTopDownLogicForEverything = true` в QSS cfg (**на каждом клиенте**) |
| Крафт жрёт ресурсы дважды | V+ `CraftFromChest` должен быть **false** при CraftyBoxes |
| В логе `Failed to deserialize Azumatt.AzuCraftyBoxes.yml` | Удали этот `.yml` из `BepInEx/config/` (`.cfg` не трогай); мод создаст файл заново |

---

## Для хоста / разработка

Сборка Yanlo, `install-client`, релизы циклов: **[`docs/HOST.md`](docs/HOST.md)**.
