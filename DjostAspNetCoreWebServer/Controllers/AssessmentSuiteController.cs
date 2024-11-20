using AppServiceCore.Interfaces.AssessmentSuite;
using AppServiceCore.Models.AssessmentSuite;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DjostAspNetCoreWebServer.Controllers.AssessmentSuite
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion(1)]
    [Authorize]
    public class AssessmentSuiteController : ControllerBase
    {
        private readonly IIpAddressValidationService _ipAddressValidationService;
        private readonly ISpecialCharactersService _specialCharactersService;
        private readonly IArrayCodingQuestionsService _arrayCodingQuestionsService;
        private readonly IAssessmentSuiteService _AssessmentSuiteService;

        public AssessmentSuiteController(
            IIpAddressValidationService ipAddressValidationService,
            ISpecialCharactersService specialCharactersService,
            IArrayCodingQuestionsService arrayCodingQuestionsService,
            IAssessmentSuiteService AssessmentSuiteService)
        {
            //_logger = logger;
            _ipAddressValidationService = ipAddressValidationService;
            _specialCharactersService = specialCharactersService;
            _arrayCodingQuestionsService = arrayCodingQuestionsService;
            _AssessmentSuiteService = AssessmentSuiteService;
        }

        [Route("SingletonUseCase")]
        [HttpGet]
        public IActionResult SingletonUseCase()
        {
            var response = _AssessmentSuiteService.SingletonUseCase();
            return Ok(response);
        }


        [Route("ToTitleCase")]
        [HttpPost]
        public IActionResult ToTitleCase([FromBody] ToTitleCaseRequestDto request)
        {
            var response = _AssessmentSuiteService.ToTitleCase(request);
            return Ok(response);
        }


        [Route("ReverseString")]
        [HttpPost]
        public IActionResult ReverseString([FromBody] ReverseStringRequestDto request)
        {
            var response = _AssessmentSuiteService.ReverseString(request);
            return Ok(response);
        }

        [Route("ArrayContainsOnlyDigits")]
        [HttpPost]
        public IActionResult ArrayContainsOnlyDigits([FromBody] ContainsOnlyDigitsRequestDto request)
        {
            var result = _AssessmentSuiteService.ArrayContainsOnlyDigits(request);
            return Ok(result);
        }

        [Route("FindDuplicateNumber")]
        [HttpPost]
        public IActionResult FindDuplicateNumber([FromBody] DuplicateNumberRequestDto request)
        {
            var result = _AssessmentSuiteService.FindDuplicateNumber(request);
            return Ok(result);
        }

        [Route("FindDuplicateNumbers")]
        [HttpPost]
        public IActionResult FindDuplicateNumbers([FromBody] DuplicateNumbersRequestDto request)
        {
            var result = _AssessmentSuiteService.FindDuplicateNumbers(request);
            return Ok(result);
        }

        [Route("RemoveDuplicateNumbers")]
        [HttpPost]
        public IActionResult RemoveDuplicateNumbers([FromBody] RemoveDuplicateNumbersRequestDto request)
        {
            var response = _AssessmentSuiteService.RemoveDuplicateNumbers(request);
            return Ok(response);
        }

        [Route("RemoveDuplicateNumbersNoLibraries")]
        [HttpPost]
        public IActionResult RemoveDuplicateNumbersNoLibraries([FromBody] RemoveDuplicateNumbersRequestDto request)
        {
            var response = _AssessmentSuiteService.RemoveDuplicateNumbersNoLibraries(request);
            return Ok(response);
        }

        [Route("FindMinMaxNumber")]
        [HttpPost]
        public IActionResult FindMinMaxNumber([FromBody] MinMaxNumbersRequestDto request)
        {
            var response = _AssessmentSuiteService.FindMinMaxNumber(request);
            return Ok(response);
        }

        [Route("RemoveSpecialCharacters")]
        [HttpPost]
        public IActionResult RemoveSpecialCharacters([FromBody] RemoveSpecialCharactersRequestDto request)
        {
            var result = _AssessmentSuiteService.RemoveSpecialCharacters(request);
            return Ok(result);
        }

        [Route("IsValidIpAddress")]
        [HttpPost]
        public IActionResult IsValidIpAddress([FromBody] IpAddressValidationRequestDto request)
        {
            var result = _AssessmentSuiteService.IsValidIpAddress(request);
            return Ok(result);
        }

        [Route("CountLessThanEqualToGreaterThanCompareValue")]
        [HttpPost]
        public IActionResult CountLessThanEqualToGreaterThanCompareValue([FromBody] CompareNumberToValueRequestDto request)
        {
            var result = _AssessmentSuiteService.CountLessThanEqualToGreaterThanCompareValue(request);
            return Ok(result);
        }

        [Route("ConvertFrom12To24HoursFormat")]
        [HttpPost]
        public IActionResult ConvertFrom12To24HoursFormat(ConvertTimeFormatRequestDto request)
        {
            var result = _AssessmentSuiteService.ConvertFrom12To24HoursFormat(request);
            return Ok(result);
        }

        [Route("FormatAlphabetAlternatingCase")]
        [HttpPost]
        public IActionResult FormatAlphabetAlternatingCase(FormatAlphabetAlternatingCaseRequestDto request)
        {
            var result = _AssessmentSuiteService.FormatAlphabetAlternatingCase(request);
            return Ok(result);
        }
    }
}
