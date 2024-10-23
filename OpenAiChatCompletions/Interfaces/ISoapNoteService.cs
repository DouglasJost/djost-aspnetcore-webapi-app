using AppServiceCore;
using OpenAiChatCompletions.Models.MedicalVisitNote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAiChatCompletions.Interfaces
{
    public interface ISoapNoteService
    {
        Task<CommandResult<SoapNoteResponseDto>> GetSoapNoteAsync(SoapNoteRequestDto request);
    }
}
