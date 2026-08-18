# Yanlo-PortalWardFix 1.1.0

Fixes **WardIsLove 3.7.2** using the last ward’s radius for every `IsInside` check.

## Bug

`WardMonoscript.IsInside` overwrites `m_radius` with each ward in `m_allAreas` and then compares against the **last** one. Chests, doors, stations, and portals then look “inside” a far ward. `CustomCheck.CheckAccess` takes the first such hit → friends cannot open their own chests after a portal / zone reload. Host is fine (`AdminAutoPerm`).

`TeleportWorldTeleportPatch` also false-denies walk-through when `CheckIn` is a false positive and `WardMonoscriptsINSIDE` is empty.

## What 1.1.0 does

- Postfix on `IsInside`: `DistanceXZ < this.GetWardRadius() + extra` (does not skip WiL’s collider side effects).
- Teleport force-allow **only** if the portal is honestly outside every enabled ward. Does **not** bypass per-ward NoTeleport.

## Install

- **Client required** (same profile as WardIsLove) — unzip folder into `BepInEx/plugins/`
- Dedicated: recommended (same folder) if WiL is on the server

Requires **Azumatt.WardIsLove**.

## Not

Does not change ward radii, XPortal, or overlapping-ward `CustomCheck` first-match. Does not ship debug probes.
