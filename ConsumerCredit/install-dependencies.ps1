# Function to check if winget is available
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

# Function to install winget (if needed) and ensure its path is added
function Install-Winget {
    Write-Host "Attempting to install winget..."

    # Define download URL and file path
    $wingetDownloadUrl = "https://github.com/microsoft/winget-cli/releases/download/v1.8.1911/Microsoft.DesktopAppInstaller_8wekyb3d8bbwe.msixbundle"
    $destinationPath = "$env:USERPROFILE\Downloads\winget.msixbundle"

    # Check if the file already exists
    if (Test-Path $destinationPath) {
        Write-Host "Winget msixbundle already exists at $destinationPath. Skipping download."
    } else {
        # Attempt to download the file
        try {
            Write-Host "Downloading Winget from GitHub..."
            Invoke-WebRequest -Uri $wingetDownloadUrl -OutFile $destinationPath -UseBasicParsing
            Write-Host "Download complete. File saved to $destinationPath"
        } catch {
            Write-Host "Failed to download Winget. Please check your internet connection or try downloading the file manually."
            return
        }
    }

    # Install the MSIX bundle using Add-AppxPackage
    try {
        Write-Host "Installing Winget using Add-AppxPackage..."
        Add-AppxPackage -Path $destinationPath
        Write-Host "Installation completed."
    } catch {
        Write-Host "Failed to install winget. Please install manually."
        return
    }

    # After installation, search for winget.exe and add to PATH
    Write-Host "Searching for winget.exe after installation..."
    $wingetPath = Get-ChildItem -Path 'C:\Program Files\WindowsApps' -Recurse -Filter winget.exe -ErrorAction SilentlyContinue | Select-Object -First 1

    if ($wingetPath) {
        Write-Host "winget found at $($wingetPath.DirectoryName). Adding to PATH."
        $env:Path += ";$($wingetPath.DirectoryName)"
    } else {
        Write-Host "winget.exe not found after installation."
    }
}

# Check if winget is installed
if (-not (Check-Winget)) {
    Write-Host "winget is not installed. Attempting installation via GitHub..."
    Install-Winget
} else {
    Write-Host "winget is already installed."
}




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

# Function to add certificate to Trusted Root Certification Authorities
function Generate-And-Trust-Certificates {    
    # Define the base directory (where this script is located)
	$baseDir = $PSScriptRoot
	$serverProjectPath = Join-Path $baseDir "Acquisition.WebApplication\Acquisition.WebApplication.Server"

	# Set developper certificate for local api
	dotnet dev-certs https --clean
	dotnet dev-certs https --trust

	# Step 1: Navigate to the Angular client project folder and install dependencies
	$clientProjectPath = Join-Path $baseDir "Acquisition.WebApplication\acquisition.webapplication.client"

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

	# Step 2: Navigate to the server project and start the server, which also starts Angular
	if (Test-Path "$serverProjectPath\Acquisition.WebApplication.Server.csproj") {
		Write-Host "Navigating to the Server project folder..."
		Set-Location $serverProjectPath

		Write-Host "Starting Acquisition.WebApplication.Server project..."
		
		# Start the server project (which will also start Angular)
		# Assumes you're using .NET SDK, adjust the command if using different tooling.
		$serverProcess = Start-Process "dotnet" -ArgumentList "run" -PassThru -NoNewWindow -ErrorAction Stop

		# Give the server some time to start and Angular to generate the certificate (e.g., 10-15 seconds)
		Start-Sleep -Seconds 15

		# Step 3: Stop the server process if needed (optional)
		if ($serverProcess.HasExited) {
			Write-Host "Server process has already exited."
		} else {
			Write-Host "Stopping the server..."
			Stop-Process -Id $serverProcess.Id
		}
	} else {
		Write-Host "Server project not found in the directory."
		exit 1
	}

	# Step 4: Add the generated certificate to Trusted Root Certification Authorities
	$certPath = "$env:APPDATA\ASP.NET\https\acquisition.webapplication.client.pem"
	if (Test-Path $certPath) {
		Write-Host "Adding SSL certificate to Trusted Root Certification Authorities..."
		certutil -addstore "Root" $certPath
		Write-Host "Certificate added successfully."
	} else {
		Write-Host "Certificate not found at $certPath. Make sure the certificate is generated."
	}
	
	Set-Location $baseDir
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

# Trust the certificate if it exists
Generate-And-Trust-Certificates

Write-Host "All checks complete."
