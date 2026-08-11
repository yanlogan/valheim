# Chest Unload Button (Yanlo)

Client-only BepInEx helper for our Valheim stack:

1. Hides vanilla **Place Stacks** (optional, on by default)
2. Finds SmarterContainers Unload button (`\||/`)
3. Moves it next to **Take All** (default: **same row, to the right**)
4. Renames label to **Unload**
5. **v1.3.0:** after SC Unload to nearby relevant chests, dumps **remaining eligible** items into the **open** chest (`DumpLeftoversToOpenChest`, default true). Same SC filters — not Store-All.

## Requires (client)

- BepInEx
- `Roses-SmarterContainers` with `[Unload] enabled=true` and **`nativeButton=false`** (custom `\||/` button)
- QSS: `HideBaseGamePlaceStacksButton=true` recommended (plugin also force-hides)

## Install

Copy folder to:

`%AppData%\r2modmanPlus-local\Valheim\profiles\Valheim_Client\BepInEx\plugins\Yanlo-ChestUnloadButton\`

**Not on dedicated server.**

## If you remove QSS / SmarterContainers Unload

- If **SC Unload** is disabled: delete this plugin (nothing to restyle / patch).
- If **QSS** is removed: you can keep this plugin (it mainly styles SC Unload); Place Stacks hide still works via reflection.

## Config (`yanlo.ChestUnloadButton.cfg` after first run)

| Key | Default |
|-----|---------|
| Enabled | true |
| HidePlaceStacks | true |
| Label | Unload |
| Placement | **Below** (or `Right`) |
| Gap | 6 |
| MatchTakeAllSize | true |
| ExtraOffsetX / ExtraOffsetY | 0 |
| **DumpLeftoversToOpenChest** | **true** — leftover eligible → open chest after SC nearby unload |
| DebugLog | false |

## Build

```bash
dotnet build custom-mods/ChestUnloadButton/ChestUnloadButton.csproj -c Release
```

Copy `bin/ChestUnloadButton.dll` into the plugins folder.

## Replaces

Older experiment `Yanlo-QSSSortButtonOffset` — delete that folder if present.
