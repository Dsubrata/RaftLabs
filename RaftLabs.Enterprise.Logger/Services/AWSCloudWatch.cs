using Amazon.Runtime;
using NLog;
using NLog.AWS.Logger;
using NLog.Config;
using RaftLabs.Enterprise.Configuration;

namespace RaftLabs.Enterprise.Logger.Services
{
    internal class AWSCloudWatch : Logger
    {
        private readonly NLog.Logger logger;

        public AWSCloudWatch(ISettings settings) : base(settings)
        {
            LoggingConfiguration config = new();
            AWSTarget awsTarget = new()
            {
                Region = GlobalSettings.Configuration.CloudSettings.AWSRegion,
                LogGroup = GlobalSettings.Configuration.Logger.AWS.LogGroup,
                Credentials = new BasicAWSCredentials(GlobalSettings.Configuration.CloudSettings.AWSAccessKey, GlobalSettings.Configuration.CloudSettings.AWSSecretKey)
            };
            config.AddTarget("aws", awsTarget);
            config.LoggingRules.Add(new("*", LogLevel.Info, awsTarget));
            LogManager.Configuration = config;
            logger = LogManager.GetCurrentClassLogger();
        }

        public override Guid Info(string source, string message)
        {
            Guid logReferenceId = Guid.NewGuid();
            logger.Info($"{DateTime.Now.ToUniversalTime()} # {logReferenceId} # {source} # {message}");
            return logReferenceId;
        }

        public override Guid Warning(string source, string message)
        {
            Guid logReferenceId = Guid.NewGuid();
            logger.Warn($"{DateTime.Now.ToUniversalTime()} # {logReferenceId} # {source} # {message}");
            return logReferenceId;
        }

        public override Guid Debug(string source, string message)
        {
            Guid logReferenceId = Guid.NewGuid();
            logger.Debug($"{DateTime.Now.ToUniversalTime()} # {logReferenceId} # {source} # {message}");
            return logReferenceId;
        }

        public override Guid Error(string source, string message)
        {
            Guid logReferenceId = Guid.NewGuid();
            logger.Error($"{DateTime.Now.ToUniversalTime()} # {logReferenceId} # {source} # {message}");
            return logReferenceId;
        }
    }
}
