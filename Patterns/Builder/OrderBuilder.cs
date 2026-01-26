namespace Lab01.Patterns.Builder
{
    public class OrderBuilder : IOrderBuilder
    {
        private readonly Order _order;

        public OrderBuilder()
        {
            _order = new Order
            {
                OrderDate = DateTime.Now,
                Status = "Pending"
            };
        }

        public IOrderBuilder SetOrderId(string orderId)
        {
            _order.OrderId = orderId;
            return this;
        }

        public IOrderBuilder SetCustomerInfo(string name, string email, string phone)
        {
            _order.CustomerName = name;
            _order.CustomerEmail = email;
            _order.CustomerPhone = phone;
            return this;
        }

        public IOrderBuilder SetShippingAddress(string address)
        {
            _order.ShippingAddress = address;
            return this;
        }

        public IOrderBuilder SetBillingAddress(string address)
        {
            _order.BillingAddress = address;
            return this;
        }

        public IOrderBuilder AddItem(string productName, decimal price, int quantity)
        {
            _order.Items.Add(new OrderItem
            {
                ProductName = productName,
                Price = price,
                Quantity = quantity
            });
            return this;
        }

        public IOrderBuilder SetPaymentMethod(string paymentMethod)
        {
            _order.PaymentMethod = paymentMethod;
            return this;
        }

        public IOrderBuilder SetShippingFee(decimal fee)
        {
            _order.ShippingFee = fee;
            return this;
        }

        public IOrderBuilder SetTax(decimal tax)
        {
            _order.Tax = tax;
            return this;
        }

        public IOrderBuilder SetDiscount(decimal discount)
        {
            _order.Discount = discount;
            return this;
        }

        public IOrderBuilder SetCoupon(string couponCode)
        {
            _order.CouponCode = couponCode;
            // Apply discount based on coupon
            if (couponCode.ToUpper() == "SAVE10")
            {
                _order.Discount += _order.GetSubtotal() * 0.1m;
            }
            else if (couponCode.ToUpper() == "SAVE20")
            {
                _order.Discount += _order.GetSubtotal() * 0.2m;
            }
            return this;
        }

        public IOrderBuilder SetNotes(string notes)
        {
            _order.Notes = notes;
            return this;
        }

        public IOrderBuilder SetAsGift(string giftMessage)
        {
            _order.IsGift = true;
            _order.GiftMessage = giftMessage;
            return this;
        }

        public IOrderBuilder SetOrderDate(DateTime orderDate)
        {
            _order.OrderDate = orderDate;
            return this;
        }

        public IOrderBuilder SetStatus(string status)
        {
            _order.Status = status;
            return this;
        }

        public Order Build()
        {
            // Validation before building
            if (string.IsNullOrEmpty(_order.CustomerName))
                throw new InvalidOperationException("Customer name is required");
            
            if (_order.Items.Count == 0)
                throw new InvalidOperationException("Order must have at least one item");

            if (string.IsNullOrEmpty(_order.OrderId))
                _order.OrderId = $"ORD-{DateTime.Now:yyyyMMddHHmmss}";

            return _order;
        }
    }
}
