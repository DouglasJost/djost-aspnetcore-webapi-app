using AppServiceCore;
using OpenAiChatCompletions.Models.MedicalVisitNote;
using System.Threading.Tasks;

namespace OpenAiChatCompletions.Interfaces
{
    public interface ISoapNoteService
    {
        Task<CommandResult<SoapNoteResponseDto>> GetSoapNoteAsync(SoapNoteRequestDto request);
    }
}
