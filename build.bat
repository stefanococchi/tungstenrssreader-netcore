@echo off
SET mypath=%~dp0
dotnet restore TungstenRssReader.sln
dotnet build --configuration Release --no-restore
dotnet test --configuration Release --no-build --no-restore TungstenRssReader.UnitTests\TungstenRssReader.UnitTests.csproj
cmd /c "pushd %mypath:~0,-1%\TungstenRssReader\AngularClientApp && ng build --prod && popd"
dotnet publish --configuration Release --no-build --no-restore --output %mypath:~0,-1%\build TungstenRssReader\TungstenRssReader.csproj
echo Build OK