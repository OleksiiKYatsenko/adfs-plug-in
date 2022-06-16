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
            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "Application";
                eventLog.WriteEntry(message, EventLogEntryType.Error);
            }
        }
        public void LogInformation(string message)
        {
            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "Application";
                eventLog.WriteEntry(message, EventLogEntryType.Information);
            }
        }
        public void LogWarning(string message)
        {
            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "Application";
                eventLog.WriteEntry(message, EventLogEntryType.Warning);
            }
        }
    }
}
