#Requires -Version 5.1
<#
.SYNOPSIS
  Build active Yanlo mods into dist/Yanlo-<Name>/
#>
$ErrorActionPreference = "Stop"
$Root = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)
$Dist = Join-Path $Root "dist"

$Mods = @(
    @{ Name = "ChestUnloadButton"; Project = "mods\ChestUnloadButton\ChestUnloadButton.csproj"; Dll = "ChestUnloadButton.dll" },
    @{ Name = "ShipExplorationAll"; Project = "mods\ShipExplorationAll\ShipExplorationAll.csproj"; Dll = "ShipExplorationAll.dll" },
    @{ Name = "CraftyBoxesDrawerFix"; Project = "mods\CraftyBoxesDrawerFix\CraftyBoxesDrawerFix.csproj"; Dll = "CraftyBoxesDrawerFix.dll" }
)

New-Item -ItemType Directory -Force -Path $Dist | Out-Null

foreach ($m in $Mods) {
    $proj = Join-Path $Root $m.Project
    $modDir = Split-Path -Parent $proj
    Write-Host "==> Building $($m.Name)..." -ForegroundColor Cyan
    & dotnet build $proj -c Release --nologo
    if ($LASTEXITCODE -ne 0) { throw "dotnet build failed: $($m.Name)" }

    $outDir = Join-Path $Dist ("Yanlo-" + $m.Name)
    if (Test-Path $outDir) { Remove-Item -Recurse -Force $outDir }
    New-Item -ItemType Directory -Force -Path $outDir | Out-Null

    $dllSrc = Join-Path $modDir "bin\$($m.Dll)"
    if (-not (Test-Path $dllSrc)) {
        # SDK sometimes nests under bin\Release\
        $alt = Join-Path $modDir "bin\Release\$($m.Dll)"
        if (Test-Path $alt) { $dllSrc = $alt }
        else { throw "DLL not found: $dllSrc" }
    }
    Copy-Item $dllSrc $outDir
    Copy-Item (Join-Path $modDir "manifest.json") $outDir
    $readme = Join-Path $modDir "README.md"
    if (Test-Path $readme) { Copy-Item $readme $outDir }

    Write-Host "    -> $outDir" -ForegroundColor Green
}

Write-Host "Done. Run .\scripts\install-client.ps1 to copy into Valheim_Client plugins." -ForegroundColor Green
