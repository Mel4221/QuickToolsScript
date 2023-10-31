
echo "You must add this properties in order for it to work"; 
echo "<PropertyGroup>";
echo "<PublishAot>true</PublishAot>";
echo "</PropertyGroup>";

start dotnet publish -r win-x64 -c Release
start dotnet publish -r linux-arm64 -c LinuxRelease