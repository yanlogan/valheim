# yanlo-valheim

Свои Valheim-моды (**Yanlo-***) + friends-циклы (полные MD + GitHub Releases).

Репо: [github.com/yanlogan/valheim](https://github.com/yanlogan/valheim) · [Latest Release](https://github.com/yanlogan/valheim/releases/latest)

`BepInEx/plugins` — только install target (копия скриптом). Не git. Не junction.

## Друзьям

1. Открой [Latest Release](https://github.com/yanlogan/valheim/releases/latest) → **What's Changed**.
2. Поставь/выключи моды по [`CLIENT_STACK.md`](CLIENT_STACK.md) (r2modman).
3. Скачай `YanloMods-cycle-….zip` → папки `Yanlo-*` в Client `plugins/`.
4. Полный changelog цикла — ссылка в Discord-брифе / файл в [`changelogs/`](changelogs/).

Полный r2modman export профиля сюда **не** кладём (~1 ГБ). Чужое — с Thunderstore по списку.

## Структура

| Путь | Что |
|------|-----|
| `mods/` | исходники Yanlo |
| `mods/_archived/` | устаревшие |
| `dist/` | сборка (gitignore) |
| `CLIENT_STACK.md` | актуальный клиентский стек + геймплей |
| `changelogs/PENDING.md` | текущий цикл (полный MD) |
| `changelogs/PENDING_DISCORD.md` | короткий Discord paste |
| `changelogs/YYYY-MM-DD_slug.md` | закрытые циклы (= тело Release notes) |
| `scripts/build.ps1` | → `dist/Yanlo-*` |
| `scripts/install-client.ps1` | `dist/` → твой `Valheim_Client/plugins` |
| `scripts/release.ps1` | zip + `gh release create cycle-DATE` |

## Два слоя версий

- **Semver мода** — `mods/*/manifest.json` (`ChestUnloadButton@1.3.0`)
- **Цикл друзей** — GitHub Release `cycle-YYYY-MM-DD` + файл в `changelogs/`

## Workflow хоста

```text
правки → build.ps1 → install-client.ps1 → тест
→ дописать PENDING.md + PENDING_DISCORD.md + CLIENT_STACK.md
→ «синк с друзьями»:
    PENDING.md → changelogs/YYYY-MM-DD_slug.md
    release.ps1 -Tag cycle-YYYY-MM-DD -NotesFile changelogs/YYYY-MM-DD_slug.md
    новый пустой PENDING
→ Discord: PENDING_DISCORD (+ ссылки на Release и полный MD)
```

Чужие моды: r2modman на Client; нужные на dedicated — **руками** в  
`…\Valheim dedicated server\BepInEx\plugins`.

## Build / install / release

Нужны: Valheim + профиль `Valheim_Client` (HintPath в `.csproj`), [GitHub CLI](https://cli.github.com/) для release.

```powershell
.\scripts\build.ps1
.\scripts\install-client.ps1
.\scripts\release.ps1 -Tag cycle-2026-08-11 -NotesFile .\changelogs\2026-08-11_slug.md
```

## Не в git

`dist/`, `bin/`, `obj/`, third-party DLL, Client profile, миры, секреты.
