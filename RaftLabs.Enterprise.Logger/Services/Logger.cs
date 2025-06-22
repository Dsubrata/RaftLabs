using RaftLabs.Enterprise.Configuration;
using RaftLabs.Enterprise.Logger.Interfaces;

namespace RaftLabs.Enterprise.Logger.Services
{
    internal abstract class Logger : ILogger
    {
        protected static ISettings GlobalSettings;

        public abstract Guid Debug(string source, string message);
        public abstract Guid Error(string source, string message);
        public abstract Guid Info(string source, string message);
        public abstract Guid Warning(string source, string message);

        public Logger(ISettings settings)
        {
            GlobalSettings = settings;
        }

    }
}
