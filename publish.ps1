dotnet publish .\app\cepix1234.WgetTracker.App -r linux-x64 -p:PublishSingleFile=true --self-contained true
dotnet publish .\app\cepix1234.WgetTracker.App -r win-x64 -p:PublishSingleFile=true --self-contained true
dotnet publish .\app\cepix1234.WgetTracker.App -r linux-arm64 -p:PublishSingleFile=true --self-contained true
dotnet publish .\app\cepix1234.WgetTracker.App -r linux/amd64 -p:PublishSingleFile=true --self-contained true


$ReleasePath = ".\Release"
$ReleaseWinPath = "$ReleasePath\Windows"
$ReleaseLinuxPath = "$ReleasePath\Linux"
$ReleaseLinuxArmPath = "$ReleasePath\LinuxArm"

if(![System.IO.File]::Exists($ReleasePath)) {
    New-Item -ItemType Directory -Force -Path $ReleasePath
    New-Item -ItemType Directory -Force -Path $ReleaseWinPath
    New-Item -ItemType Directory -Force -Path $ReleaseLinuxPath
    New-Item -ItemType Directory -Force -Path $ReleaseLinuxArmPath
}

if(![System.IO.File]::Exists($ReleaseWinPath)) {
    New-Item -ItemType Directory -Force -Path $ReleaseWinPath
}

if(![System.IO.File]::Exists($ReleaseLinuxPath)) {
    New-Item -ItemType Directory -Force -Path $ReleaseLinuxPath
}


if(![System.IO.File]::Exists($ReleaseLinuxArmPath)) {
    New-Item -ItemType Directory -Force -Path $ReleaseLinuxArmPath
}

Copy-Item .\app\cepix1234.WgetTracker.App\bin\Release\net8.0\win-x64\publish\cepix1234.WgetTracker.App.exe $ReleaseWinPath\cepix1234.WgetTracker.exe
Copy-Item .\app\cepix1234.WgetTracker.App\bin\Release\net8.0\win-x64\publish\appsettings.json $ReleaseWinPath\appsettings.json
Copy-Item .\app\cepix1234.WgetTracker.App\bin\Release\net8.0\linux-x64\publish\cepix1234.WgetTracker.App $ReleaseLinuxPath\cepix1234.WgetTracker
Copy-Item .\app\cepix1234.WgetTracker.App\bin\Release\net8.0\linux-x64\publish\appsettings.json $ReleaseLinuxPath\appsettings.json
Copy-Item .\app\cepix1234.WgetTracker.App\bin\Release\net8.0\linux-arm64\publish\cepix1234.WgetTracker.App $ReleaseLinuxArmPath\cepix1234.WgetTracker
Copy-Item .\app\cepix1234.WgetTracker.App\bin\Release\net8.0\linux-arm64\publish\appsettings.json $ReleaseLinuxArmPath\appsettings.json