namespace Lab01.Patterns.FactoryMethod
{
    public class PaypalPayment : IPaymentMethod
    {
        public string GetPaymentType() => "Paypal";

        public string ProcessPayment(decimal amount, string description)
        {
            var transactionId = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
            return $"[PAYPAL] Payment processed: ${amount} USD - {description}. Transaction ID: {transactionId}. Redirecting to PayPal...";
        }
    }
}
