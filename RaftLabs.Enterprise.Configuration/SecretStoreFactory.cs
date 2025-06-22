using RaftLabs.Enterprise.Utility.Enums;

namespace RaftLabs.Enterprise.Configuration
{
    public abstract class SecretStoreFactory
    {
        public static ISecretStore Create(BasicCloudSettings basicCloudSettings)
        {
            return basicCloudSettings.HostingEnvironment switch
            {
                HostingEnvironment.AWS => new AmazonSecretManager(basicCloudSettings),
                HostingEnvironment.Azure => new AzureKeyVault(basicCloudSettings),
                _ => throw new Exception("Secret Store Configuration Failed"),
            };
        }
    }
}
