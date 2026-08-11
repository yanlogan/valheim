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
| `changelogs/PENDING.md` | дельта текущего цикла |
| `changelogs/PENDING_DISCORD.md` | короткий Discord paste |
| `scripts/build.ps1` | → `dist/Yanlo-*` |
| `scripts/install-client.ps1` | `dist/` → `Valheim_Client/plugins` |
| `scripts/release.ps1` | zip + GitHub Release `cycle-DATE` |

`BepInEx/plugins` — только install target (robocopy). Не git. Не junction.

## Два слоя версий

- **Semver мода** — `mods/*/manifest.json` (`ChestUnloadButton@1.3.0`)
- **Цикл друзей** — Release `cycle-YYYY-MM-DD` + `changelogs/YYYY-MM-DD_slug.md`

## Workflow

```text
правки → .\scripts\build.ps1 → .\scripts\install-client.ps1 → тест
→ дописать changelogs/PENDING.md + PENDING_DISCORD.md
→ при смене стека обновить docs/STACK.md (+ HOWTO при новом UX)
→ «синк с друзьями»:
    PENDING.md → changelogs/YYYY-MM-DD_slug.md
    .\scripts\release.ps1 -Tag cycle-YYYY-MM-DD -NotesFile .\changelogs\YYYY-MM-DD_slug.md
    новый PENDING из _PENDING_TEMPLATE.md
→ Discord: PENDING_DISCORD (+ ссылки на Release и полный MD)
```

Чужие моды: r2modman на Client. Нужные на dedicated — **руками** в  
`C:\Program Files (x86)\Steam\steamapps\common\Valheim dedicated server\BepInEx\plugins`  
(по server `docs/MOD_PLACEMENT.md`).

## Команды

Нужны: Valheim + профиль `Valheim_Client` (HintPath в `.csproj`), [GitHub CLI](https://cli.github.com/) (`gh auth login`).

```powershell
cd E:\Dev\yanlo-valheim
.\scripts\build.ps1
.\scripts\install-client.ps1
.\scripts\release.ps1 -Tag cycle-2026-08-11 -NotesFile .\changelogs\2026-08-11_slug.md
```

## Не в git

`dist/`, `bin/`, `obj/`, third-party DLL, Client profile, миры, секреты.
