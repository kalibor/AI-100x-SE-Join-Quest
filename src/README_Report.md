# SpecFlow HTML 報告生成指南

## 📄 概述
本專案使用 SpecFlow 進行 BDD (行為驅動開發) 測試，並提供了多種方式來生成 HTML 測試報告。

## 🚀 快速開始

### 方法一：使用批次檔案 (推薦)
```bash
.\GenerateReport.bat
```

### 方法二：使用 PowerShell 腳本
```powershell
.\GenerateReport.ps1
```

### 方法三：手動生成
```bash
# 1. 執行測試
dotnet test --logger "trx;LogFileName=SpecFlowTestResults.trx" --results-directory TestResults

# 2. 打開 HTML 報告
start SpecFlowReport.html
```

## 📊 報告內容

生成的 HTML 報告包含以下內容：

- **測試執行摘要**: 總測試數、通過數、失敗數、成功率
- **詳細測試情境**: 每個 Scenario 的詳細步驟和結果
- **視覺化呈現**: 使用顏色和圖標清楚顯示測試狀態
- **表格資料**: 測試中使用的輸入和預期輸出資料

## 🎯 測試情境

目前包含的測試情境：

1. **Single product without promotions** - 無促銷活動的單一商品
2. **Threshold discount applies** - 閾值折扣促銷活動
3. **Buy-one-get-one for cosmetics - multiple products** - 化妝品買一送一(多商品)
4. **Buy-one-get-one for cosmetics - same product twice** - 化妝品買一送一(同商品多個)
5. **Buy-one-get-one for cosmetics - mixed categories** - 化妝品買一送一(混合類別)
6. **Multiple promotions stacked** - 多重促銷活動疊加

## 📁 檔案結構

```
src/
├── OrderModule/                    # 主要業務邏輯
│   └── OrderService.cs            # 訂單處理服務
├── OrderModule.Tests/              # 測試專案
│   ├── Features/
│   │   └── OrderPricing.feature    # BDD 功能檔案
│   └── StepDefinitions/
│       └── OrderPricingSteps.cs    # 步驟定義
├── TestResults/                    # 測試結果檔案
├── SpecFlowReport.html             # HTML 報告
├── GenerateReport.bat              # Windows 批次檔案
└── GenerateReport.ps1              # PowerShell 腳本
```

## 🔧 技術堆疊

- **測試框架**: SpecFlow 3.9.74 + NUnit 3.14.0
- **開發語言**: C# (.NET 6.0)
- **報告工具**: 自訂 HTML 報告 + SpecFlow+ LivingDoc

## 📝 注意事項

1. 確保已安裝 .NET 6.0 SDK
2. 第一次執行前，請先執行 `dotnet restore` 來還原套件
3. HTML 報告會自動在預設瀏覽器中打開
4. 測試結果檔案會儲存在 `TestResults/` 目錄中

## 🔄 持續整合

可以將報告生成整合到 CI/CD 流水線中：

```yaml
# 範例 GitHub Actions 設定
- name: Run SpecFlow Tests
  run: dotnet test --logger "trx;LogFileName=SpecFlowTestResults.trx"
  
- name: Upload Test Results
  uses: actions/upload-artifact@v2
  with:
    name: test-results
    path: TestResults/
```

## 📞 支援

如有任何問題，請參考：
- [SpecFlow 官方文件](https://docs.specflow.org/)
- [NUnit 文件](https://docs.nunit.org/)
