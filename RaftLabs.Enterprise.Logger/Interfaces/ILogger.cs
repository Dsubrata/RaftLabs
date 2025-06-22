namespace RaftLabs.Enterprise.Logger.Interfaces
{
    public interface ILogger
    {
        Guid Info(string source, string message);
        Guid Warning(string source, string message);
        Guid Debug(string source, string message);
        Guid Error(string source, string message);
    }
}
