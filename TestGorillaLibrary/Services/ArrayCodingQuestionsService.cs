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
    }
}
