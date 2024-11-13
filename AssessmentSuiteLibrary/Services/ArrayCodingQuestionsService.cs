using AppServiceCore.Interfaces.AssessmentSuite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssessmentSuiteLibrary.Services
{
    public class ArrayCodingQuestionsService : IArrayCodingQuestionsService
    {
        /*
             Open brackets must be closed in the correct order.
        
             Every close bracket has a corresponding open bracket of the same type.
        
             Example 1:
             Input: s = "()"
             Output: true
        
             Example 2:
             Input: s = "()[({})]{}"
             Output: true
        
             Example 3:
             Input: s = "([)]"
             Output: false
        */
        public bool AreBracketsBalanced(string testStr)
        {
            Stack<char> openBrackets = new System.Collections.Generic.Stack<char>();

            Dictionary<char, char> bracketPairs = new Dictionary<char, char>
            {
                { ')', '(' },
                { ']', '[' },
                { '}', '{' }
            };
            
            foreach(var chr in testStr)
            {
                if (chr == '(' || chr == '[' || chr == '{')
                {
                    // push opening bracket to stack
                    openBrackets.Push(chr);
                }
                else if (chr == ')' || chr == ']' || chr == '}')
                {
                    // check if stack is empty or top of stack is not the closing bracket
                    if (openBrackets.Count == 0 || openBrackets.Peek() != bracketPairs[chr])
                    {
                        return false;
                    }
                    openBrackets.Pop();
                }
            }

            return (openBrackets.Count == 0);
        }


        /*
            The given code snippet is supposed to remove all special characters from a string.  The only
            characters allowed in the string are the 26 letters in the English alphabet(A to Z and a to z),
            digits(0 to 9), spaces(), dashes(-) and underscores(_).  The function should return NA if
            the string only contains special characters.

            Uppercase alphabets in ASCII range from 65 ('A') to 90 ('Z')
            Lowercase alphabets in ASCII range from 97 ('a') to 122 ('z')

            Space       ( )  ASCII 32
            Dash/Minus  (-)  ASCII 45
            Underscore  (_)  ASCII 95
            Integer Numbers from 48 (0) to 57 (9)
        */
        public (string SentenceWithoutSpecialCharacters, string RemovedSpecialCharacters) RemoveSpecialCharacters(string sentence)
        {
            var updatedSentence = new StringBuilder();
            HashSet<char> specialCharsHash = new HashSet<char>();

            for (int indx = 0; indx < sentence.Length; indx++)
            {
                var validCharacter = false;

                if (sentence[indx] >= 65 && sentence[indx] <= 90)
                {
                    // char is upper case letter A .. Z
                    validCharacter = true;
                }
                else if (sentence[indx] >= 97 && sentence[indx] <= 122)
                {
                    // char is lower case letter a .. z
                    validCharacter = true;
                }
                else if (sentence[indx] >= 48 && sentence[indx] <= 57)
                {
                    // char is a number 0 .. 9
                    validCharacter = true;
                }
                else if (sentence[indx] == 32)
                {
                    // space
                    validCharacter = true;
                }
                else if (sentence[indx] == 45)
                {
                    // Dash or minus (-)
                    validCharacter = true;
                }
                else if (sentence[indx] == 95)
                {
                    // underscore (_)
                    validCharacter = true;
                }

                if (validCharacter)
                {
                    updatedSentence.AppendFormat("{0}", sentence[indx]);
                }
                else
                {
                    specialCharsHash.Add(sentence[indx]);
                }
            }

            return (updatedSentence.ToString(), new string(specialCharsHash.ToArray()));
        }


        /*
            Reverse a given string.
            ======================
            Hello   becomes olleh
            Goodbye becomes eybdoog
        */
        public string ReverseString(string str)
        {

            if (str.Length <= 1)
            {
                return str;
            }
            return ReverseString(str.Substring(1)) + str[0];
        }


        public bool ArrayContainsOnlyDigits(char[] chars)
        {
            var isAllDigits = true;
            foreach (var c in chars)
            {
                if (!Char.IsDigit(c))
                {
                    isAllDigits = false;
                    break;
                }
            }
            return isAllDigits;
        }


        public int? FindDuplicateNumber(int[] numbers)
        {
            int? duplicateNumber = null;

            // HashSet<int> is preferred for its effinciency in duplicate
            //              detection due to constant-time operations, prevention
            //              of duplicates by design, and optimal memory use.
            HashSet<int> seenNumbers = new HashSet<int>();


            foreach (var number in numbers)
            {
                if (seenNumbers.Contains(number))
                {
                    duplicateNumber = number;
                    break;
                }
                else
                {
                    seenNumbers.Add(number);
                }
            }

            return duplicateNumber;
        }

        public List<int> FindDuplicateNumbers(int[] numbers)
        {
            List<int> duplicates = new List<int>();
            Dictionary<int, int> numberMap = new Dictionary<int, int>();

            foreach (var number in numbers)
            {
                if (numberMap.ContainsKey(number))
                {
                    numberMap[number] = numberMap[number] + 1;
                }
                else
                {
                    numberMap[number] = 1;
                }
            }

            foreach (var entry in numberMap)
            {
                if (entry.Value > 1)
                {
                    duplicates.Add(entry.Key);
                }
            }

            return duplicates.ToList();
        }

        public List<int> RemoveDuplicateNumbers(int[] numbers)
        {
            HashSet<int> uniqueNumbers = new HashSet<int>();
            foreach (var number in numbers)
            {
                if (!uniqueNumbers.Contains(number))
                {
                    uniqueNumbers.Add(number);
                }
            }

            return uniqueNumbers.ToList();
        }

        public List<int> RemoveDuplicateNumbersNoLibraries(int[] numbers)
        {
            int len = numbers.Length;
            int[] temp = new int[len];
            int j = 0;

            for (int i = 0; i < len; i++)
            {
                bool isDup = false;

                for (int k = 0; k < j; k++)
                {
                    if (temp[k] == numbers[i])
                    {
                        isDup = true;
                        break;
                    }
                }

                if (!isDup)
                {
                    temp[j] = numbers[i];
                    j++;
                }
            }

            int[] results = new int[j];
            for (int i = 0; i < j; i++)
            {
                results[i] = temp[i];
            }

            return results.ToList();
        }

        public (int MinNumber, int MaxNumber) FindMinMaxNumber(int[] numbers)
        {
            int minNumber = numbers[0];
            int maxNumber = numbers[0];

            foreach (var number in numbers)
            {
                if (number < minNumber)
                {
                    minNumber = number;
                }
                if (number > maxNumber)
                {
                    maxNumber = number;
                }
            }

            return (minNumber, maxNumber);
        }

        public void FindMinMaxNumber(int[] numbers, out int? minNumber, out int? maxNumber)
        {
            minNumber = null;
            maxNumber = null;

            if (numbers.Length == 0)
            {
                return;
            }

            minNumber = numbers[0];
            maxNumber = numbers[0];

            foreach (var number in numbers)
            {
                if (number < minNumber)
                {
                    minNumber = number;
                }
                if (number > maxNumber)
                {
                    maxNumber = number;
                }
            }
        }


        /*
            CountLessThanEqualToGreaterThanCompareValue()  - returns a tuple

            You are given:

            val1: an integer
            arr : list of integers

            Write a function that takes the array arr, its size n, and the integer val1 as input
            and returns an array containing the count of integers that are smaller than, equal to,
            and greater than val1, respectively.
        */
        public (int lessCnt, int equalCnt, int greaterCnt) CountLessThanEqualToGreaterThanCompareValue(int compareVal, int[] numbers)
        {
            int lessCnt = 0;
            int equalCnt = 0;
            int greaterCnt = 0;

            foreach (var number in numbers)
            {
                if (compareVal < number)
                {
                    lessCnt++;
                }
                else if (compareVal == number)
                {
                    equalCnt++;
                }
                else
                {
                     greaterCnt++;
                }
            }

            return (lessCnt, equalCnt, greaterCnt);
        }

        public string ConvertFrom12To24HoursFormat(string inputTime)
        {
            /*
                Using .NET and C#, you need to convert the time from 12 hours format to 24 hours format.

                Assume the skeleton class TimeConvertor is given.  You need to implement the function
                ConvertFrom12To24HoursFormat(string inputTime).  This function parses the given input
                string (inputTime in 12 hours format) and converts the time to 24 hours format.

                The TimeConvertor class takes a string (InputTime in 12 hours format) and calls the 
                ConvertFrom12To24HoursFormat function to convert the time to 24 hours format.

                Two examples:
                  Input: inputTime = 12:00 am
                  Output: 0:00

                  Input: inputTime = 1:05 pm
                  Output: 13:05
            */

            var time = inputTime.Trim().ToUpper();
            var indexAM = time.IndexOf("AM");
            var indexPM = time.IndexOf("PM");

            time = time.Replace("AM", string.Empty);
            time = time.Replace("PM", string.Empty);

            var timeParts = time.Split(':');
            var hours = int.Parse(timeParts[0]);   // Convert to an int.  May need to do MATH.
            var minutes = timeParts[1];            // Leave as a string.  If converted to an int, will format "05" as "5".


            if (indexAM > -1)
            {
                if (hours == 12)
                {
                    hours = 0;
                }
            }
            else
            {
                if (hours != 12)
                {
                    hours = hours + 12;
                }
            }

            return $"{hours}:{minutes}";
        }

        public string FormatAlphabetAlternatingCase(bool isFirstCharUpper)
        {
            // Uppercase alphabets in ASCII range from 65 ('A') to 90 ('Z')
            // Lowercase alphabets in ASCII range from 97 ('a') to 122 ('z')

            var alphabet = new StringBuilder();

            for (int i = 0; i < 26; i++)
            {
                var baseAsciiCode = 65;
                if (isFirstCharUpper)
                {
                    baseAsciiCode = i % 2 == 0 ? 65 : 97;
                }
                else
                {
                    baseAsciiCode = i % 2 == 0 ? 97 : 65;
                }

                // Calculate current character's ASCII by adding 1 and adjusting case
                var letter = (char)(baseAsciiCode + i);

                alphabet.AppendFormat($"{letter}");
            }

            return alphabet.ToString();
        }
    }
}
