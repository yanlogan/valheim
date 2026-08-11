#Requires -Version 5.1
<#
.SYNOPSIS
  Zip dist/Yanlo-* and create GitHub Release cycle-YYYY-MM-DD.

.PARAMETER Tag
  Release tag, e.g. cycle-2026-08-11

.PARAMETER NotesFile
  Markdown file for release body (archived changelog).

.PARAMETER SkipBuild
  Do not run build.ps1 first.

.EXAMPLE
  .\scripts\release.ps1 -Tag cycle-2026-08-11 -NotesFile .\changelogs\2026-08-11_inventory-stack.md
#>
param(
    [Parameter(Mandatory = $true)]
    [string] $Tag,

    [Parameter(Mandatory = $true)]
    [string] $NotesFile,

    [switch] $SkipBuild
)

$ErrorActionPreference = "Stop"
$Root = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)
Set-Location $Root

if ($Tag -notmatch '^cycle-\d{4}-\d{2}-\d{2}') {
    throw "Tag must look like cycle-YYYY-MM-DD (got: $Tag)"
}
if (-not (Test-Path $NotesFile)) {
    throw "Notes file not found: $NotesFile"
}

$gh = Get-Command gh -ErrorAction SilentlyContinue
if (-not $gh) {
    $candidates = @(
        "$env:ProgramFiles\GitHub CLI\gh.exe",
        "${env:ProgramFiles(x86)}\GitHub CLI\gh.exe",
        "$env:LOCALAPPDATA\GitHubCLI\gh.exe"
    )
    foreach ($c in $candidates) {
        if (Test-Path $c) { $gh = @{ Source = $c }; break }
    }
}
if (-not $gh) {
    throw "GitHub CLI (gh) not found. Install: https://cli.github.com/ then gh auth login"
}
$GhExe = if ($gh.Source) { $gh.Source } else { $gh.Path }

if (-not $SkipBuild) {
    Write-Host "==> build.ps1" -ForegroundColor Cyan
    & "$Root\scripts\build.ps1"
}

$Dist = Join-Path $Root "dist"
$packs = @(Get-ChildItem -Path $Dist -Directory -Filter "Yanlo-*" -ErrorAction SilentlyContinue)
if (-not $packs -or $packs.Count -eq 0) {
    throw "No dist/Yanlo-* folders. Run build.ps1 first."
}

$OutDir = Join-Path $Root "dist"
$ZipName = "YanloMods-$Tag.zip"
$ZipPath = Join-Path $OutDir $ZipName
if (Test-Path $ZipPath) { Remove-Item -Force $ZipPath }

Write-Host "==> Zipping $($packs.Count) pack(s) -> $ZipPath" -ForegroundColor Cyan
# Compress relative Yanlo-* folders into one zip
Push-Location $Dist
try {
    Compress-Archive -Path ($packs.Name) -DestinationPath $ZipPath -Force
}
finally {
    Pop-Location
}

$notesAbs = (Resolve-Path $NotesFile).Path
$zipAbs = (Resolve-Path $ZipPath).Path

Write-Host "==> gh release create $Tag" -ForegroundColor Cyan
& $GhExe release create $Tag `
    --title $Tag `
    --notes-file $notesAbs `
    $zipAbs

if ($LASTEXITCODE -ne 0) {
    throw "gh release create failed ($LASTEXITCODE)"
}

Write-Host "OK: https://github.com/yanlogan/valheim/releases/tag/$Tag" -ForegroundColor Green
Write-Host "Asset: $ZipName" -ForegroundColor Green
Write-Host "Update PENDING_DISCORD + archived notes Links section with this URL." -ForegroundColor Yellow
