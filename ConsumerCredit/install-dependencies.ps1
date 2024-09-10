# Function to check if a specific .NET SDK version is installed
function Check-Net8 {
    $net8Installed = $false
    try {
        $sdks = & dotnet --list-sdks
        if ($sdks -match '8\.\d+\.\d+') {
            $net8Installed = $true
        }
    } catch {
        Write-Host ".NET SDK is not installed."
    }
    return $net8Installed
}

# Function to check if Docker Desktop is installed
function Check-DockerDesktop {
    $dockerInstalled = $false
    try {
        $installedPackages = winget list
        if ($installedPackages -match 'Docker Desktop') {
            $dockerInstalled = $true
        }
    } catch {
        Write-Host "Docker Desktop is not installed."
    }
    return $dockerInstalled
}

# Install .NET 8 if not installed
function Install-Net8 {
    Write-Host "Checking for .NET 8 SDK..."
    if (-not (Check-Net8)) {
        Write-Host ".NET 8 SDK is not installed. Installing .NET 8 SDK..."
        winget install Microsoft.DotNet.SDK.8
        Write-Host ".NET 8 SDK installation complete."
    } else {
        Write-Host ".NET 8 SDK is already installed."
    }
}

# Install Docker Desktop if not installed
function Install-DockerDesktop {
    Write-Host "Checking for Docker Desktop..."
    if (-not (Check-DockerDesktop)) {
        Write-Host "Docker Desktop is not installed. Installing Docker Desktop..."
        winget install Docker.DockerDesktop
        Write-Host "Docker Desktop installation complete."
    } else {
        Write-Host "Docker Desktop is already installed."
    }
}

# Main script logic
Write-Host "Starting installation check for .NET 8 SDK and Docker Desktop..."

# Ensure winget is available
if (-not (Get-Command winget -ErrorAction SilentlyContinue)) {
    Write-Host "winget is not available. Please install winget manually."
    exit 1
}

# Check and install .NET 8 SDK
Install-Net8

# Check and install Docker Desktop
Install-DockerDesktop

Write-Host "All checks complete."
