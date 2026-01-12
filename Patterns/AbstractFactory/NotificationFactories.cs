namespace Lab01.Patterns.AbstractFactory
{
    // Concrete Factory 1 - Creates Standard notification family
    public class StandardNotificationFactory : INotificationFactory
    {
        public IEmailNotification CreateEmailNotification()
        {
            return new StandardEmailNotification();
        }

        public ISMSNotification CreateSMSNotification()
        {
            return new StandardSMSNotification();
        }

        public string GetFactoryType() => "Standard";
    }

    // Concrete Factory 2 - Creates Premium notification family
    public class PremiumNotificationFactory : INotificationFactory
    {
        public IEmailNotification CreateEmailNotification()
        {
            return new PremiumEmailNotification();
        }

        public ISMSNotification CreateSMSNotification()
        {
            return new PremiumSMSNotification();
        }

        public string GetFactoryType() => "Premium";
    }
}
