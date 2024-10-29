using Microsoft.AspNetCore.Mvc;
using OpenAiChatCompletions.Interfaces;
using OpenAiChatCompletions.Models.ChatCompletion;
using System.Threading.Tasks;

namespace DjostAspNetCoreWebServer.Controllers.OpenAiChatCompletions
{
    [ApiController]
    [Route("api/v1/[controller]")]

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