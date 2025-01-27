using System;
using System.Threading.Tasks;
using Azure;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace AppServiceCore.Services.KeyVaultService
{
    public static class KeyVaultSecretNames
    {
        public static readonly string Authentication_Audience = "Authentication-Audience";
        public static readonly string Authentication_Issuer = "Authentication-Issuer";
        public static readonly string Authentication_SecretForKey = "Authentication-SecretForKey";
        public static readonly string Azure_KeyVault_Url = "Azure-KeyVault-Url";
        public static readonly string ChatCompletions_Azure_OpenAI_Key = "ChatCompletions-Azure-OpenAI-Key";
        public static readonly string ChatCompletions_Azure_OpenAI_Url = "ChatCompletions-Azure-OpenAI-Url";
        public static readonly string ChatCompletions_OpenAI_Token = "ChatCompletions-OpenAI-Token";
        public static readonly string ChatCompletions_OpenAI_Url = "ChatCompletions-OpenAI-Url";
        public static readonly string DB_Connection_String_MusicCollectionDB = "DB-Connection-String-MusicCollectionDB";
    }

    public class KeyVaultService
    {
        private readonly SecretClient _secretClient;
        private readonly string _environment;

        public KeyVaultService(string keyVaultUrl, string environment)
        {
            if (string.IsNullOrWhiteSpace(keyVaultUrl))
            {
                throw new ArgumentNullException(nameof(keyVaultUrl), "Azure Key Vault URL cannot be null or empty.");
            }
            if (string.IsNullOrWhiteSpace(environment))
            {
                throw new ArgumentNullException(nameof(environment), "Environment cannot be null or empty.");
            }

            _secretClient = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
            _environment = environment.ToUpperInvariant();
        }

        public async Task<string> GetSecretValueAsync(string secretName)
        {
            if (string.IsNullOrWhiteSpace(secretName))
            {
                throw new ArgumentNullException(nameof(secretName), "Secret name cannot be null or empty.");
            }

            try
            {
                string secretValue;

                if (_environment == "DEV")
                {
                    secretValue = Environment.GetEnvironmentVariable(secretName)
                    ?? throw new InvalidOperationException($"The environment variable '{secretName}' is not set in the DEV environment.");
                }
                else
                {
                    KeyVaultSecret secret = await _secretClient.GetSecretAsync(secretName);
                    secretValue = secret?.Value ?? throw new InvalidOperationException($"The secret '{secretName}' is not found in Azure Key Vault.");
                }

                return secretValue;
            }
            catch (RequestFailedException ex) when (ex.Status == 403)
            {
                // Handle access denied specifically
                throw new UnauthorizedAccessException($"Access to the Azure Key Vault is denied. Unable to retrieve the secret '{secretName}'. Check RBAC permissions.", ex);
            }
            catch (RequestFailedException ex)
            {
                // Handle other Azure-related errors
                throw new InvalidOperationException($"An error occurred while retrieving the secret '{secretName}' from Azure Key Vault. Status Code: {ex.Status}.", ex);
            }
            catch (Exception ex)
            {
                // General error handling
                throw new InvalidOperationException($"An unexpected error occurred while retrieving the secret '{secretName}'.", ex);
            }
        }
    }
}
