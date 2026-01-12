namespace Lab01.Services
{
    public interface ILoggerService
    {
        void Log(string message);
        List<string> GetLogs();
    }
}
