@echo off

if [%1]==[] goto USAGE
set target=%1

cd ..
call Publish %target% Complex.SpecFlow.Assist 2.0.0
cd Complex.SpecFlow.Assist
goto :eof

:USAGE
echo Usage:
echo Publish ^<local^|remote^>
echo;

