using AppServiceCore;
using AppServiceCore.Logging;
using Microsoft.Extensions.Logging;
using OpenAiChatCompletions.Interfaces;
using OpenAiChatCompletions.Models.ChatCompletion;
using OpenAiChatCompletions.Models.MedicalVisitNote;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpenAiChatCompletions.Repositories
{
    public class ChatCompletionsRepository : IChatCompletionsRepository
    {
        private readonly ILogger _logger = AppLogger.GetLogger(LoggerCategoryType.OpenAiChatCompletions);

        private readonly IVisitNoteTranscriptService _visitNoteTranscriptService;
        private readonly ISoapNoteService _soapNoteService;
        private readonly IOpenAiChatCompletionRepository _openAiChatCompletionRepository;

        public ChatCompletionsRepository(
            IVisitNoteTranscriptService visitNoteTranscriptService,
            ISoapNoteService soapNoteService,
            IOpenAiChatCompletionRepository openAiChatCompletionRepository)
        {
            _visitNoteTranscriptService = visitNoteTranscriptService;
            _soapNoteService = soapNoteService;
            _openAiChatCompletionRepository = openAiChatCompletionRepository;
        }

        public async Task<CommandResult<ChatCompletionResponseDto>> GetCompletionAsync(ChatCompletionRequestDto request)
        {
            try
            {
                ChatCompletionEntity entity = new ChatCompletionEntity
                {
                  model = request.model,
                  messages = request.messages,
                  max_tokens = request.max_tokens,
                  temperature = request.temperature,
                  response_format = request.response_format,
                  top_p = request.top_p
                };

                var response = await _openAiChatCompletionRepository.GetOpenAiChatCompletionAsync(entity, request.chatCompletionServiceProvider);
                return CommandResult<ChatCompletionResponseDto>.Success(response);
            }
            catch (Exception ex)
            {
                var sbError = new StringBuilder("ChatCompletionsRepository.GetCompletionAsync:  Error calling chat completion REST endpoint.");
                sbError.Append($"  {ExceptionUtilities.AppendExceptionMessages(ex)}");
                _logger.LogError(ex, sbError.ToString());

                return CommandResult<ChatCompletionResponseDto>.Failure(sbError.ToString());
            }
        }

        public async Task<CommandResult<SoapNoteResponseDto>> GetMedicalSoapNoteAsync(SoapNoteRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.TranscriptionText))
            {
              // request.TranscriptionText = _visitNoteTranscriptService.GetSeriousTranscript();
              request.TranscriptionText = _visitNoteTranscriptService.GetBasicTranscript();
            }

            var response = await _soapNoteService.GetSoapNoteAsync(request);
            return response;
        }
    }
}
