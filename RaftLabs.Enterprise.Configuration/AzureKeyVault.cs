using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace RaftLabs.Enterprise.Configuration
{
    internal class AzureKeyVault : SecretStore
    {
        private readonly SecretClient client;
        public AzureKeyVault(BasicCloudSettings basicCloudSettings) : base(basicCloudSettings)
        {
            ClientSecretCredential clientSecretCredential = new(CloudSettings.AzureTenantId, CloudSettings.AzureClientId, CloudSettings.AzureClientSecret);
            client = new(new(CloudSettings.AzureKeyVaultUrl), clientSecretCredential);
        }

        public override string GetValue(string key)
        {
            KeyVaultSecret data = client.GetSecret(key).Value;
            return data.Value;
        }
    }
}