wget https://github.com/Mel4221/QuickTools/raw/testing/bin/Release/QuickTools.dll -O QuickTools/QuickTools.dll
wget https://github.com/Mel4221/QuickTools/raw/testing/bin/Release/QuickTools.xml -O QuickTools/QuickTools.xml
Say  'QuickTools Downloaded Sucessfully'

bash ClownShell.Static/./build.sh
bash ClownShell.Parser/./build.sh
bash ClownShell.MainLoop/./build.sh
bash ClownShell.Init/./build.sh


#dotnet publish -r win-x64 -c Releasxe
#dotnet publish -r linux-arm64 -c Release
#<SelfContained>true</SelfContained>
#<RuntimeIdentifier>win-x64</RuntimeIdentifier>
#dotnet publish ClownShell.Init/ClownShell.Init.csproj -r $(cat platform) -o bin/Release/