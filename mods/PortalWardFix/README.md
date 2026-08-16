# Yanlo-PortalWardFix 1.0.1

Fixes **WardIsLove 3.7.2** silently blocking portal walk-through.

## Bug

`TeleportWorldTeleportPatch` denies teleport when `CheckInWardMonoscript(portal)` is true but `WardMonoscriptsINSIDE` is empty. `CheckIn` is often a false positive: WiL `IsInside` overwrites `m_radius` with the **last** ward’s radius in `m_allAreas` (a far ward at 80 poisons nearby checks).

## Install

- **Client required** (same profile as WardIsLove) — unzip folder into `BepInEx/plugins/`
- Dedicated: optional but recommended (same folder) if WiL is on the server

Requires **Azumatt.WardIsLove**.

## Not

Does not change ward radii, NoTeleport toggles, or XPortal. Does not ship debug probes.
