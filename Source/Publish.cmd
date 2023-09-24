@echo off

if [%1]==[] goto USAGE
if [%2]==[] goto USAGE
if [%3]==[] goto USAGE
set target=%1
set project=%2
set version=%3
set packageName=%4
if [%packageName%]==[] set packageName=%project%

@echo [92m%project% package...[0m

cd %project%

@echo [93mRemove existing packages...[0m
rmdir /Q /S %userprofile%\.nuget\packages\%project%\%version%
nuget delete %project% %version% -source c:\nuget\packages -noninteractive

@echo [93mBuild project...[0m
dotnet build -c Release

@echo [93mPack project...[0m
dotnet pack -c Release

if [%target%]==[local] (
	@echo [93mPublish package locally...[0m
	nuget add pkgs\Release\%packageName%.%version%.nupkg -source c:\nuget\packages
)

if [%target%]==[remote] (
	@echo [93mPublish package remotely...[0m
	nuget push pkgs\Release\%packageName%.%version%.nupkg %NuGetApiKey% -Source https://api.nuget.org/v3/index.json
)


cd ..

@echo [92mDone...[0m
goto :eof

:USAGE
echo Usage:
echo Publish ^<local^|remote^> ^<project^> ^<version^>
echo;
