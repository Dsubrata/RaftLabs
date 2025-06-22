namespace RaftLabs.Enterprise.Configuration
{
    internal abstract class SecretStore : ISecretStore
    {
        protected BasicCloudSettings CloudSettings { get; }

        public SecretStore(BasicCloudSettings basicCloudSettings)
        {
            CloudSettings = basicCloudSettings;
        }

        public abstract string GetValue(string key);

    }
}
