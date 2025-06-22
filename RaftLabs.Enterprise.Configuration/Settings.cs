using Microsoft.Extensions.Configuration;
using RaftLabs.Enterprise.Configuration.Loggers;
using RaftLabs.Enterprise.Utility.Enums;

namespace RaftLabs.Enterprise.Configuration
{
    public class Settings : ISettings
    {
        private Settings configuration;
        private static ISecretStore secretStore;

        public Settings(IConfiguration configuration)
        {
            HostingEnvironment = (HostingEnvironment)configuration.GetValue<int>("AppSettings:HostingEnvironment");
            DeploymentEnvironment = (DeploymentEnvironment)configuration.GetValue<int>("AppSettings:DeploymentEnvironment");
            EnableAzureAD = configuration.GetValue<bool>("AppSettings:EnableAzureAD");
            ApplicationName = configuration.GetValue<string>("AppSettings:ApplicationName");

            if (HostingEnvironment == 0)
            {
                throw new Exception("Hosting environment not configured");
            }

            if (HostingEnvironment is HostingEnvironment.OnPrem)
            {
                ApplicationDbConnectionString = configuration.GetConnectionString("ApplicationDb");
                ReportingDbConnectionString = configuration.GetConnectionString("ReportingDb");
                DbProvider = (DbProvider)configuration.GetValue<int>("AppSettings:DbProvider");
                WebApiBaseUrl = configuration.GetValue<string>("AppSettings:ApiBaseUrl");
                ConfigureOnPrem(configuration);
                return;
            }

            CloudSettings.HostingEnvironment = HostingEnvironment;
            CloudSettings.EnableAzureAD = EnableAzureAD;
            CloudSettings.DeploymentEnvironment = DeploymentEnvironment;
            //Connect to Secret Store
            switch (HostingEnvironment)
            {
                case HostingEnvironment.AWS:
                    CloudSettings.AWSRegion = configuration.GetValue<string>("AppSettings:AWS:Region");
                    break;
                case HostingEnvironment.Azure:
                    CloudSettings.AzureClientId = configuration.GetValue<string>("AppSettings:Azure:ClientId");
                    CloudSettings.AzureTenantId = configuration.GetValue<string>("AppSettings:Azure:TenantId");
                    break;                
                case HostingEnvironment.OnPrem:
                    break;
                    throw new Exception("Case not found");
            }

            secretStore = SecretStoreFactory.Create(CloudSettings);

            if (secretStore is not null)
            {
                ApplicationDbConnectionString = secretStore.GetValue("ApplicationDb");
                ReportingDbConnectionString = secretStore.GetValue("ReportingDb");
                DbProvider = (DbProvider)Convert.ToInt16(secretStore.GetValue("DbProvider"));

                WebApiBaseUrl = secretStore.GetValue("WebApiBaseUrl");

                switch (HostingEnvironment)
                {
                    case HostingEnvironment.AWS:
                        ConfigureAWS(secretStore);
                        break;
                    case HostingEnvironment.Azure:
                        ConfigureAzure(secretStore);
                        break;
                    case HostingEnvironment.OnPrem:
                        break;
                        throw new Exception("Case no found");
                }
            }
        }

        private void ConfigureOnPrem(IConfiguration configuration)
        {
            
        }

        private void ConfigureAzure(ISecretStore secretStore)
        {
            Logger.Azure.ConnectionString = secretStore.GetValue("AppInsightConnectionString");           
        }

        private void ConfigureAWS(ISecretStore secretStore)
        {
            Logger.AWS.LogGroup = secretStore.GetValue("LogGroup");           
        }
      
        Settings ISettings.Configuration { get => this; set => configuration = value; }
        public HostingEnvironment HostingEnvironment { get; internal set; }
        public DeploymentEnvironment DeploymentEnvironment { get; internal set; }
        public BasicCloudSettings CloudSettings { get; internal set; } = new();
        public LoggerSettings Logger { get; internal set; } = new();
        public string ApplicationDbConnectionString { get; internal set; }
        public string ReportingDbConnectionString { get; internal set; }
        public DbProvider DbProvider { get; internal set; }
        public string WebApiBaseUrl { get; internal set; }
        public string ReportingApiBaseUrl { get; internal set; }
        public string ContentType { get; internal set; } = "application/json";
        public int ServiceInvokeInterval { get; internal set; } = 0;
        public bool EnableAzureAD { get; internal set; }
        public int ServiceRestartIteration { get; internal set; }
        public string ApplicationName { get; internal set; }
    }
}
