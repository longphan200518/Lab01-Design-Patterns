namespace Lab01.Patterns.AbstractFactory
{
    // Abstract Product A
    public interface IEmailNotification
    {
        string SendEmail(string recipient, string subject, string body);
    }
}
