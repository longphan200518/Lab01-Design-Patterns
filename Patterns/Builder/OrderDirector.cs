namespace Lab01.Patterns.Builder
{
    /// <summary>
    /// Director class - Defines common order building sequences
    /// </summary>
    public class OrderDirector
    {
        public Order CreateStandardOrder(IOrderBuilder builder, string customerName, string email)
        {
            return builder
                .SetCustomerInfo(customerName, email, "")
                .AddItem("Standard Product", 100, 1)
                .SetPaymentMethod("Cash")
                .SetShippingFee(25000)
                .SetTax(10000)
                .Build();
        }

        public Order CreatePremiumOrder(IOrderBuilder builder, string customerName, string email, string phone)
        {
            return builder
                .SetCustomerInfo(customerName, email, phone)
                .AddItem("Premium Product A", 500, 2)
                .AddItem("Premium Product B", 750, 1)
                .SetShippingAddress("123 Premium Street, District 1, HCMC")
                .SetBillingAddress("123 Premium Street, District 1, HCMC")
                .SetPaymentMethod("VNPay")
                .SetShippingFee(0) // Free shipping for premium
                .SetTax(125000)
                .SetCoupon("SAVE10")
                .Build();
        }

        public Order CreateGiftOrder(IOrderBuilder builder, string senderName, string recipientAddress, string giftMessage)
        {
            return builder
                .SetCustomerInfo(senderName, $"{senderName.Replace(" ", "").ToLower()}@email.com", "")
                .AddItem("Gift Box", 250, 1)
                .SetShippingAddress(recipientAddress)
                .SetPaymentMethod("Paypal")
                .SetShippingFee(35000)
                .SetTax(12500)
                .SetAsGift(giftMessage)
                .SetNotes("Handle with care - Gift package")
                .Build();
        }
    }
}
