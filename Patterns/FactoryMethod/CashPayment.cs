namespace Lab01.Patterns.FactoryMethod
{
    public class CashPayment : IPaymentMethod
    {
        public string GetPaymentType() => "Cash";

        public string ProcessPayment(decimal amount, string description)
        {
            return $"[CASH] Payment processed: {amount:C} VND - {description}. Please collect cash at counter.";
        }
    }
}
