using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAiChatCompletions.Models.MedicalVisitNote
{
    public enum TranscriptionLanguageType
    {
        English,
        Spanish
    }

    public class SoapNoteRequestDto
    {
        public string TranscriptionText { get; set; }
        public TranscriptionLanguageType TranscriptionLanguage { get; set; } = TranscriptionLanguageType.English;
        public Guid? VisitId { get; set; }
    }
}
