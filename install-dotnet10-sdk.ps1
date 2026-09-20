$ErrorActionPreference = "Stop"

Write-Host ""
Write-Host "=== .NET SDK cleanup and installation ==="
Write-Host ""

# Check Administrator privileges

$identity = [Security.Principal.WindowsIdentity]::GetCurrent()
$principal = New-Object Security.Principal.WindowsPrincipal($identity)

if (-not $principal.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)) {
Write-Host "ERROR: Run PowerShell as Administrator."
exit 1
}

# SDK location

$sdkPath = "C:\Program Files\dotnet\sdk"

Write-Host "SDK directory:"
Write-Host $sdkPath
Write-Host ""

# Show currently installed SDKs

Write-Host "Currently installed SDKs:"
dotnet --list-sdks

Write-Host ""
Write-Host "Currently installed runtimes:"
dotnet --list-runtimes

Write-Host ""
Write-Host "Press ENTER to remove the SDK directories."
Read-Host

# Verify SDK directory exists

if (-not (Test-Path -LiteralPath $sdkPath)) {
Write-Host "ERROR: SDK directory was not found."
exit 1
}

# Get SDK directories

$sdkDirectories = Get-ChildItem -LiteralPath $sdkPath -Directory

# Remove SDK directories only

foreach ($sdkDirectory in $sdkDirectories) {
Write-Host "Removing SDK directory: " $sdkDirectory.Name
Remove-Item -LiteralPath $sdkDirectory.FullName -Recurse -Force
}

Write-Host ""
Write-Host "SDK cleanup completed."

# Install .NET 10 SDK

Write-Host ""
Write-Host "Installing .NET 10 SDK..."
Write-Host ""

winget install --id Microsoft.DotNet.SDK.10 --source winget --accept-source-agreements --accept-package-agreements

Write-Host ""
Write-Host "=== Final verification ==="
Write-Host ""

Write-Host "Installed SDKs:"
dotnet --list-sdks

Write-Host ""
Write-Host "Installed runtimes:"
dotnet --list-runtimes

Write-Host ""
Write-Host "=== Finished ==="
Write-Host ""
Write-Host "SDK directories were removed and .NET 10 SDK was installed."
Write-Host "Runtimes were not targeted."
