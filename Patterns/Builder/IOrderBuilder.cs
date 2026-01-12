namespace Lab01.Patterns.Builder
{
    public interface IOrderBuilder
    {
        IOrderBuilder SetOrderId(string orderId);
        IOrderBuilder SetCustomerInfo(string name, string email, string phone);
        IOrderBuilder SetShippingAddress(string address);
        IOrderBuilder SetBillingAddress(string address);
        IOrderBuilder AddItem(string productName, decimal price, int quantity);
        IOrderBuilder SetPaymentMethod(string paymentMethod);
        IOrderBuilder SetShippingFee(decimal fee);
        IOrderBuilder SetTax(decimal tax);
        IOrderBuilder SetDiscount(decimal discount);
        IOrderBuilder SetCoupon(string couponCode);
        IOrderBuilder SetNotes(string notes);
        IOrderBuilder SetAsGift(string giftMessage);
        IOrderBuilder SetOrderDate(DateTime orderDate);
        IOrderBuilder SetStatus(string status);
        Order Build();
    }
}
