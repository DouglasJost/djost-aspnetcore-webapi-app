using AppServiceCore.Interfaces.TestGorilla;
using AppServiceCore.Models.TestGorilla;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace DjostAspNetCoreWebServer.Controllers.TestGorilla
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TestGorillaController : ControllerBase
    {
        private readonly ILogger<TestGorillaController> _logger;
        private readonly IIpAddressValidationService _ipAddressValidationService;
        private readonly ISpecialCharactersService _specialCharactersService;
        private readonly IArrayCodingQuestionsService _arrayCodingQuestionsService;
        private readonly ITestGorillaService _testGorillaService;

        public TestGorillaController(
            ILogger<TestGorillaController> logger,
            IIpAddressValidationService ipAddressValidationService,
            ISpecialCharactersService specialCharactersService,
            IArrayCodingQuestionsService arrayCodingQuestionsService,
            ITestGorillaService testGorillaService)
        {
            _logger = logger;
            _ipAddressValidationService = ipAddressValidationService;
            _specialCharactersService = specialCharactersService;
            _arrayCodingQuestionsService = arrayCodingQuestionsService;
            _testGorillaService = testGorillaService;
        }

        [Route("ToTitleCase")]
        [HttpPost]
        public IActionResult ToTitleCase([FromBody] ToTitleCaseRequestDto request)
        {
            var response = _testGorillaService.ToTitleCase(request);
            return Ok(response);
        }


        [Route("ReverseString")]
        [HttpPost]
        public IActionResult ReverseString([FromBody] ReverseStringRequestDto request)
        {
            var response = _testGorillaService.ReverseString(request);
            return Ok(response);
        }

        [Route("ArrayContainsOnlyDigits")]
        [HttpPost]
        public IActionResult ArrayContainsOnlyDigits([FromBody] ContainsOnlyDigitsRequestDto request)
        {
            var result = _testGorillaService.ArrayContainsOnlyDigits(request);
            return Ok(result);
        }

        [Route("FindDuplicateNumber")]
        [HttpPost]
        public IActionResult FindDuplicateNumber([FromBody] DuplicateNumberRequestDto request)
        {
            var result = _testGorillaService.FindDuplicateNumber(request);
            return Ok(result);
        }

        [Route("FindDuplicateNumbers")]
        [HttpPost]
        public IActionResult FindDuplicateNumbers([FromBody] DuplicateNumbersRequestDto request)
        {
            var result = _testGorillaService.FindDuplicateNumbers(request);
            return Ok(result);
        }

        [Route("RemoveDuplicateNumbers")]
        [HttpPost]
        public IActionResult RemoveDuplicateNumbers([FromBody] RemoveDuplicateNumbersRequestDto request)
        {
            var response = _testGorillaService.RemoveDuplicateNumbers(request);
            return Ok(response);
        }

        [Route("RemoveDuplicateNumbersNoLibraries")]
        [HttpPost]
        public IActionResult RemoveDuplicateNumbersNoLibraries([FromBody] RemoveDuplicateNumbersRequestDto request)
        {
            var response = _testGorillaService.RemoveDuplicateNumbersNoLibraries(request);
            return Ok(response);
        }

        [Route("FindMinMaxNumber")]
        [HttpPost]
        public IActionResult FindMinMaxNumber([FromBody] MinMaxNumbersRequestDto request)
        {
            var response = _testGorillaService.FindMinMaxNumber(request);
            return Ok(response);
        }

        [Route("RemoveSpecialCharacters")]
        [HttpPost]
        public IActionResult RemoveSpecialCharacters([FromBody] RemoveSpecialCharactersRequestDto request)
        {
            var result = _testGorillaService.RemoveSpecialCharacters(request);
            return Ok(result);
        }

        [Route("IsValidIpAddress")]
        [HttpPost]
        public IActionResult IsValidIpAddress([FromBody] IpAddressValidationRequestDto request)
        {
            var result = _testGorillaService.IsValidIpAddress(request);
            return Ok(result);
        }

        [Route("CountLessThanEqualToGreaterThanCompareValue")]
        [HttpPost]
        public IActionResult CountLessThanEqualToGreaterThanCompareValue([FromBody] CompareNumberToValueRequestDto request)
        {
            var result = _testGorillaService.CountLessThanEqualToGreaterThanCompareValue(request);
            return Ok(result);
        }

        [Route("ConvertFrom12To24HoursFormat")]
        [HttpPost]
        public IActionResult ConvertFrom12To24HoursFormat(ConvertTimeFormatRequestDto request)
        {
            var result = _testGorillaService.ConvertFrom12To24HoursFormat(request);
            return Ok(result);
        }

        [Route("FormatAlphabetAlternatingCase")]
        [HttpPost]
        public IActionResult FormatAlphabetAlternatingCase(FormatAlphabetAlternatingCaseRequestDto request)
        {
            var result = _testGorillaService.FormatAlphabetAlternatingCase(request);
            return Ok(result);
        }
    }
}
