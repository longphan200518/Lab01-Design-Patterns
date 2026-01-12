namespace Lab01.Patterns.FactoryMethod
{
    public class VNPayPayment : IPaymentMethod
    {
        public string GetPaymentType() => "VNPay";

        public string ProcessPayment(decimal amount, string description)
        {
            var transactionId = $"VNP{DateTime.Now:yyyyMMddHHmmss}{new Random().Next(1000, 9999)}";
            return $"[VNPAY] Payment processed: {amount:N0} VND - {description}. Transaction ID: {transactionId}. Payment via VNPay Gateway.";
        }
    }
}
