namespace Lab01.Patterns.AbstractFactory
{
    public class PremiumEmailNotification : IEmailNotification
    {
        public string SendEmail(string recipient, string subject, string body)
        {
            return $"[PREMIUM EMAIL] To: {recipient} | Subject: {subject} | Body: {body} | Features: HTML Template + Tracking + Priority Delivery | Status: Sent via SendGrid";
        }
    }

    public class PremiumSMSNotification : ISMSNotification
    {
        public string SendSMS(string phoneNumber, string message)
        {
            return $"[PREMIUM SMS] To: {phoneNumber} | Message: {message} | Features: Delivery Confirmation + Brand Name | Status: Sent via Twilio Premium";
        }
    }
}
