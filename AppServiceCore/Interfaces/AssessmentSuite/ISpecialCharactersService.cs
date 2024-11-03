using AppServiceCore.Models.AssessmentSuite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServiceCore.Interfaces.AssessmentSuite
{
    public interface ISpecialCharactersService
    {
        (string SentenceWithoutSpecialCharacters, string RemovedSpecialCharacters) RemoveSpecialCharacters(string sentence);
    }
}
