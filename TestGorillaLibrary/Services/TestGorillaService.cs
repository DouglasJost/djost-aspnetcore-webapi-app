using AppServiceCore;
using AppServiceCore.Interfaces.TestGorilla;
using AppServiceCore.Models.TestGorilla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestGorillaLibrary.Services
{
    public class TestGorillaService : ITestGorillaService
    {
        private readonly IArrayCodingQuestionsService _arrayCodingQuestionsService;
        private readonly ISpecialCharactersService _specialCharactersService;
        private readonly IIpAddressValidationService _ipAddressValidationService;

        public TestGorillaService(
            IArrayCodingQuestionsService arrayCodingQuestionsService,
            ISpecialCharactersService specialCharactersService,
            IIpAddressValidationService ipAddressValidationService)
        {
            _arrayCodingQuestionsService = arrayCodingQuestionsService;
            _specialCharactersService = specialCharactersService;
            _ipAddressValidationService = ipAddressValidationService;
        }

        public CommandResult<ReverseStringResponseDto> ReverseString(ReverseStringRequestDto request)
        {
            var originalString = request.Request;
            if (string.IsNullOrEmpty(originalString))
            {
                return CommandResult<ReverseStringResponseDto>.Failure("String to reverse cannot be null or empty.");
            }

            var response = new ReverseStringResponseDto
            {
                Request = originalString,
                Response = string.Empty
            };

            try
            {
                response.Response = _arrayCodingQuestionsService.ReverseString(originalString);
            }
            catch (Exception ex)
            {
                return CommandResult<ReverseStringResponseDto>.Failure(ExceptionUtilities.AppendExceptionMessages(ex));
            }

            return CommandResult<ReverseStringResponseDto>.Success(response);
        }

        public CommandResult<ContainsOnlyDigitsResponseDto> ArrayContainsOnlyDigits(ContainsOnlyDigitsRequestDto request)
        {
            var originalChars = request.Chars;
            if (originalChars == null || originalChars.Length <= 0)
            {
                return CommandResult<ContainsOnlyDigitsResponseDto>.Failure("Request chars array cannot be empty.");
            }

            var response = new ContainsOnlyDigitsResponseDto
            {
                Chars = originalChars,
                IsAllDigits = false,
            };

            try
            {
                response.IsAllDigits = _arrayCodingQuestionsService.ArrayContainsOnlyDigits(originalChars);
            }
            catch (Exception ex)
            {
                return CommandResult<ContainsOnlyDigitsResponseDto>.Failure(ExceptionUtilities.AppendExceptionMessages(ex));
            }

            return CommandResult<ContainsOnlyDigitsResponseDto>.Success(response);
        }

        public CommandResult<DuplicateNumberResponseDto> FindDuplicateNumber(DuplicateNumberRequestDto request)
        {
            var ints = request.Numbers;
            if (ints == null || ints.Length <= 0)
            {
                return CommandResult<DuplicateNumberResponseDto>.Failure("Request integer array cannot be empty.");
            }

            var response = new DuplicateNumberResponseDto
            {
                Numbers = ints,
                DuplicateNumber = null,
            };

            try
            {
                response.DuplicateNumber = _arrayCodingQuestionsService.FindDuplicateNumber(ints);
            }
            catch (Exception ex)
            {
                return CommandResult<DuplicateNumberResponseDto>.Failure(ExceptionUtilities.AppendExceptionMessages(ex));
            }

            return CommandResult<DuplicateNumberResponseDto>.Success(response);
        }

        public CommandResult<DuplicateNumbersResponseDto> FindDuplicateNumbers(DuplicateNumbersRequestDto request)
        {
            var ints = request.Numbers;
            if (ints == null || ints.Length <= 0)
            {
                return CommandResult<DuplicateNumbersResponseDto>.Failure("Request integer array cannot be empty.");
            }

            var response = new DuplicateNumbersResponseDto
            {
                Numbers = ints,
                Duplicates = null,
            };

            try
            {
                response.Duplicates = _arrayCodingQuestionsService.FindDuplicateNumbers(ints);
            }
            catch (Exception ex)
            {
                return CommandResult<DuplicateNumbersResponseDto>.Failure(ExceptionUtilities.AppendExceptionMessages(ex));
            }

            return CommandResult<DuplicateNumbersResponseDto>.Success(response);
        }

        public CommandResult<RemoveDuplicateNumbersResponseDto> RemoveDuplicateNumbers(RemoveDuplicateNumbersRequestDto request)
        {
            var numbers = request.Numbers;
            if (numbers == null || numbers.Length <= 0)
            {
                return CommandResult<RemoveDuplicateNumbersResponseDto>.Failure("Request integer array cannot be empty.");
            }

            var response = new RemoveDuplicateNumbersResponseDto
            {
                Numbers = numbers,
                UniqueNumbers = null,
            };

            try
            {
                response.UniqueNumbers = _arrayCodingQuestionsService.RemoveDuplicateNumbers(numbers);
            }
            catch (Exception ex)
            {
                return CommandResult<RemoveDuplicateNumbersResponseDto>.Failure(ExceptionUtilities.AppendExceptionMessages(ex));
            }

            return CommandResult<RemoveDuplicateNumbersResponseDto>.Success(response);
        }

        public CommandResult<RemoveDuplicateNumbersResponseDto> RemoveDuplicateNumbersNoLibraries(RemoveDuplicateNumbersRequestDto request)
        {
            var numbers = request.Numbers;
            if (numbers == null || numbers.Length <= 0)
            {
                return CommandResult<RemoveDuplicateNumbersResponseDto>.Failure("Request integer array cannot be empty.");
            }

            var response = new RemoveDuplicateNumbersResponseDto
            {
                Numbers = numbers,
                UniqueNumbers = null,
            };

            try
            {
                response.UniqueNumbers = _arrayCodingQuestionsService.RemoveDuplicateNumbersNoLibraries(numbers);
            }
            catch (Exception ex)
            {
                return CommandResult<RemoveDuplicateNumbersResponseDto>.Failure(ExceptionUtilities.AppendExceptionMessages(ex));
            }

            return CommandResult<RemoveDuplicateNumbersResponseDto>.Success(response);
        }

        public CommandResult<MinMaxNumbersResponseDto> FindMinMaxNumber(MinMaxNumbersRequestDto request)
        {
            var numbers = request.Numbers;
            if (numbers == null || numbers.Length <= 0)
            {
                return CommandResult<MinMaxNumbersResponseDto>.Failure("Request integer array cannot be empty.");
            }

            var response = new MinMaxNumbersResponseDto
            {
                Numbers = numbers,
                MaxNumber = null,
                MinNumber = null,
            };

            try
            {
                var result = _arrayCodingQuestionsService.FindMinMaxNumber(numbers);
                response.MaxNumber = result.MaxNumber;
                response.MinNumber = result.MinNumber;
            }
            catch (Exception ex)
            {
                return CommandResult<MinMaxNumbersResponseDto>.Failure(ExceptionUtilities.AppendExceptionMessages(ex));
            }

            return CommandResult<MinMaxNumbersResponseDto>.Success(response);
        }

        public CommandResult<RemoveSpecialCharactersResponseDto> RemoveSpecialCharacters(RemoveSpecialCharactersRequestDto request)
        {
            var sentence = request.Sentence;
            if (string.IsNullOrEmpty(sentence))
            {
                return CommandResult<RemoveSpecialCharactersResponseDto>.Failure("Request sentence string cannot be null or empty.");
            }

            var response = new RemoveSpecialCharactersResponseDto
            {
                OriginalSentence = sentence,
                SentenceWithoutSpecialCharacters = string.Empty,
                RemovedSpecialCharacters = string.Empty,
            };

            try
            {
                var result = _specialCharactersService.RemoveSpecialCharacters(sentence);
                response.SentenceWithoutSpecialCharacters = result.SentenceWithoutSpecialCharacters;
                response.RemovedSpecialCharacters = result.RemovedSpecialCharacters;
            }
            catch (Exception ex)
            {
                return CommandResult<RemoveSpecialCharactersResponseDto>.Failure(ExceptionUtilities.AppendExceptionMessages(ex));
            }

            return CommandResult<RemoveSpecialCharactersResponseDto>.Success(response);
        }

        public CommandResult<IpAddressValidationResponseDto> IsValidIpAddress(IpAddressValidationRequestDto request)
        {
            var ipAddress = request.IpAddress;
            if (string.IsNullOrEmpty(ipAddress))
            {
                return CommandResult<IpAddressValidationResponseDto>.Failure("Request IP Address string cannot be null or empty.");
            }

            var response = new IpAddressValidationResponseDto
            {
                IpAddress = ipAddress,
                IsValidAddress = false,
            };

            try
            {
                response.IsValidAddress = _ipAddressValidationService.IsValidIpAddress(ipAddress);
            }
            catch (Exception ex)
            {
                return CommandResult<IpAddressValidationResponseDto>.Failure(ExceptionUtilities.AppendExceptionMessages(ex));
            }

            return CommandResult<IpAddressValidationResponseDto>.Success(response);
        }

        public CommandResult<CompareNumberToValueResponseDto> CountLessThanEqualToGreaterThanCompareValue(CompareNumberToValueRequestDto request)
        {
            var compareValue = request.CompareValue;
            var numbers = request.Numbers;

            if (compareValue == null || numbers == null || numbers.Length <= 0)
            {
                return CommandResult<CompareNumberToValueResponseDto>.Failure("Numbers array and CompareValue cannot be null or empty.");
            }

            var response = new CompareNumberToValueResponseDto
            {
                CompareValue = compareValue,
                Numbers = numbers,
                LessThanCount = 0,
                EqualToCount = 0,
                GreaterThanCount = 0,
            };

            try
            {
                var result = _arrayCodingQuestionsService.CountLessThanEqualToGreaterThanCompareValue(compareValue.Value, numbers);
                response.LessThanCount = result.lessCnt;
                response.EqualToCount = result.equalCnt;
                response.GreaterThanCount = result.greaterCnt;
            }
            catch (Exception ex)
            {
                return CommandResult<CompareNumberToValueResponseDto>.Failure(ExceptionUtilities.AppendExceptionMessages(ex));
            }

            return CommandResult<CompareNumberToValueResponseDto>.Success(response);
        }

        public CommandResult<ConvertTimeFormResponseDto> ConvertFrom12To24HoursFormat(ConvertTimeFormatRequestDto request)
        {
            var time12Format = request.InputTime;
            if (string.IsNullOrEmpty(time12Format))
            {
                return CommandResult<ConvertTimeFormResponseDto>.Failure("Input time cannot be null or empty.");
            }

            var response = new ConvertTimeFormResponseDto
            {
                InputTime = time12Format,
                OutputTime = string.Empty,
            };

            try
            {
                response.OutputTime = _arrayCodingQuestionsService.ConvertFrom12To24HoursFormat(time12Format);
            }
            catch (Exception ex)
            {
                return CommandResult<ConvertTimeFormResponseDto>.Failure(ExceptionUtilities.AppendExceptionMessages(ex));
            }

            return CommandResult<ConvertTimeFormResponseDto>.Success(response);
        }

        public CommandResult<FormatAlphabetAlternatingCaseResponseDto> FormatAlphabetAlternatingCase(FormatAlphabetAlternatingCaseRequestDto request)
        {
            var response = new FormatAlphabetAlternatingCaseResponseDto
            {
                IsFirstCharUpper = request.IsFirstCharUpper,
                Alphabet = string.Empty,
            };

            try
            {
                response.Alphabet = _arrayCodingQuestionsService.FormatAlphabetAlternatingCase(request.IsFirstCharUpper);
            }
            catch (Exception ex)
            {
                return CommandResult<FormatAlphabetAlternatingCaseResponseDto>.Failure(ExceptionUtilities.AppendExceptionMessages(ex));
            }

            return CommandResult<FormatAlphabetAlternatingCaseResponseDto>.Success(response);
        }
    }
}
