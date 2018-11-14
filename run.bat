@echo off
SET mypath=%~dp0
cmd /c "cd /d %mypath:~0,-1%\build && dotnet TungstenRssReader.dll"