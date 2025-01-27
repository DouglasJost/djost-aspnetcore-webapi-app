using AppServiceCore.Models.AssessmentSuite;
using System.Collections.Generic;

namespace AppServiceCore.Interfaces.AssessmentSuite
{
    public interface IArrayCodingQuestionsService
    {
        public bool AreBracketsBalanced(string testStr);

        (string SentenceWithoutSpecialCharacters, string RemovedSpecialCharacters) RemoveSpecialCharacters(string sentence);

        string ReverseString(string str);

        bool ArrayContainsOnlyDigits(char[] chars);

        int? FindDuplicateNumber(int[] numbers);

        List<int> FindDuplicateNumbers(int[] numbers);

        List<int> RemoveDuplicateNumbers(int[] numbers);

        List<int> RemoveDuplicateNumbersNoLibraries(int[] numbers);

        (int MinNumber, int MaxNumber) FindMinMaxNumber(int[] numbers);

        void FindMinMaxNumber(int[] numbers, out int? minNumber, out int? maxNumber);

        (int lessCnt, int equalCnt, int greaterCnt) CountLessThanEqualToGreaterThanCompareValue(int compareVal, int[] numbers);

        string ConvertFrom12To24HoursFormat(string inputTime);

        string FormatAlphabetAlternatingCase(bool UppercaseFirst);
    }
}
