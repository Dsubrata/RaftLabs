using RaftLabs.Enterprise.Configuration;
using RaftLabs.Enterprise.Logger.Interfaces;
using RaftLabs.Enterprise.Logger.Services;
using RaftLabs.Enterprise.Utility.Enums;

namespace RaftLabs.Enterprise.Logger
{
    public abstract class LogFactory
    {
        public static ILogger Create(ISettings settings)
        {
            return settings.Configuration.HostingEnvironment switch
            {
                HostingEnvironment.AWS => new AWSCloudWatch(settings),
                HostingEnvironment.Azure => new AzureApplicationInsights(settings),
                HostingEnvironment.OnPrem => new FileLogger(settings),
                _ => new FileLogger(settings),
            };
        }
    }
}
