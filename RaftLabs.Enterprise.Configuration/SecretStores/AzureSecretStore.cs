namespace RaftLabs.Enterprise.Configuration.SecretStores
{
    public class AzureSecretStore
    {
        public string ClientId { get; internal set; }
        public string ClientSecret { get; internal set; }
        public string KeyVaultUrl { get; internal set; }
        public string TenantId { get; internal set; }
    }
}
