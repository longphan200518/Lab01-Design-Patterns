namespace Lab01.Patterns.FactoryMethod
{
    public interface IPaymentMethod
    {
        string ProcessPayment(decimal amount, string description);
        string GetPaymentType();
    }
}
