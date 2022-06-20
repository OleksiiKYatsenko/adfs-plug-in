using ADFS_Plug_in.Interfaces;
using NLog;
using Microsoft.Extensions.Logging.EventLog;
using System.Diagnostics;

namespace ADFS_Plug_in.Loggers
{
    internal class EventLogLogger : ILogManager
    {
        public void LogError(string message)
        {
            Log(message, EventLogEntryType.Error);
        }
        public void LogInformation(string message)
        {
            Log(message, EventLogEntryType.Information);
        }
        public void LogWarning(string message)
        {
            Log(message, EventLogEntryType.Warning);

        }

        public void Log(string message, EventLogEntryType type)
        {
            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "Application";
                eventLog.WriteEntry(message, type);
            }
        }
    }
}
