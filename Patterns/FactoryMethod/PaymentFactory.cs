namespace Lab01.Patterns.FactoryMethod
{
    public class PaymentFactory
    {
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
