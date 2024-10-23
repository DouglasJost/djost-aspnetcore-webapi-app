using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAiChatCompletions.Interfaces
{
    public interface IVisitNoteTranscriptService
    {
        string GetBasicTranscript();
        string GetSeriousTranscript();
    }
}
