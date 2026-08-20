# CraftyBoxes Drawer Fix (Yanlo)



Client-only patch. **1.1.8**



## Fixes



### ItemDrawers (CraftyBoxes 1.8.15)

Inject Makail drawers into CraftyBoxes nearby list (1.8.15 regression: scratch list never merged into `_cachedAll`).



### AAA `Max:` craft amount

Per-item CraftyBoxes counts (no stale `AcbExtra`); allow `Max: 0`.



### AAA multi-craft / reclaim queue (1.1.5)

AAA `queueNextCraft` auto-clicks Craft after each item. `UpdateCraftingPanel` can jump selection to list index 0 (`GetSelectedRecipeIndex` fallback) — next craft becomes the wrong recipe (same with Recycle reclaim). We pin selection to the started recipe, or cancel remaining AAA amount if it is gone/uncraftable.



### Take Stack after autodeposit (1.1.8)

V+ kiln/furnace `autoDeposit` writes into the drawer 1-slot inventory. MultiUserChest then clones that slot onto `_item` (`maxStackSize = 9999`). ItemDrawers `E` sent `Drop(9999)` → one vanilla stack in inventory, rest destroyed. We send prefab `maxStackSize` instead.

### Empty drawer keeps type (1.1.7)

CraftyBoxes `ConsumeSilently` called ItemDrawers `Clear()` at qty 0 (same as Alt+E). We decrement qty only — empty drawer stays locked to that item (floor vacuum still works).

### Perf / station fill

- **1.1.6:** also inject while hovering `Smelter` / fermenter / fireplace / cooking / turret / shield (mill, spinning wheel, kiln). 1.1.4 skipped those because they are not `CraftingStation` — chests still filled via CraftyBoxes, drawers did not.
- **1.1.4:** no drawer inject outside craft station / hammer build mode (inventory/chest hitch).

- **1.1.3:** one `AggregatedMkzContainer` (dict `ItemCount`); AAA recount only at crafting station; inject interval **0.5 s**.

- **1.1.2:** cached shared names.

- **1.1.1:** rate-limit `FindObjectsByType`.



## Requires (client)



- BepInEx

- `Azumatt-AzuCraftyBoxes` **1.8.15**

- `makail-ItemDrawers`

- Optional: `Azumatt-AAA_Crafting` (Max + queue fix soft-dep)



## Install



`%AppData%\r2modmanPlus-local\Valheim\profiles\Valheim_Client\BepInEx\plugins\Yanlo-CraftyBoxesDrawerFix\`



**Not on dedicated server.**



## Config (`yanlo.CraftyBoxesDrawerFix.cfg`)



| Key | Default |

|-----|---------|

| Enabled | true |

| FixAaaMaxCraft | true |

| FixAaaCraftQueue | true |

| DebugLog | false |



## Build



```powershell

cd E:\Dev\yanlo-valheim

.\scripts\build.ps1

.\scripts\install-client.ps1

```

