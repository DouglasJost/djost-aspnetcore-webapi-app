using AppServiceCore;
using AppServiceCore.Interfaces.TestGorilla;
using AppServiceCore.Models.TestGorilla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestGorillaLibrary.Services
{
    public class ArrayCodingQuestionsService : IArrayCodingQuestionsService
    {
        public string ReverseString(string str)
        {
            //  Reverse a given string.
            //  ======================
            //  Hello   becomes olleh
            //  Goodbye becomes eybdoog

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
