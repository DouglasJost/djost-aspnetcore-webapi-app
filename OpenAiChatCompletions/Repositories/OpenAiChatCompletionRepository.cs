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


namespace OpenAiChatCompletions.Repositories
{
    public class OpenAiChatCompletionRepository : IOpenAiChatCompletionRepository
    {
        public async Task<ChatCompletionResponseDto> GetOpenAiChatCompletionAsync(ChatCompletionRequestDto request)
        {
            //
            // Retrieve and validate environment varialbes
            //
            //   To create an environment variable from PowerShell prompt:
            //     PS>set OPENAI_API_TOKEN=  
            //     PS>set OPENAI_API_URL=
            //      
            //   To Display an enivronment varialbe from PowerShell prompt:
            //     PS>Get-ChildItem Env:
            //
            //   Or, System Properties > Advanced Tab > Environment Variables 
            //
            //   Or, use Azure KeyVault
            //
            var apiToken = Environment.GetEnvironmentVariable("OPENAI_API_TOKEN");
            var url = Environment.GetEnvironmentVariable("OPENAI_API_URL");
            if (string.IsNullOrWhiteSpace(apiToken) || string.IsNullOrWhiteSpace(url))
            {
                throw new InvalidOperationException("API Token or URL is not configured.");
            }

            var chatCompletionResponse = new ChatCompletionResponseDto();
            var httpClient = GetClient();  // TODO: Add DjostCache : IDjostCache

            // Add API key to the request headers
            //httpClient.DefaultRequestHeaders.Add("api-key", apiKey);

            // Set Authorization header with Bearer token
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);

            // Serialize enum properties to strings instead of their numeric values
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new StringEnumConverter());

            // Serialize the JSON object
            var jsonRequestBody = JsonConvert.SerializeObject(request, settings);

            // Create the request content
            var requestContent = new StringContent(jsonRequestBody, System.Text.Encoding.UTF8, "application/json");

            // Send the POST request
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
