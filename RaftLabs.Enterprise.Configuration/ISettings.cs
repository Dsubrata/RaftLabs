namespace RaftLabs.Enterprise.Configuration
{
    public interface ISettings
    {
        public Settings Configuration { get; internal set; }
    }
}