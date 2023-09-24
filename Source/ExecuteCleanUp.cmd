@echo off
echo [93mCleaning obj folders...[0m
for /f "tokens=*" %%g in ('dir /b /ad /s obj') do (
	@echo "Removing %%g"
	rmdir /s /q "%%g"
)
echo [93mCleaning bin folders...[0m
for /f "tokens=*" %%g in ('dir /b /ad /s bin') do (
	@echo "Removing %%g"
	rmdir /s /q "%%g"
)

echo [93mCleaning Test Results...[0m
for /f "tokens=*" %%g in ('dir /b /ad /s TestResults') do (
	@echo "Removing %%g"
	rmdir /s /q "%%g"
)
rmdir /s /q TestResults

echo [93mCleaning Coverage files...[0m
for /f "tokens=*" %%g in ('dir /b /ad /s CoverageReports') do (
	@echo "Removing %%g"
	rmdir /s /q "%%g"
)
rmdir /s /q CoverageReports

pause
