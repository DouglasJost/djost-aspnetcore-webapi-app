using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAiChatCompletions.Models.ChatCompletion
{
    //
    // NOTE:  Case for the various Chat Completion attributes must match OpenAI attribute names.
    //   

    public class ChatCompletionResponseDto
    {
        public CompletionResponseDto completion_response { get; set; }
        public ErrorResponseDto error_response { get; set; }
    }

    public class ErrorResponseDto
    {
        public int status_code { get; set; }
        public string status_code_name { get; set; }
        public ErrorDto error { get; set; }
    }

    public class ErrorDto
    {
        public string code { get; set; }
        public string message { get; set; }
        public string param { get; set; }
        public string type { get; set; }
    }

    public class CompletionResponseDto
    {
        public List<ChoiceDto> choices { get; set; }
        public int created { get; set; }
        public string id { get; set; }
        public string model { get; set; }
        public string @object { get; set; }
        public string system_fingerprint { get; set; }
        public UsageDto usage { get; set; }
    }

    public class ChoiceDto
    {
        public string finish_reason { get; set; }
        public int index { get; set; }
        public ChatCompletionMessageDto message { get; set; }
    }

    public class UsageDto
    {
        public int completion_tokens { get; set; }
        public int prompt_tokens { get; set; }
        public int total_tokens { get; set; }
    }

}
