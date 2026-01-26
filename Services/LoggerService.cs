namespace Lab01.Services
{
    public class LoggerService : ILoggerService
    {
        private static LoggerService? _instance;
        private static readonly object _lock = new object();
        private readonly List<string> _logs;

        private LoggerService()
        {
            _logs = new List<string>();
            Log("LoggerService Singleton instance created");
        }

        public static LoggerService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new LoggerService();
                        }
                    }
                }
                return _instance;
            }
        }

        public void Log(string message)
        {
            var logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
            _logs.Add(logEntry);
            Console.WriteLine(logEntry);
        }

        public List<string> GetLogs()
        {
            return new List<string>(_logs);
        }
    }
}
