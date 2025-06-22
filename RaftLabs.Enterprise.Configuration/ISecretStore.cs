namespace RaftLabs.Enterprise.Configuration
{
    public interface ISecretStore
    {
        string GetValue(string key);
    }
}
