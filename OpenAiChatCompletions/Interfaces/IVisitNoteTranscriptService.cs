namespace OpenAiChatCompletions.Interfaces
{
    public interface IVisitNoteTranscriptService
    {
        string GetBasicTranscript();
        string GetSeriousTranscript();
    }
}
