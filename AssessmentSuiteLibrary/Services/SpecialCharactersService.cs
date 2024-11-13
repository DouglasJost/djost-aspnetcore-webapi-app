using AppServiceCore.Interfaces.AssessmentSuite;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssessmentSuiteLibrary.Services
{
    public class SpecialCharactersService : ISpecialCharactersService
    {
        public (string SentenceWithoutSpecialCharacters, string RemovedSpecialCharacters) RemoveSpecialCharacters123(string sentence)
        {
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
    }
}
