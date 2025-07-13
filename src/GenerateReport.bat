@echo off
echo =================================================
echo    SpecFlow BDD Testing Report Generator
echo =================================================
echo.

echo [1/3] Executing SpecFlow Tests...
dotnet test --logger "trx;LogFileName=SpecFlowTestResults.trx" --results-directory TestResults

echo.
echo [2/3] Test Results:
findstr /C:"通過" TestResults\SpecFlowTestResults.trx >nul 2>&1
if %errorlevel% == 0 (
    echo ✅ All tests passed successfully!
) else (
    echo ❌ Some tests failed. Check the test output above.
)

echo.
echo [3/3] HTML Report generated: SpecFlowReport.html
echo You can open this file in your web browser to view the detailed test report.

echo.
echo =================================================
echo Opening HTML Report in default browser...
start SpecFlowReport.html

pause
