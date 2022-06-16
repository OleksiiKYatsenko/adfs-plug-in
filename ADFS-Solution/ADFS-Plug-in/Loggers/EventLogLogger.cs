using ADFS_Plug_in.Interfaces;
using NLog;

namespace ADFS_Plug_in.Loggers
{
    internal class EventLogLogger : ILogManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        public void LogDebug(string message)
        {
            logger.Debug(message);
        }
        public void LogError(string message)
        {
            Logger logger = LogManager.GetLogger("EventLogTarget");
            var logEventInfo = new LogEventInfo(LogLevel.Error, logger.Name, message);
            logger.Log(logEventInfo);
        }
        public void LogInformation(string message)
        {
            logger.Info(message);
        }
        public void LogWarning(string message)
        {
            logger.Warn(message);
        }
    }
}
