namespace Lab01.Patterns.AbstractFactory
{
    public class StandardEmailNotification : IEmailNotification
    {
        public string SendEmail(string recipient, string subject, string body)
        {
            return $"[STANDARD EMAIL] To: {recipient} | Subject: {subject} | Body: {body} | Status: Sent via Standard SMTP";
        }
    }

    public class StandardSMSNotification : ISMSNotification
    {
        public string SendSMS(string phoneNumber, string message)
        {
            return $"[STANDARD SMS] To: {phoneNumber} | Message: {message} | Status: Sent via Standard SMS Gateway";
        }
    }
}
