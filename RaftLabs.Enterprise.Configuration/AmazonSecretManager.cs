using Amazon;
using Amazon.Runtime;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Newtonsoft.Json;
using System.Text;

namespace RaftLabs.Enterprise.Configuration
{
    internal class AmazonSecretManager : SecretStore
    {
        private static Dictionary<string, string> keyValues;
        private readonly IAmazonSecretsManager client;

        public AmazonSecretManager(BasicCloudSettings basicCloudSettings) : base(basicCloudSettings)
        {
            BasicAWSCredentials awsCreds = new(CloudSettings.AWSAccessKey, CloudSettings.AWSSecretKey);
            client = new AmazonSecretsManagerClient(awsCreds, RegionEndpoint.GetBySystemName(CloudSettings.AWSRegion));
        }

        public override string GetValue(string key)
        {
            GetSecretValueRequest request = new()
            {
                SecretId = CloudSettings.AWSSecretName,
                VersionStage = CloudSettings.AWSSecretTagVersion
            };

            GetSecretValueResponse response = client.GetSecretValueAsync(request).Result;

            if (response.SecretString != null)
            {
                keyValues = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.SecretString);
            }
            else
            {
                MemoryStream memoryStream = response.SecretBinary;
                StreamReader reader = new(memoryStream);
                keyValues = JsonConvert.DeserializeObject<Dictionary<string, string>>(Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadToEnd())));
            }

            return keyValues[key];
        }

    }
}