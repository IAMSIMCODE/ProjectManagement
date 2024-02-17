using NLog;

namespace ProjectManagement.Domain.Utility
{
    public interface ILoggerManager
    {
        void LogInfo(string message); void LogWarning(string message); void LogDebug(string message); 
        void LogError(string message); void Error(Exception ex);
    }

    public class LoggerManager : ILoggerManager
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public void Error(Exception ex) => ex?.GetBaseException().ToString();

        public void LogDebug(string message) => logger.Debug(message);

        public void LogError(string message) => logger.Error(message);

        public void LogInfo(string message) => logger.Info(message);

        public void LogWarning(string message) => logger.Warn(message);
    }
}
