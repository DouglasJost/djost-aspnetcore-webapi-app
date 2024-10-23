using AppServiceCore;
using Microsoft.Extensions.Logging;
using OpenAiChatCompletions.Interfaces;
using OpenAiChatCompletions.Models.ChatCompletion;
using OpenAiChatCompletions.Models.MedicalVisitNote;
using OpenAiChatCompletions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAiChatCompletions.Repositories
{
    public class ChatCompletionsRepository : IChatCompletionsRepository
    {
        private readonly ILogger<ChatCompletionsRepository> _logger;
        private readonly IVisitNoteTranscriptService _visitNoteTranscriptService;
        private readonly ISoapNoteService _soapNoteService;
        private readonly IOpenAiChatCompletionRepository _openAiChatCompletionRepository;

        public ChatCompletionsRepository(
            ILogger<ChatCompletionsRepository> logger,
            IVisitNoteTranscriptService visitNoteTranscriptService,
            ISoapNoteService soapNoteService,
            IOpenAiChatCompletionRepository openAiChatCompletionRepository)
        {
            _logger = logger;
            _visitNoteTranscriptService = visitNoteTranscriptService;
            _soapNoteService = soapNoteService;
            _openAiChatCompletionRepository = openAiChatCompletionRepository;
        }

        public async Task<CommandResult<ChatCompletionResponseDto>> GetCompletionAsync(ChatCompletionRequestDto request)
        {
            try
            {
                var response = await _openAiChatCompletionRepository.GetOpenAiChatCompletionAsync(request);
                return CommandResult<ChatCompletionResponseDto>.Success(response);
            }
            catch (Exception ex)
            {
                var sbError = new StringBuilder();
                sbError.Append("ChatCompletionsRepository.GetCompletionAsync:  Error calling chat completion REST endpoint.");
                _logger.LogError(ex, sbError.ToString());

                sbError.Append($"  {ExceptionUtilities.AppendExceptionMessages(ex)}");
                return CommandResult<ChatCompletionResponseDto>.Failure(sbError.ToString());
            }
        }

        public async Task<CommandResult<SoapNoteResponseDto>> GetMedicalSoapNoteAsync()
        {
            var request = new SoapNoteRequestDto
            {
                // TranscriptionText = _visitNoteTranscriptService.GetBasicTranscript(),
                TranscriptionText = _visitNoteTranscriptService.GetSeriousTranscript(),
                TranscriptionLanguage = TranscriptionLanguageType.English,
                VisitId = Guid.Empty
            };

            var response = await _soapNoteService.GetSoapNoteAsync(request);
            return response;
        }
    }
}
