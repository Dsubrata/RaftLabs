namespace RaftLabs.Enterprise.Configuration.Loggers
{
    public class LoggerSettings
    {
        private static AWSLogger aws = new();
        private static AzureLogger azure = new();
        private static OnPremLogger onprem = new();

        public AWSLogger AWS { get => aws; internal set => aws = value; }
        public AzureLogger Azure { get => azure; internal set => azure = value; }
        public OnPremLogger OnPrem { get => onprem; internal set => onprem = value; }
    }
}
