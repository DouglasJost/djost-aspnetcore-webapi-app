using AppServiceCore.Models.AssessmentSuite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServiceCore.Interfaces.AssessmentSuite
{
    public interface IAssessmentSuiteService
    {
        CommandResult<ToTileCaseResponseDto> ToTitleCase(ToTitleCaseRequestDto request);

        CommandResult<ReverseStringResponseDto> ReverseString(ReverseStringRequestDto request);

        CommandResult<ContainsOnlyDigitsResponseDto> ArrayContainsOnlyDigits(ContainsOnlyDigitsRequestDto request);

        CommandResult<DuplicateNumberResponseDto> FindDuplicateNumber(DuplicateNumberRequestDto request);

        CommandResult<DuplicateNumbersResponseDto> FindDuplicateNumbers(DuplicateNumbersRequestDto request);

        CommandResult<RemoveDuplicateNumbersResponseDto> RemoveDuplicateNumbers(RemoveDuplicateNumbersRequestDto request);

        CommandResult<RemoveDuplicateNumbersResponseDto> RemoveDuplicateNumbersNoLibraries(RemoveDuplicateNumbersRequestDto request);

        CommandResult<MinMaxNumbersResponseDto> FindMinMaxNumber(MinMaxNumbersRequestDto request);

        CommandResult<RemoveSpecialCharactersResponseDto> RemoveSpecialCharacters(RemoveSpecialCharactersRequestDto sentence);

        CommandResult<IpAddressValidationResponseDto> IsValidIpAddress(IpAddressValidationRequestDto request);

        CommandResult<CompareNumberToValueResponseDto> CountLessThanEqualToGreaterThanCompareValue(CompareNumberToValueRequestDto request);

        CommandResult<ConvertTimeFormResponseDto> ConvertFrom12To24HoursFormat(ConvertTimeFormatRequestDto request);

        CommandResult<FormatAlphabetAlternatingCaseResponseDto> FormatAlphabetAlternatingCase(FormatAlphabetAlternatingCaseRequestDto request);
    }
}
