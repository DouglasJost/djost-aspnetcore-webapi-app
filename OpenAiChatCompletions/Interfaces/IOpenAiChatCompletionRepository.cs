using OpenAiChatCompletions.Models.ChatCompletion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAiChatCompletions.Interfaces
{
    public interface IOpenAiChatCompletionRepository
    {
        Task<ChatCompletionResponseDto> GetOpenAiChatCompletionAsync(ChatCompletionRequestDto request);
    }
}
