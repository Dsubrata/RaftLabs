using RaftLabs.Enterprise.Configuration.CloudProviders;

namespace RaftLabs.Enterprise.Configuration.Loggers
{
    public class AzureLogger : MicrosoftAzure
    {
        public string ConnectionString { get; internal set; }
    }
}
