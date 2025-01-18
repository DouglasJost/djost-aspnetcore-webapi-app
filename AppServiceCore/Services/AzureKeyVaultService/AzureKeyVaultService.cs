using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace AppServiceCore.Services.AzureKeyVaultService
{
  public class AzureKeyVaultService
  {
    private readonly SecretClient _secretClient;

    public AzureKeyVaultService(string keyVaultUrl) 
    {
      // DefaultAzureCredential supports RBAC automatically
      _secretClient = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
    }

    public async Task<string> GetStringAsync(string secretName)
    {
      try 
      {
        KeyVaultSecret secret = await _secretClient.GetSecretAsync(secretName);
        return secret.Value;
      }
      catch (Azure.RequestFailedException arfEx)
      {
        throw new UnauthorizedAccessException($"Access to the Key Vault is denied. Unable to retrieve Key Vault secret {secretName}. Check RBAC permissions.", arfEx);
      }
      catch (Exception ex)
      {
        throw new UnauthorizedAccessException($"Unable retrieve Key Vault secret {secretName}.", ex);
      }
    }
  }
}
