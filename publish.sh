dotnet publish ./app/cepix1234.WgetTracker.App -r linux-x64 -p:PublishSingleFile=true --self-contained true
dotnet publish ./app/cepix1234.WgetTracker.App -r win-x64 -p:PublishSingleFile=true --self-contained true
dotnet publish ./app/cepix1234.WgetTracker.App -r linux-arm64 -p:PublishSingleFile=true --self-contained true

ReleasePath = "./Release"
ReleaseWinPath = "${ReleasePath}/Windows"
ReleaseLinuxPath = "${ReleasePath}/Linux"
ReleaseLinuxArmPath = "${ReleasePath}/LinuxArm"

if [ ! -d "$ReleasePath"]; then
  mkdir "$ReleasePath"
  mkdir "$ReleaseWinPath"
  mkdir "$ReleaseLinuxPath"
  mkdir "$ReleaseLinuxArmPath"
fi

if [ ! -d "$ReleaseWinPath"]; then
  mkdir "$ReleaseWinPath"
fi

if [ ! -d "$ReleaseLinuxPath"]; then
  mkdir "$ReleaseLinuxPath"
fi

if [ ! -d "$ReleaseLinuxArmPath"]; then
  mkdir "$ReleaseLinuxArmPath"
fi

cp ./app/cepix1234.WgetTracker.App/bin/Release/net8.0/win-x64/publish/cepix1234.WgetTracker.App.exe "${ReleaseWinPath}/cepix1234.WgetTracker.exe"
cp ./app/cepix1234.WgetTracker.App/bin/Release/net8.0/win-x64/publish/appsettings.json "${ReleaseWinPath}/appsettings.json"
cp ./app/cepix1234.WgetTracker.App/bin/Release/net8.0/linux-x64\publish/cepix1234.WgetTracker.App "${ReleaseLinuxPath}/cepix1234.WgetTracker"
cp ./app/cepix1234.WgetTracker.App/bin/Release/net8.0/linux-x64\publish/appsettings.json "${ReleaseLinuxPath}/appsettings.json"
cp ./app/cepix1234.WgetTracker.App/bin/Release/net8.0/linux-arm64\publish/cepix1234.WgetTracker.App "${ReleaseLinuxArmPath}/cepix1234.WgetTracker"
cp ./app/cepix1234.WgetTracker.App/bin/Release/net8.0/linux-arm64\publish/appsettings.json "${ReleaseLinuxArmPath}/appsettings.json"