using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using OpenAiChatCompletions.Interfaces;
using OpenAiChatCompletions.Models.ChatCompletion;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Runtime.Caching;
using System.Net.Http.Headers;
using AppServiceCore.Services.AzureKeyVaultService;


namespace OpenAiChatCompletions.Repositories
{
    public class OpenAiChatCompletionRepository : IOpenAiChatCompletionRepository
    {
        private readonly AzureKeyVaultService _azureKeyVaultService;

        public OpenAiChatCompletionRepository(AzureKeyVaultService azureKeyVaultService)
        {
          _azureKeyVaultService = azureKeyVaultService;
        }

        public async Task<ChatCompletionResponseDto> GetOpenAiChatCompletionAsync(
          ChatCompletionEntity request,
          ChatCompletionServiceProviderType chatCompletionServiceProvider)
        {
            //
            // Retrieve and validate environment varialbes
            //
            //   To create an environment variable from PowerShell prompt:
            //     PS>set OPENAI_API_TOKEN=  
            //     PS>set OPENAI_API_URL=
            //     PS>set AZURE_API_KEY=  
            //     PS>set AZURE_API_ENDPOINT=
            //      
            //   To Display an enivronment varialbe from PowerShell prompt:
            //     PS>Get-ChildItem Env:
            //
            //   Or, System Properties > Advanced Tab > Environment Variables 
            //
            //   Or, use Azure KeyVault
            //

            string openAiApiToken = await _azureKeyVaultService.GetStringAsync("OPENAI-API-TOKEN");
            string openAiApiUrl = await _azureKeyVaultService.GetStringAsync("OPENAI-API-URL");
            if (chatCompletionServiceProvider == ChatCompletionServiceProviderType.OpenAI)
            {
              if (string.IsNullOrWhiteSpace(openAiApiToken) || string.IsNullOrWhiteSpace(openAiApiUrl))
              {
                throw new InvalidOperationException("API Token or URL is not configured.");
              }
            }

            string azureApiKey = await _azureKeyVaultService.GetStringAsync("AZURE-API-KEY");
            string azureApiEndpoint = await _azureKeyVaultService.GetStringAsync("AZURE-API-ENDPOINT");
            if (chatCompletionServiceProvider == ChatCompletionServiceProviderType.AzureOpenAI)
            {
              if (string.IsNullOrWhiteSpace(azureApiKey) || string.IsNullOrWhiteSpace(azureApiEndpoint))
              {
                throw new InvalidOperationException("API Key or Endpoint is not configured.");
              }
            }

            //var openAiApiToken = Environment.GetEnvironmentVariable("OPENAI_API_TOKEN");
            //var openAiApiUrl = Environment.GetEnvironmentVariable("OPENAI_API_URL");
            //if (chatCompletionServiceProvider == ChatCompletionServiceProviderType.OpenAI)
            //{
            //  if (string.IsNullOrWhiteSpace(openAiApiToken) || string.IsNullOrWhiteSpace(openAiApiUrl))
            //  {
            //    throw new InvalidOperationException("API Token or URL is not configured.");
            //  }
            //}

            //var azureApiKey = Environment.GetEnvironmentVariable("AZURE_API_KEY");
            //var azureApiEndpoint = Environment.GetEnvironmentVariable("AZURE_API_ENDPOINT");
            //if (chatCompletionServiceProvider == ChatCompletionServiceProviderType.AzureOpenAI)
            //{
            //  if (string.IsNullOrWhiteSpace(azureApiKey) || string.IsNullOrWhiteSpace(azureApiEndpoint))
            //  {
            //    throw new InvalidOperationException("API Key or Endpoint is not configured.");
            //  }
            //}

            var chatCompletionResponse = new ChatCompletionResponseDto();
            var httpClient = GetClient();  // TODO: Add DjostCache : IDjostCache

            if (chatCompletionServiceProvider == ChatCompletionServiceProviderType.OpenAI)
            {
              // Set Authorization header with Bearer token
              httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", openAiApiToken);
            }
            else if (chatCompletionServiceProvider == ChatCompletionServiceProviderType.AzureOpenAI)
            {
              // Add API key to the request headers
              httpClient.DefaultRequestHeaders.Add("api-key", azureApiKey);
            }

            // Serialize enum properties to strings instead of their numeric values
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new StringEnumConverter());

            // Serialize the JSON object
            var jsonRequestBody = JsonConvert.SerializeObject(request, settings);

            // Create the request content
            var requestContent = new StringContent(jsonRequestBody, System.Text.Encoding.UTF8, "application/json");

            // Send the POST request
            var url = (chatCompletionServiceProvider == ChatCompletionServiceProviderType.OpenAI) ? openAiApiUrl : azureApiEndpoint;
            var response = await httpClient.PostAsync(url, requestContent);

            // Get result
            var responseContent = response.Content;
            var result = await responseContent.ReadAsStringAsync();

            // Check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                var completionResponse = JsonConvert.DeserializeObject<CompletionResponseDto>(result);
                chatCompletionResponse.completion_response = completionResponse;
            }
            else
            {
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponseDto>(result);
                chatCompletionResponse.error_response = errorResponse;
                if (chatCompletionResponse.error_response != null)
                {
                    chatCompletionResponse.error_response.status_code = (int)response.StatusCode;
                    chatCompletionResponse.error_response.status_code_name = response.StatusCode.ToString();
                }
            }

            return chatCompletionResponse;
        }

        private HttpClient GetClient()
        {
            // Step 1: Create a ServiceCollection to hold the services
            var serviceCollection = new ServiceCollection();

            // Step 2: Register the IHttpClientFactory service
            serviceCollection.AddHttpClient();

            // Step 3: Build the ServiceProvider to create service instances
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Step 4: Manually get the IHttpClientFactory instance
            var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();

            // Step 5: Use the factory to create an HttpClient
            var client = httpClientFactory.CreateClient();

            return client;
        }

        // Get Non-Expiring CacheItemPolicy that handles IDisposable if for some reason they do get removed
        private static CacheItemPolicy GetDisposableCachePolicy()
        {
            return new CacheItemPolicy
            {
                RemovedCallback = x =>
                {
                    // Dispose of stuff like HttpClient/StaticHttpClient when it gets removed from the cache
                    var item = x.CacheItem.Value as IDisposable;
                    item?.Dispose();
                }
            };
        }
    }
}
