namespace RaftLabs.Enterprise.Configuration.SecretStores
{
    public class AWSSecretStore
    {
        public string AccessKey { get; internal set; }
        public string Region { get; internal set; }
        public string SecretKey { get; internal set; }
        public string SecretName { get; internal set; }
        public string SecretTagVersion { get; internal set; } = "AWSCURRENT";
    }
}
