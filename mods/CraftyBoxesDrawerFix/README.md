# CraftyBoxes Drawer Fix (Yanlo)

Client-only patch. **1.1.3**

## Fixes

### ItemDrawers (CraftyBoxes 1.8.15)
Inject Makail drawers into CraftyBoxes nearby list (1.8.15 regression: scratch list never merged into `_cachedAll`).

### AAA `Max:` craft amount
Per-item CraftyBoxes counts (no stale `AcbExtra`); allow `Max: 0`.

### Perf
- **1.1.3:** one `AggregatedMkzContainer` (dict `ItemCount`) instead of N per-drawer wrappers; AAA recount only at a crafting station; inject interval **0.5 s**.
- **1.1.2:** cached shared names (stock `mkzDrawer.Name` hit ObjectDB every `ItemCount`).
- **1.1.1:** rate-limit `FindObjectsByType`.

## Requires (client)

- BepInEx
- `Azumatt-AzuCraftyBoxes` **1.8.15**
- `makail-ItemDrawers`
- Optional: `Azumatt-AAA_Crafting` (Max fix soft-dep)

## Install

`%AppData%\r2modmanPlus-local\Valheim\profiles\Valheim_Client\BepInEx\plugins\Yanlo-CraftyBoxesDrawerFix\`

**Not on dedicated server.**

## Config (`yanlo.CraftyBoxesDrawerFix.cfg`)

| Key | Default |
|-----|---------|
| Enabled | true |
| FixAaaMaxCraft | true |
| DebugLog | false |

## Build

```powershell
cd E:\Dev\yanlo-valheim
.\scripts\build.ps1
.\scripts\install-client.ps1
```
