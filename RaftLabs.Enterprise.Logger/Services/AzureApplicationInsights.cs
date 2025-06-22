using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility;
using RaftLabs.Enterprise.Configuration;

namespace RaftLabs.Enterprise.Logger.Services
{
    internal class AzureApplicationInsights : Logger
    {
        private readonly TelemetryClient logger;

        public AzureApplicationInsights(ISettings settings) : base(settings)
        {
            TelemetryConfiguration configuration = TelemetryConfiguration.CreateDefault();
            configuration.ConnectionString = GlobalSettings.Configuration.Logger.Azure.ConnectionString;
            configuration.TelemetryInitializers.Add(new HttpDependenciesParsingTelemetryInitializer());

            logger = new TelemetryClient(configuration);
        }

        public override Guid Info(string source, string message)
        {
            Guid logReferenceId = Guid.NewGuid();
            logger.TrackTrace($"{DateTime.Now.ToUniversalTime()} # {logReferenceId} # {source} # {message}", SeverityLevel.Information);
            logger.Flush();
            return logReferenceId;
        }

        public override Guid Warning(string source, string message)
        {
            Guid logReferenceId = Guid.NewGuid();
            logger.TrackTrace($"{DateTime.Now.ToUniversalTime()} # {logReferenceId} # {source} # {message}", SeverityLevel.Warning);
            logger.Flush();
            return logReferenceId;
        }

        public override Guid Debug(string source, string message)
        {
            Guid logReferenceId = Guid.NewGuid();
            logger.TrackTrace($"{DateTime.Now.ToUniversalTime()} # {logReferenceId} # {source} # {message}", SeverityLevel.Verbose);
            logger.Flush();
            return logReferenceId;
        }

        public override Guid Error(string source, string message)
        {
            Guid logReferenceId = Guid.NewGuid();
            logger.TrackTrace($"{DateTime.Now.ToUniversalTime()} # {logReferenceId} # {source} # {message}", SeverityLevel.Error);
            logger.Flush();
            return logReferenceId;
        }
    }
}
