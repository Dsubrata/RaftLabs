using RaftLabs.Enterprise.Configuration;

namespace RaftLabs.Enterprise.Logger.Services
{
    internal class FileLogger : Logger
    {
        private readonly Serilog.Core.Logger logger;

        public FileLogger(ISettings settings) : base(settings)
        {
            //logger = new LoggerConfiguration()
            //    .MinimumLevel.Debug()
            //    .WriteTo.File(GlobalSettings.Configuration.Logger.OnPrem.FilePath, rollingInterval: RollingInterval.Day)
            //    .CreateLogger();
        }

        public override Guid Debug(string source, string message)
        {
            Guid logReferenceId = Guid.NewGuid();
            //logger.Information($"{DateTime.Now.ToUniversalTime()} # {logReferenceId} # {source} # {message}");
            return logReferenceId;
        }

        public override Guid Error(string source, string message)
        {
            Guid logReferenceId = Guid.NewGuid();
            //logger.Error($"{DateTime.Now.ToUniversalTime()} # {logReferenceId} # {source} # {message}");
            return logReferenceId;
        }

        public override Guid Info(string source, string message)
        {
            Guid logReferenceId = Guid.NewGuid();
            //logger.Information($"{DateTime.Now.ToUniversalTime()} # {logReferenceId} # {source} # {message}");
            return logReferenceId;
        }

        public override Guid Warning(string source, string message)
        {
            Guid logReferenceId = Guid.NewGuid();
            //logger.Warning($"{DateTime.Now.ToUniversalTime()} # {logReferenceId} # {source} # {message}");
            return logReferenceId;
        }
    }
}
