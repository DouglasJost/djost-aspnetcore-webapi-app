using Microsoft.Identity.Client;
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

    public enum ChatCompletionRole
    {
        system,
        assistant,
        user
    }

    public enum ChatCompletionServiceProviderType
    {
      OpenAI,
      AzureOpenAI
    }

    //
    // TODO : Use FluentValidation
    // https://docs.fluentvalidation.net/en/latest/index.html
    //
    public class ChatCompletionRequestDto
    {
        public string model { get; set; } = "gpt-4o";
        public List<ChatCompletionMessageDto> messages { get; set; }
        public int? max_tokens { get; set; }
        public decimal? temperature { get; set; }
        public response_format_type? response_format { get; set; }
        public decimal? top_p { get; set; }
        public ChatCompletionServiceProviderType chatCompletionServiceProvider { get; set; } = ChatCompletionServiceProviderType.OpenAI;
  }

  public class ChatCompletionEntity
  {
    public string model { get; set; } = "gpt-4o";
    public List<ChatCompletionMessageDto> messages { get; set; }
    public int? max_tokens { get; set; }
    public decimal? temperature { get; set; }
    public response_format_type? response_format { get; set; }
    public decimal? top_p { get; set; }
  }

  public class ChatCompletionMessageDto
  {
      public string content { get; set; }
      public ChatCompletionRole role { get; set; }
  }

  public class response_format_type
  {
      public string type { get; set; }
  }
}
