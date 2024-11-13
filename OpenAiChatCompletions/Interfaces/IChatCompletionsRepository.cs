using System.Threading.Tasks;
using AppServiceCore;
using OpenAiChatCompletions.Models.ChatCompletion;
using OpenAiChatCompletions.Models.MedicalVisitNote;

namespace OpenAiChatCompletions.Interfaces
{
    public interface IChatCompletionsRepository
    {
        Task<CommandResult<ChatCompletionResponseDto>> GetCompletionAsync(ChatCompletionRequestDto request);

        Task<CommandResult<SoapNoteResponseDto>> GetMedicalSoapNoteAsync();
    }
}
