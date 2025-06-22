using RaftLabs.Enterprise.Configuration;
using System.Diagnostics;
using System.Runtime.Versioning;

namespace RaftLabs.Enterprise.Logger.Services
{
    internal class WindowsEventLog : Logger
    {
        private readonly string applicationName;
        private static EventLog eventLog;

        [SupportedOSPlatform("windows")]
        public WindowsEventLog(ISettings settings) : base(settings)
        {
            applicationName = settings.Configuration.ApplicationName;
            eventLog = new("Application");
        }

        [SupportedOSPlatform("windows")]
        public override Guid Debug(string source, string message)
        {
            Guid logReferenceId = Guid.NewGuid();
            eventLog.Source = applicationName;
            eventLog.WriteEntry($"# {logReferenceId} # {message}", EventLogEntryType.Information);
            return logReferenceId;
        }

        [SupportedOSPlatform("windows")]
        public override Guid Error(string source, string message)
        {
            Guid logReferenceId = Guid.NewGuid();
            eventLog.Source = applicationName;
            eventLog.WriteEntry($"# {logReferenceId} # {message}", EventLogEntryType.Error);
            return logReferenceId;
        }

        [SupportedOSPlatform("windows")]
        public override Guid Info(string source, string message)
        {
            Guid logReferenceId = Guid.NewGuid();
            eventLog.Source = applicationName;
            eventLog.WriteEntry($"# {logReferenceId} # {message}", EventLogEntryType.Information);
            return logReferenceId;
        }

        [SupportedOSPlatform("windows")]
        public override Guid Warning(string source, string message)
        {
            Guid logReferenceId = Guid.NewGuid();
            eventLog.Source = applicationName;
            eventLog.WriteEntry($"# {logReferenceId} # {message}", EventLogEntryType.Warning);
            return logReferenceId;
        }
    }
}
