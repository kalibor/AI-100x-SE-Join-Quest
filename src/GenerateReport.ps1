# SpecFlow BDD Testing Report Generator
Write-Host "=================================================" -ForegroundColor Cyan
Write-Host "    SpecFlow BDD Testing Report Generator" -ForegroundColor Cyan
Write-Host "=================================================" -ForegroundColor Cyan
Write-Host ""

Write-Host "[1/3] Executing SpecFlow Tests..." -ForegroundColor Yellow
$testResult = dotnet test --logger "trx;LogFileName=SpecFlowTestResults.trx" --results-directory TestResults

Write-Host ""
Write-Host "[2/3] Test Results:" -ForegroundColor Yellow
if ($LASTEXITCODE -eq 0) {
    Write-Host "✅ All tests passed successfully!" -ForegroundColor Green
} else {
    Write-Host "❌ Some tests failed. Check the test output above." -ForegroundColor Red
}

Write-Host ""
Write-Host "[3/3] HTML Report generated: SpecFlowReport.html" -ForegroundColor Yellow
Write-Host "You can open this file in your web browser to view the detailed test report." -ForegroundColor White

Write-Host ""
Write-Host "=================================================" -ForegroundColor Cyan
Write-Host "Opening HTML Report in default browser..." -ForegroundColor Green
Start-Process "SpecFlowReport.html"

Write-Host ""
Write-Host "Report generation completed!" -ForegroundColor Green
Read-Host "Press Enter to exit"
