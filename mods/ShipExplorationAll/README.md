# Ship Exploration All (Yanlo)

Client-only map explore radius while on a ship. **Replaces** `GemHunter1.ShipExploration` (do not install both).

## Covers

- **Vanilla:** Raft, Karve, Longship (`viking` / `vikingship` / `longship`)
- **OdinShip 0.7.6 (our pack):** BigCargoShip, CargoShip, MercantShip, WarShip, LittleBoat, RowingCanoe, DoubleRowingCanoe
- **OdinShipPlus-style names** (optional keys; unused if prefab absent)
- **`DefaultShipMultiplier`** for any other `Ship` component

## Install

1. Copy `Yanlo-ShipExplorationAll/` →  
   `%AppData%\r2modmanPlus-local\Valheim\profiles\Valheim_Client\BepInEx\plugins\`
2. In r2modman: **Disable / uninstall** `GemHunter1-ShipExploration`
3. Relog client. **Not on dedicated.**

## Config

`yanlo.ShipExplorationAll.cfg` after first run.

## Build

```bash
dotnet build custom-mods/ShipExplorationAll/ShipExplorationAll.csproj -c Release
```
