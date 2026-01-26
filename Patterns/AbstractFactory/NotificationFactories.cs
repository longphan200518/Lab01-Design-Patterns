namespace Lab01.Patterns.AbstractFactory
{
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
