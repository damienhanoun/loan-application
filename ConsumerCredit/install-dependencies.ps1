function Check-Winget {
    $wingetInstalled = $false
    try {
        # Try running winget directly
        $wingetVersion = & winget --version
        if ($wingetVersion) {
            $wingetInstalled = $true
        }
    } catch {
        Write-Host "winget is not available. Searching for winget executable..."

        # Search for winget.exe in the WindowsApps folder
        $wingetPath = Get-ChildItem -Path 'C:\Program Files\WindowsApps' -Recurse -Filter winget.exe -ErrorAction SilentlyContinue | Select-Object -First 1

        if ($wingetPath) {
            Write-Host "winget found at $($wingetPath.DirectoryName). Temporarily adding to PATH."
            $env:Path += ";$($wingetPath.DirectoryName)"
            
            # Try running winget again after adding to PATH
            try {
                $wingetVersion = & winget --version
                if ($wingetVersion) {
                    $wingetInstalled = $true
                    Write-Host "winget is now available."
                }
            } catch {
                Write-Host "Failed to run winget after adding to PATH."
            }
        } else {
            Write-Host "winget.exe not found in WindowsApps folder."
        }
    }
    return $wingetInstalled
}

# Check if winget is installed
function Install-Winget {
    if (-not (Check-Winget))
    {
        Write-Host "winget is not installed. Attempting installation via GitHub..."
        Write-Host "Attempting to install winget..."

        # Define download URL and file path
        $wingetDownloadUrl = "https://github.com/microsoft/winget-cli/releases/download/v1.8.1911/Microsoft.DesktopAppInstaller_8wekyb3d8bbwe.msixbundle"
        $destinationPath = "$env:USERPROFILE\Downloads\winget.msixbundle"

        # Check if the file already exists
        if (Test-Path $destinationPath)
        {
            Write-Host "Winget msixbundle already exists at $destinationPath. Skipping download."
        }
        else
        {
            # Attempt to download the file
            try
            {
                Write-Host "Downloading Winget from GitHub..."
                Invoke-WebRequest -Uri $wingetDownloadUrl -OutFile $destinationPath -UseBasicParsing
                Write-Host "Download complete. File saved to $destinationPath"
            }
            catch
            {
                Write-Host "Failed to download Winget. Please check your internet connection or try downloading the file manually."
                return
            }
        }

        # Install the MSIX bundle using Add-AppxPackage
        try
        {
            Write-Host "Installing Winget using Add-AppxPackage..."
            Add-AppxPackage -Path $destinationPath
            Write-Host "Installation completed."
        }
        catch
        {
            Write-Host "Failed to install winget. Please install manually."
            return
        }

        # After installation, search for winget.exe and add to PATH
        Write-Host "Searching for winget.exe after installation..."
        $wingetPath = Get-ChildItem -Path 'C:\Program Files\WindowsApps' -Recurse -Filter winget.exe -ErrorAction SilentlyContinue | Select-Object -First 1

        if ($wingetPath)
        {
            Write-Host "winget found at $( $wingetPath.DirectoryName ). Adding to PATH."
            $env:Path += ";$( $wingetPath.DirectoryName )"
        }
        else
        {
            Write-Host "winget.exe not found after installation."
        }
    }
    else
    {
        Write-Host "winget is already installed."
    }
}

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

function Install-NSwag {
    Write-Host "Installing nswag"
    dotnet tool install --global NSwag.ConsoleCore
}

# Check if Chocolatey is installed
function Install-Chocolatey {
    if (!(Get-Command choco -ErrorAction SilentlyContinue))
    {
        Write-Host "Chocolatey not found. Installing Chocolatey..."

        # Download and install Chocolatey
        Set-ExecutionPolicy Bypass -Scope Process -Force
        [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072
        Invoke-Expression ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))

        Write-Host "Chocolatey installed successfully."
    }
    else
    {
        Write-Host "Chocolatey is already installed."
    }
}

# Function to add certificate to Trusted Root Certification Authorities
function Generate-And-Trust-Certificates {
	$baseDir = $PSScriptRoot

	# Step 1: Set developper certificate for local api
    Write-Host "Generate API certificate"    
	dotnet dev-certs https --clean
	dotnet dev-certs https --trust

    # Step 2: Generate certificate for angular
    Write-Host "Generate Angular certificate"
    $clientProjectPath = Join-Path $baseDir "Acquisition.WebApplication\loanApplicationJourney.angular\ssl"
    
    mkcert -install
    Set-Location $clientProjectPath
    mkcert localhost
    
    Set-Location $baseDir
}

function Install-MkCert {
    Write-Host "Installing MkCert"
    choco install mkcert -y
}

function Npm-Install {
    $baseDir = $PSScriptRoot
    $clientProjectPath = Join-Path $baseDir "Acquisition.WebApplication\loanApplicationJourney.angular"
    
    if (Test-Path "$clientProjectPath\package.json") {
        Write-Host "Navigating to the Angular project folder..."
        Set-Location $clientProjectPath

        Write-Host "Installing necessary dependencies..."
        npm install
        Write-Host "Dependencies installed."
    } else {
        Write-Host "package.json not found in the Angular project directory."
        exit 1
    }
    
    Set-Location $baseDir
}

# Main script logic
Write-Host "Starting installation of dependencies..."

# Install dependencies
Install-Chocolatey
Install-MkCert
Install-Winget
Install-NSwag
Install-Net8
Install-DockerDesktop
Npm-Install
Generate-And-Trust-Certificates

Write-Host "All checks complete."
