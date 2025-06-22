using RaftLabs.Enterprise.Utility.Enums;

namespace RaftLabs.Enterprise.Configuration
{
    public class BasicCloudSettings
    {
        public HostingEnvironment HostingEnvironment { get; set; }
        public DeploymentEnvironment DeploymentEnvironment { get; set; }
        public bool EnableAzureAD { get; set; }
        public string AWSAccessKey { get; set; }
        public string AWSRegion { get; set; }
        public string AWSSecretKey { get; set; }
        public string AWSSecretName { get; set; }
        public string AWSSecretTagVersion { get; set; } = "AWSCURRENT";
        public string AzureClientId { get; set; }
        public string AzureClientSecret { get; set; }
        public string AzureKeyVaultUrl { get; set; }
        public string AzureTenantId { get; set; }

    }
}
