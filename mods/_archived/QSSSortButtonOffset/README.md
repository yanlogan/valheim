# QSS Sort Button Offset

Tiny client-only BepInEx helper: after [QSS](https://thunderstore.io/c/valheim/p/Goldenrevolver/Quick_Stack_Store_Sort_Trash_Restock/) places the chest **Sort** button, nudge it so it does not cover vanilla **Place Stacks** (used by SmarterContainers Unload via `nativeButton=true`).

## Requires

- BepInEx
- `Goldenrevolver-Quick_Stack_Store_Sort_Trash_Restock` (QSS) with chest Sort enabled (`DisplaySortButtons` includes container)

## Install (r2modman `Valheim_Client`)

Copy folder into:

`%AppData%\r2modmanPlus-local\Valheim\profiles\Valheim_Client\BepInEx\plugins\Yanlo-QSSSortButtonOffset\`

Contents: `QSSSortButtonOffset.dll` (+ optional this README).

**Not needed on dedicated server** (UI-only).

## If you remove / disable QSS

**Delete this plugin folder too** — it only exists to fix QSS Sort layout.

## Config (`BepInEx/config/yanlo.QSSSortButtonOffset.cfg`)

| Key | Default | Notes |
|-----|---------|--------|
| `Enabled` | true | |
| `OffsetX` | 0 | localPosition delta |
| `OffsetY` | -42 | negative = down under Place Stacks |
| `DebugLog` | false | |

Tune `OffsetY` / `OffsetX` in F1 Configuration Manager if your UI scale differs.

## Build

```bash
dotnet build custom-mods/QSSSortButtonOffset/QSSSortButtonOffset.csproj -c Release
```

Copy `custom-mods/QSSSortButtonOffset/bin/QSSSortButtonOffset.dll` into the plugins folder above.
