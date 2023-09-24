@echo off

if [%1]==[] goto USAGE
set target=%1

call Publish %target% Complex.SpecFlow.Assist 2.0.0
goto :eof

:USAGE
echo Usage:
echo PublishAll ^<local^|remote^>
echo;
