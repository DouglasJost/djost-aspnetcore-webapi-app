using OpenAiChatCompletions.Models.ChatCompletion;
using System;

namespace OpenAiChatCompletions.Models.MedicalVisitNote
{
  public enum TranscriptionLanguageType
    {
        English,
        Spanish
    }

    public class SoapNoteRequestDto
    {
        public ChatCompletionServiceProviderType ChatCompletionServiceProvider { get; set; } = ChatCompletionServiceProviderType.OpenAI;
        public string TranscriptionText { get; set; } = string.Empty;
        public TranscriptionLanguageType TranscriptionLanguage { get; set; } = TranscriptionLanguageType.English;
        public Guid? VisitId { get; set; }
  }
}
