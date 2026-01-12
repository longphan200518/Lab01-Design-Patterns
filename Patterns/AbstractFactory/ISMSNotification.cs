namespace Lab01.Patterns.AbstractFactory
{
    // Abstract Product B
    public interface ISMSNotification
    {
        string SendSMS(string phoneNumber, string message);
    }
}
