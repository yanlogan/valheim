# Sync stack cfg files from Valheim_Client into repo cfg/
param(
    [string]$ProfileName = "Valheim_Client"
)

$ErrorActionPreference = "Stop"
$RepoRoot = Split-Path -Parent $PSScriptRoot
$DestDir = Join-Path $RepoRoot "cfg"
$SrcDir = Join-Path $env:AppData "r2modmanPlus-local\Valheim\profiles\$ProfileName\BepInEx\config"

$Files = @(
    "goldenrevolver.quick_stack_store.cfg",
    "flueno.SmartContainers.cfg",
    "Azumatt.AzuCraftyBoxes.cfg",
    "valheim_plus.cfg",
    "Azumatt.AzuAntiArthriticCrafting.cfg",
    "com.inventoryux.valheim.cfg"
)

if (-not (Test-Path $SrcDir)) {
    throw "Config dir not found: $SrcDir"
}

New-Item -ItemType Directory -Force -Path $DestDir | Out-Null

foreach ($name in $Files) {
    $src = Join-Path $SrcDir $name
    if (-not (Test-Path $src)) {
        throw "Missing: $src"
    }
    Copy-Item -LiteralPath $src -Destination (Join-Path $DestDir $name) -Force
    Write-Host "OK  $name"
}

Write-Host "Synced -> $DestDir"
