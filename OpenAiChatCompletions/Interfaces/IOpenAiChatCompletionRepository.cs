using OpenAiChatCompletions.Models.ChatCompletion;
using System.Threading.Tasks;

namespace OpenAiChatCompletions.Interfaces
{
    public interface IOpenAiChatCompletionRepository
    {
        Task<ChatCompletionResponseDto> GetOpenAiChatCompletionAsync(
          ChatCompletionEntity entity, 
          ChatCompletionServiceProviderType chatCompletionServiceProvider);
    }
}
