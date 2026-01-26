namespace Lab01.Patterns.Builder
{
    public class Order
    {
        public string OrderId { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string ShippingAddress { get; set; } = string.Empty;
        public string BillingAddress { get; set; } = string.Empty;
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public string PaymentMethod { get; set; } = string.Empty;
        public decimal ShippingFee { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public string CouponCode { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public bool IsGift { get; set; }
        public string GiftMessage { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = "Pending";

        public decimal GetSubtotal()
        {
            return Items.Sum(item => item.Price * item.Quantity);
        }

        public decimal GetTotal()
        {
            return GetSubtotal() + ShippingFee + Tax - Discount;
        }

        public override string ToString()
        {
            return $"Order #{OrderId} - Customer: {CustomerName} - Total: {GetTotal():C} - Status: {Status}";
        }
    }

    public class OrderItem
    {
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        
        public override string ToString()
        {
            return $"{ProductName} x{Quantity} = {Price * Quantity:C}";
        }
    }
}
