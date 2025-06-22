using RaftLabs.Enterprise.Configuration.CloudProviders;

namespace RaftLabs.Enterprise.Configuration.Loggers
{
    public class AWSLogger : AWS
    {
        public string LogGroup { get; internal set; }
    }
}
