using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServiceCore;
using AppServiceCore.Models.AssessmentSuite;
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
