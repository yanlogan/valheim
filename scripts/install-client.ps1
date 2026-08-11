#Requires -Version 5.1
<#
.SYNOPSIS
  Copy dist/Yanlo-* into r2modman Valheim_Client BepInEx/plugins (robocopy mirror per mod).
#>
$ErrorActionPreference = "Stop"
$Root = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)
$Dist = Join-Path $Root "dist"
$Plugins = Join-Path $env:APPDATA "r2modmanPlus-local\Valheim\profiles\Valheim_Client\BepInEx\plugins"

if (-not (Test-Path $Plugins)) {
    throw "Client plugins folder not found: $Plugins"
}
if (-not (Test-Path $Dist)) {
    throw "dist/ missing. Run .\scripts\build.ps1 first."
}

$packs = Get-ChildItem -Path $Dist -Directory -Filter "Yanlo-*"
if (-not $packs) {
    throw "No Yanlo-* folders in dist/. Run .\scripts\build.ps1 first."
}

foreach ($pack in $packs) {
    $dest = Join-Path $Plugins $pack.Name
    Write-Host "==> Installing $($pack.Name) -> $dest" -ForegroundColor Cyan
    # /E copy subdirs, /IS /IT include same+tweaked, /NFL /NDL quieter, /NJH /NJS
    & robocopy $pack.FullName $dest /E /IS /IT /NFL /NDL /NJH /NJS /R:2 /W:1 | Out-Null
    $code = $LASTEXITCODE
    # robocopy: 0-7 success-ish
    if ($code -ge 8) { throw "robocopy failed ($code) for $($pack.Name)" }
    Write-Host "    OK" -ForegroundColor Green
}

Write-Host "Installed $($packs.Count) pack(s) into Valheim_Client plugins." -ForegroundColor Green
