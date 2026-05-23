using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using System.Text.Json;

public static class SecretsManagerHelper
{
    private static readonly IAmazonSecretsManager _secretsManager;

    static SecretsManagerHelper()
    {
        // Initialize the AWS Secrets Manager client
        // The AWS credentials and region are automatically picked up from the environment
        // (e.g., IAM role for EC2/ECS/Lambda or ~/.aws/credentials for local development).
        _secretsManager = new AmazonSecretsManagerClient(RegionEndpoint.EUCentral1);
    }

    public static async Task<string> GetSecretAsync(string secretName)
    {
        try
        {
            var request = new GetSecretValueRequest
            {
                SecretId = secretName
            };

            var response = await _secretsManager.GetSecretValueAsync(request);

            if (response.SecretString != null)
            {
                // Parse the JSON secret string to extract the connection string
                var secretJson = JsonDocument.Parse(response.SecretString);
                return secretJson.RootElement.GetProperty("connectionString").GetString();
            }
            else
            {
                // If the secret is binary (unlikely for connection strings)
                throw new InvalidOperationException("Secret is not a string.");
            }
        }
        catch (ResourceNotFoundException)
        {
            throw new InvalidOperationException($"Secret {secretName} not found.");
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error retrieving secret: {ex.Message}");
        }
    }
}