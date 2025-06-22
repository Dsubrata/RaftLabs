namespace RaftLabs.Enterprise.Configuration.SecretStores
{
    public class SecretStoreSettings
    {
        private static AWSSecretStore aws = new();
        private static AzureSecretStore azure = new();


        public AWSSecretStore AWS { get => aws; internal set => aws = value; }
        public AzureSecretStore Azure { get => azure; internal set => azure = value; }

    }
}
