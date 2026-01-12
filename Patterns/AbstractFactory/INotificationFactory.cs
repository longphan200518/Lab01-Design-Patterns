namespace Lab01.Patterns.AbstractFactory
{
    /// <summary>
    /// Abstract Factory - Creates families of related notification objects
    /// </summary>
    public interface INotificationFactory
    {
        IEmailNotification CreateEmailNotification();
        ISMSNotification CreateSMSNotification();
        string GetFactoryType();
    }
}
