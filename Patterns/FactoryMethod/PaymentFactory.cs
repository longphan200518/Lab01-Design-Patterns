namespace Lab01.Patterns.FactoryMethod
{
    /// <summary>
    /// Factory Method Pattern - Creates payment method objects based on payment type
    /// </summary>
    public class PaymentFactory
    {
        // Factory Method
        public static IPaymentMethod CreatePaymentMethod(string paymentType)
        {
            return paymentType.ToLower() switch
            {
                "cash" => new CashPayment(),
                "paypal" => new PaypalPayment(),
                "vnpay" => new VNPayPayment(),
                _ => throw new ArgumentException($"Payment type '{paymentType}' is not supported. Supported types: Cash, Paypal, VNPay")
            };
        }

        public static List<string> GetSupportedPaymentTypes()
        {
            return new List<string> { "Cash", "Paypal", "VNPay" };
        }
    }
}
