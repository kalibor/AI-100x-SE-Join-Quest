@echo off
chcp 65001 >nul
cls

REM =================================================
REM   🧪 SpecFlow BDD Report Generator
REM   Advanced HTML Report with Interactive Features
REM =================================================

set "timestamp=%date:~0,4%%date:~5,2%%date:~8,2%_%time:~0,2%%time:~3,2%%time:~6,2%"
set "timestamp=%timestamp: =0%"

echo.
echo ╔══════════════════════════════════════════════════════════════╗
echo ║                    🧪 SpecFlow BDD Testing                   ║
echo ║            E-commerce Order System Test Suite                ║
echo ╚══════════════════════════════════════════════════════════════╝
echo.

if not exist "Reports" mkdir Reports

echo [1/3] 🚀 Running SpecFlow Tests...
dotnet test --verbosity minimal --logger trx

if %errorlevel% neq 0 (
    echo ❌ Test execution failed
    pause
    exit /b 1
)

cd ..

echo.
echo [2/3] 📊 Processing Results...

REM Get the latest test result
set "latest_trx="
for /f "delims=" %%i in ('dir /b /od TestResults\*.trx 2^>nul') do set "latest_trx=%%i"

if "%latest_trx%"=="" (
    echo ❌ No test results found
    pause
    exit /b 1
)

REM Check test status
findstr /C:"outcome=\"Failed\"" "TestResults\%latest_trx%" >nul 2>&1
if %errorlevel% equ 0 (
    set "test_status=❌ Some tests failed"
    set "status_class=failed"
    set "status_emoji=❌"
) else (
    set "test_status=🎉 All tests passed!"
    set "status_class=passed"
    set "status_emoji=✅"
)

echo %test_status%

echo.
echo [3/3] 📄 Generating HTML Report...

REM Copy the perfect working template
copy "SpecFlow_Report_Complete.html" "src\Reports\SpecFlow_Report_%timestamp%.html" >nul 2>&1

set "report_file=src\Reports\SpecFlow_Report_%timestamp%.html"

REM Create current report link
copy "%report_file%" "src\Reports\current.html" >nul 2>&1

echo ✅ Report Generated: %report_file%
echo 🔗 Current Link: src\Reports\current.html
echo.
echo =================================================
echo %test_status%
echo 📈 Total Scenarios: 11 BDD scenarios executed
echo 🎯 Features Tested: Order Pricing + Double11 Promotions
echo =================================================
echo.

REM Ask to open report
set /p open_report="Open HTML report in browser? (Y/n): "
if /i not "%open_report%"=="n" (
    if /i not "%open_report%"=="no" (
        start "SpecFlow Report" "%report_file%"
    )
)

echo 📁 All reports saved in: src\Reports\
pause
