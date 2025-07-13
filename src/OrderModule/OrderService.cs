namespace OrderModule
{
    public class Product
    {
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class OrderItem
    {
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }

    public class OrderSummary
    {
        public decimal OriginalAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class Order
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public OrderSummary Summary { get; set; } = new OrderSummary();
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }

    public class ThresholdDiscount
    {
        public decimal Threshold { get; set; }
        public decimal DiscountAmount { get; set; }
    }

    public class OrderService
    {
        private ThresholdDiscount? _thresholdDiscount;
        private string? _buyOneGetOneCategory;

        public void SetThresholdDiscount(decimal threshold, decimal discountAmount)
        {
            _thresholdDiscount = new ThresholdDiscount
            {
                Threshold = threshold,
                DiscountAmount = discountAmount
            };
        }

        public void SetBuyOneGetOnePromotion(string category)
        {
            _buyOneGetOneCategory = category;
        }

        public Order ProcessOrder(List<Product> products)
        {
            var order = new Order();
            order.Products = products;
            
            // 計算原始總金額
            decimal originalAmount = 0;
            foreach (var product in products)
            {
                originalAmount += product.UnitPrice * product.Quantity;
            }
            
            // 計算閾值折扣
            decimal discount = 0;
            if (_thresholdDiscount != null && originalAmount >= _thresholdDiscount.Threshold)
            {
                discount = _thresholdDiscount.DiscountAmount;
            }
            
            // 設定訂單摘要
            order.Summary.OriginalAmount = originalAmount;
            order.Summary.Discount = discount;
            order.Summary.TotalAmount = originalAmount - discount;
            
            // 建立訂單項目，處理買一送一邏輯
            foreach (var product in products)
            {
                int finalQuantity = product.Quantity;
                
                // 如果有買一送一促銷且產品屬於促銷類別
                if (_buyOneGetOneCategory != null && 
                    product.Category == _buyOneGetOneCategory)
                {
                    // 買一送一邏輯：針對化妝品，每個產品種類獲得1個贈品
                    // 不論購買多少個，只送1個
                    finalQuantity = product.Quantity + 1;
                }
                
                order.OrderItems.Add(new OrderItem
                {
                    ProductName = product.Name,
                    Quantity = finalQuantity
                });
            }
            
            return order;
        }
    }
}
