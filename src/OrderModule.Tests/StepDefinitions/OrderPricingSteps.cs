using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using OrderModule;

namespace OrderModule.Tests.StepDefinitions
{
    [Binding]
    public class OrderPricingSteps
    {
        private OrderService _orderService = new OrderService();
        private List<Product> _orderProducts = new List<Product>();
        private Order _processedOrder = new Order();
        private decimal _thresholdAmount;
        private decimal _discountAmount;

        [BeforeScenario]
        public void BeforeScenario()
        {
            // 每個 scenario 重新初始化
            _orderService = new OrderService();
            _orderProducts = new List<Product>();
            _processedOrder = new Order();
        }

        [Given(@"no promotions are applied")]
        public void GivenNoPromotionsAreApplied()
        {
            // 設定無促銷活動狀態
            // 暫時不實作，只是建立步驟
        }

        [Given(@"the threshold discount promotion is configured:")]
        public void GivenTheThresholdDiscountPromotionIsConfigured(Table table)
        {
            var row = table.Rows[0];
            _thresholdAmount = decimal.Parse(row["threshold"]);
            _discountAmount = decimal.Parse(row["discount"]);
            
            // 設定閾值折扣促銷活動
            _orderService.SetThresholdDiscount(_thresholdAmount, _discountAmount);
        }

        [Given(@"the buy one get one promotion for cosmetics is active")]
        public void GivenTheBuyOneGetOnePromotionForCosmeticsIsActive()
        {
            // 設定買一送一促銷活動
            _orderService.SetBuyOneGetOnePromotion("cosmetics");
        }

        [When(@"a customer places an order with:")]
        public void WhenACustomerPlacesAnOrderWith(Table table)
        {
            // 從表格建立產品清單
            foreach (var row in table.Rows)
            {
                var product = new Product
                {
                    Name = row["productName"],
                    Quantity = int.Parse(row["quantity"]),
                    UnitPrice = decimal.Parse(row["unitPrice"])
                };
                
                // 如果有 category 欄位則設定
                if (row.ContainsKey("category"))
                {
                    product.Category = row["category"];
                }
                
                _orderProducts.Add(product);
            }

            // 處理訂單但不實作邏輯
            _processedOrder = _orderService.ProcessOrder(_orderProducts);
        }

        [Then(@"the order summary should be:")]
        public void ThenTheOrderSummaryShouldBe(Table table)
        {
            var expectedRow = table.Rows[0];
            
            // 檢查是否有 totalAmount 欄位 (第一個 scenario)
            if (expectedRow.ContainsKey("totalAmount"))
            {
                var expectedTotalAmount = decimal.Parse(expectedRow["totalAmount"]);
                Assert.AreEqual(expectedTotalAmount, _processedOrder.Summary.TotalAmount, 
                    $"Expected total amount: {expectedTotalAmount}, but was: {_processedOrder.Summary.TotalAmount}");
            }
            
            // 檢查是否有完整的摘要資訊 (第二個 scenario)
            if (expectedRow.ContainsKey("originalAmount"))
            {
                var expectedOriginalAmount = decimal.Parse(expectedRow["originalAmount"]);
                var expectedDiscount = decimal.Parse(expectedRow["discount"]);
                var expectedTotalAmount = decimal.Parse(expectedRow["totalAmount"]);
                
                Assert.AreEqual(expectedOriginalAmount, _processedOrder.Summary.OriginalAmount, 
                    $"Expected original amount: {expectedOriginalAmount}, but was: {_processedOrder.Summary.OriginalAmount}");
                Assert.AreEqual(expectedDiscount, _processedOrder.Summary.Discount, 
                    $"Expected discount: {expectedDiscount}, but was: {_processedOrder.Summary.Discount}");
                Assert.AreEqual(expectedTotalAmount, _processedOrder.Summary.TotalAmount, 
                    $"Expected total amount: {expectedTotalAmount}, but was: {_processedOrder.Summary.TotalAmount}");
            }
        }

        [Then(@"the customer should receive:")]
        public void ThenTheCustomerShouldReceive(Table table)
        {
            // 檢查顧客收到的商品 - 這將會失敗因為尚未實作
            Assert.AreEqual(table.Rows.Count, _processedOrder.OrderItems.Count, 
                "Order items count mismatch");

            for (int i = 0; i < table.Rows.Count; i++)
            {
                var expectedRow = table.Rows[i];
                var expectedProductName = expectedRow["productName"];
                var expectedQuantity = int.Parse(expectedRow["quantity"]);

                var actualItem = _processedOrder.OrderItems[i];
                Assert.AreEqual(expectedProductName, actualItem.ProductName, 
                    $"Expected product name: {expectedProductName}, but was: {actualItem.ProductName}");
                Assert.AreEqual(expectedQuantity, actualItem.Quantity, 
                    $"Expected quantity: {expectedQuantity}, but was: {actualItem.Quantity}");
            }
        }
    }
}
