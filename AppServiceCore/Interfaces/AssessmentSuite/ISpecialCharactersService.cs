namespace AppServiceCore.Interfaces.AssessmentSuite
{
    public interface ISpecialCharactersService
    {
        (string SentenceWithoutSpecialCharacters, string RemovedSpecialCharacters) RemoveSpecialCharacters123(string sentence);
    }
}
