using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenAiChatCompletions.Interfaces;
using OpenAiChatCompletions.Models.ChatCompletion;
using System.Threading.Tasks;

namespace DjostAspNetCoreWebServer.Controllers.OpenAiChatCompletions
{
    // http://localhost:5149/api/v2/OpenAiChatCompletions/MedicalSoapNote

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion(1)]
    [Authorize]
    public class OpenAiChatCompletionsController : ControllerBase
    {
        private readonly IChatCompletionsRepository _chatCompletionsRepository;
        public OpenAiChatCompletionsController(IChatCompletionsRepository chatCompletionsRepository)
        {
            _chatCompletionsRepository = chatCompletionsRepository;
        }

        [Route("ChatGPT")]
        [HttpPost]
        public async Task<IActionResult> GetCompletionsAsync([FromBody] ChatCompletionRequestDto request)
        {
            var result = await _chatCompletionsRepository.GetCompletionAsync(request);
            return Ok(result);
        }

        [Route("MedicalSoapNote")]
        [HttpPost]
        public async Task<IActionResult> GetMedicalSoapNote()
        {
            var result = await _chatCompletionsRepository.GetMedicalSoapNoteAsync();
            return Ok(result);
        }
    }
}