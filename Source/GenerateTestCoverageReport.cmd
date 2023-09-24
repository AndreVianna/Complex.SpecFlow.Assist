@echo off
setlocal enableDelayedExpansion

echo;
echo Starting report generation...

if "%1" equ "-h" goto USAGE
if "%1" equ "--help" goto USAGE
set base_folder=%1
if not defined base_folder set base_folder=%cd%
if not exist %base_folder% goto NOT_FOUND
cd %base_folder%
set base_folder=%cd%
if not "%base_folder:~-1%" equ "\" set base_folder=%base_folder%\

echo;
echo Searching for test projects in "%base_folder:~0,-1%"...

set reports=
for /r %%f in (*.UnitTests.csproj) do (
	set project_path=%%~dpf
	set project_name=%%~nf
	set project_file=%%~nxf
	set log_file=%%~nf.log
	echo;
	echo Running !project_path!!project_name!...
	echo;
	echo %base_folder%
	call powershell "dotnet test !project_path!!project_file! --collect 'XPlat Code Coverage' -s '%base_folder%.runsettings' | tee !project_path!!log_file!"
	echo =======================================================================================================
	echo;
	echo Parsing !project_path!!log_file! file...
	for /f "tokens=1 delims=" %%i in ('"type !project_path!!log_file! | findstr /c:"coverage.cobertura.xml""') do ( 
		set line=%%i
		if defined reports (
			set reports=!reports!;!line: =!
		) else (
			set reports=!line: =!
		)
	)

	del !project_path!!log_file!
	echo =======================================================================================================
	echo;
)
if not defined reports goto NO_PROJECTS

set datetime_subfolder=
for /f "tokens=*" %%i in ('powershell -noninteractive -command "(Get-Date -UFormat '%%Y%%m%%d_%%H%%M%%S').toString()"') do (
	set datetime_subfolder=%%i
)
set report_folder=%base_folder%CoverageReports\%datetime_subfolder%
echo Generating report...
reportgenerator "-reports:%reports%" "-targetdir:%report_folder%" "-reporttypes:HtmlSummary;HtmlInline_AzurePipelines_Dark" -fileFilters:-*.g.cs
echo;
echo Coverage report generated in "%report_folder%".
	echo =======================================================================================================
echo;
exit

:NO_PROJECTS
echo;
echo No test project found.
goto USAGE

:NOT_FOUND
echo;
echo The path "%1" was not found.
goto USAGE

:USAGE
echo;
echo Usage: GenerateTestCoverageReport [Options] [SolutionPath]
echo;
echo Options:
echo   -h, --help             Show this help information and exit.
echo;
echo Parameters:
echo   SolutionPath           Define the base path of the solution folder containing the test projects.
echo                          If omitted the command uses the current folder.
echo;
exit
