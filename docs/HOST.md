# Для хоста (не для друзей)

Исходники Yanlo, скрипты сборки/установки, цикл PENDING → GitHub Release.

Друзьям достаточно [README](../README.md) + [STACK](STACK.md) + [HOWTO](HOWTO.md) + [Releases](https://github.com/yanlogan/valheim/releases).

## Структура репо

| Путь | Что |
|------|-----|
| `mods/` | исходники Yanlo |
| `mods/_archived/` | устаревшие |
| `dist/` | сборка (gitignore) |
| `docs/STACK.md` | канон стека для друзей |
| `cfg/` | актуальные `.cfg` для друзей (копия целиком) |
| `changelogs/PENDING.md` | дельта текущего цикла (можно техничнее) |
| `changelogs/PENDING_DISCORD.md` | короткий Discord paste |
| `changelogs/release-body-*.md` | GitHub Release для друзей |
| `docs/RELEASE_NOTES.md` | как писать Release / Discord |
| `scripts/build.ps1` | → `dist/Yanlo-*` |
| `scripts/install-client.ps1` | `dist/` → `Valheim_Client/plugins` |
| `scripts/release.ps1` | zip + GitHub Release `cycle-DATE` |
| `scripts/sync-cfg.ps1` | Client `BepInEx/config` → `cfg/` |

`BepInEx/plugins` — только install target (robocopy). Не git. Не junction.

## Два слоя версий

- **Semver мода** — `mods/*/manifest.json` (`ChestUnloadButton@1.3.0`)
- **Цикл друзей** — Release `cycle-YYYY-MM-DD` + `changelogs/YYYY-MM-DD_slug.md`

## Workflow

```text
правки → .\scripts\build.ps1 → .\scripts\install-client.ps1 → тест
→ дописать changelogs/PENDING.md + PENDING_DISCORD.md
→ при смене стека обновить docs/STACK.md (+ HOWTO при новом UX)
→ при правке cfg на Client: .\scripts\sync-cfg.ps1 (+ ключи в STACK → Конфиги)
→ показать хосту `changelogs/release-body-cycle-….md` + `PENDING_DISCORD.md` (чипы в чате) → **ждать ок** → тогда commit + push `origin/main` + mid-cycle `gh release edit --notes-file changelogs/release-body-….md`. Не пушить до аппрува. В **Release notes** не писать «цикл open / PENDING» — это только в `PENDING.md` / HOST.
→ «синк с друзьями» (все забрали):
    PENDING.md → changelogs/YYYY-MM-DD_slug.md
    .\scripts\release.ps1 -Tag cycle-YYYY-MM-DD -NotesFile .\changelogs\YYYY-MM-DD_slug.md
    новый PENDING из _PENDING_TEMPLATE.md
→ Discord: PENDING_DISCORD (URL релиза + буллеты; стиль в RELEASE_NOTES.md)
```

Release / PENDING = **дельта + ссылки**. Не дублировать полный STACK и не вставлять большие ini — канон файлов в `cfg/`, ключи в STACK `#конфиги`.  
Текст GitHub Release / Discord — [RELEASE_NOTES.md](RELEASE_NOTES.md): симптом для друга, не хост-жаргон; серверные правки в блок «делать ничего не нужно».

Чужие моды: r2modman на Client. Нужные на dedicated — **руками** в  
`C:\Program Files (x86)\Steam\steamapps\common\Valheim dedicated server\BepInEx\plugins`  
(по server `docs/MOD_PLACEMENT.md`).

## Команды

Нужны: Valheim + профиль `Valheim_Client` (HintPath в `.csproj`), [GitHub CLI](https://cli.github.com/) (`gh auth login`).

```powershell
cd E:\Dev\yanlo-valheim
.\scripts\build.ps1
.\scripts\install-client.ps1
.\scripts\sync-cfg.ps1
.\scripts\release.ps1 -Tag cycle-2026-08-11 -NotesFile .\changelogs\2026-08-11_slug.md
```

## Не в git

`dist/`, `bin/`, `obj/`, third-party DLL, Client profile, миры, секреты.
