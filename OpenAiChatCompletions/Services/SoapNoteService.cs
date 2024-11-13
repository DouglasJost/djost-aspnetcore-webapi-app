using OpenAiChatCompletions.Models.MedicalVisitNote;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OpenAiChatCompletions.Interfaces;
using AppServiceCore;
using Microsoft.Extensions.Logging;
using OpenAiChatCompletions.Models.ChatCompletion;
using System.Diagnostics;
using Newtonsoft.Json;
using AppServiceCore.Logging;

namespace OpenAiChatCompletions.Services
{
    /*
        This logic is designed to generate a detailed medical summary (Visit Note) based on conversations between a 
        patient and a healthcare provider, using an AI-powered completion service.  The transcription of this conversation
        is sent to an AI-powered service, which responds with structured information that highlights relevant health
        details, such as symptoms (chief complaint), diagnosis, services preformed, medications, and prescribed actions.
        This structured summary provides organized information that can be easily stored in the patient's medical record.
    */

    public class SoapNoteService : ISoapNoteService
    {
        private readonly ILogger _logger = AppLogger.GetLogger(LoggerCategoryType.OpenAiChatCompletions);

        private readonly IOpenAiChatCompletionRepository _openAiChatCompletionRepository;

        public SoapNoteService(
            IOpenAiChatCompletionRepository openAiChatCompletionRepository)
        {
            _openAiChatCompletionRepository = openAiChatCompletionRepository;
        }

        public async Task<CommandResult<SoapNoteResponseDto>> GetSoapNoteAsync(SoapNoteRequestDto request)
        {
            //
            //  The OpenAI Text Generation and Chat Completions guide can be found at:
            //    https://platform.openai.com/docs/guides/text-generation/chat-completions-api
            //
            //  The OpenAI Chat Completions API Reference can be found at:
            //    https://platform.openai.com/docs/api-reference/chat/create
            //

            var chatCompletionRequest = new ChatCompletionRequestDto
            {
                // max_tokens : integer or null - Optional.  4096 is currently the max value.
                //
                //   The maximum number of tokens that can be generated in the chat completion.
                //   The total length of input tokens and generated tokens is limited by the model's context length.
                //   The AI will just stop responding at the token limit.
                //
                //   NOTE: Currently removing max_tokens.  GPT4o has a max token size of 180,000 tokens.  Using this
                //         value as max_tokens.
                //
                //         Providing a max_tokens could be helpful in situations where you might anticipate the AI
                //         to put itself into some kind of infinite loop.
                //         

                // model : - Required
                //
                //   ID of the model to use. See the model endpoint compatibility table for details on which models
                //   work with the Chat API.
                //
                //   PLEASE NOTE, model value is case senitive.
                //
                //     https://platform.openai.com/docs/models/model-endpoint-compatibility
                //     https://platform.openai.com/docs/models
                //
                model = "gpt-4o",

                // messages : array - Required
                //
                //   A list of messages comprising the conversation so far.  Can be system, user, assistant, or tool message.
                //
                //   content : string - Required
                //     The contents of the system, user, assistant or tool message.
                //     
                //   role : string - Required 
                //     The role of the messages author.  Can be system, user, assistant or tool.
                //
                //   name : string - Optional 
                //     An optional name for the participant.  Provides the model information to differentiate
                //     between participants of the same role.
                //
                //  
                messages = new List<ChatCompletionMessageDto>
                {
                    new ChatCompletionMessageDto
                    {
                        content = this.GetPrompt(request.TranscriptionLanguage),
                        role = ChatCompletionRole.system,
                    },
                    new ChatCompletionMessageDto
                    {
                        content = request.TranscriptionText,
                        role = ChatCompletionRole.user,
                    },
                },

                // response_format : object - Optional
                //
                //   An object specifying the format that the model must output.
                //   Setting to "json_object" enables JSON mode, which guarantees the message the model
                //   generates is valid JSON.
                //
                //   When using JSON mode, you must also instruct the model to produce JSON yourself via
                //   a system or user message.  Without this, the model may generate an unending stream
                //   of whitespace until the generation reaches the token limit.  Also note that the
                //   message content may be partially cut off if finish_reason="length", which indicates
                //   the generation exceeded max_tokens or the conversation exceeded the max context length.
                //
                //     type : string - Optional.  Defaults to text.
                //       Must be one of text or json_object
                //
                response_format = new response_format_type()
                {
                    type = "json_object",
                },

                // top_p : number or null - Optional.  Defaults to 1
                //
                //  An alternative to sampling with temperature, called nucleus sampling, where the model
                //  considers the results of the tokens with top_p probability mass.  So 0.1 means only
                //  the tokens comprising the top 10 % probability mass are considered.
                //
                //  We generally recommend altering this or temperature, but not both.
                //
                top_p = ((decimal)0.2),

                // temperature : number of null - Optional.  Defaults to 1
                //
                //   What sampling temperature to use, between 0 and 2.Higher values like 0.8 will make
                //   the output more random, while lower values like 0.2 will make it more focused and deterministic.
                //
                //   We generally recommend altering this or top_p but not both.
                //
                // CURRENTLY, USING top_p
                //
            };

            SoapNoteResponseDto soapNoteResponse = new SoapNoteResponseDto();
            TokenUsageDto tokenUsage = null;
            var stopwatch = new Stopwatch();

            try
            {
                stopwatch.Start();

                var chatCompletionsResponse = await _openAiChatCompletionRepository.GetOpenAiChatCompletionAsync(chatCompletionRequest);

                stopwatch.Stop();

                if (chatCompletionsResponse == null)
                {
                    _logger.LogError("FndParseVisitTextService.ParseVisitTextAsync: Chat completions response is null.");
                    return CommandResult<SoapNoteResponseDto>.Failure("Chat completions response is null.");
                }

                if (chatCompletionsResponse.error_response == null)
                {
                    var jsonContent = chatCompletionsResponse?.completion_response?.choices?[0]?.message?.content;
                    if (jsonContent != null)
                    {
                        // Remove the back ticks and the json prefix
                        jsonContent = jsonContent.Trim('`').Trim();
                        jsonContent = jsonContent.StartsWith("json") ? jsonContent.Substring(4).Trim() : jsonContent;
                        soapNoteResponse = JsonConvert.DeserializeObject<SoapNoteResponseDto>(jsonContent);
                        if (soapNoteResponse == null)
                        {
                            _logger.LogError("FndParseVisitTextService.ParseVisitTextAsync: Not able to deserialize jsonContent.");
                            return CommandResult<SoapNoteResponseDto>.Failure("Not able to deserialize jsonContent.");
                        }

                        var completionTokenUsage = chatCompletionsResponse?.completion_response?.usage;
                        if (completionTokenUsage != null)
                        {
                            tokenUsage = new TokenUsageDto()
                            {
                                CompletionTokens = completionTokenUsage.completion_tokens,
                                PromptTokens = completionTokenUsage.prompt_tokens,
                                TotalTokens = completionTokenUsage.total_tokens,
                            };
                        }
                    }
                    else
                    {
                        _logger.LogError("FndParseVisitTextService.ParseVisitTextAsync: Chat completions message content is null.");
                        return CommandResult<SoapNoteResponseDto>.Failure("Chat completions message content is null.");
                    }
                }
                else
                {
                    var sbError = new StringBuilder();
                    sbError.AppendLine("FndParseVisitTextService.ParseVisitTextAsync:  A non-successful status code was returned by chat completion REST endpoint.")
                           .AppendLine($"Message: {chatCompletionsResponse.error_response.error.message}")
                           .AppendLine($"Status Code: {chatCompletionsResponse.error_response.status_code}")
                           .AppendLine($"Status Code Name: {chatCompletionsResponse.error_response.status_code_name}");
                    _logger.LogError(sbError.ToString());

                    return CommandResult<SoapNoteResponseDto>.Failure(chatCompletionsResponse.error_response.error.message);
                }

                return CommandResult<SoapNoteResponseDto>.Success(soapNoteResponse);
            }
            catch (Exception ex)
            {
                var errorMessage =
                    "FndParseVisitTextService.ParseVisitTextAsync:  Exception thrown calling chat completion REST endpoint.";
                _logger.LogError(ex, errorMessage);

                errorMessage = ExceptionUtilities.AppendExceptionMessages(ex);
                return CommandResult<SoapNoteResponseDto>.Failure(errorMessage);
            }
            finally
            {
                if (stopwatch.IsRunning)
                {
                    stopwatch.Stop();
                }

                var sbLogInfo = new StringBuilder();
                sbLogInfo.AppendFormat(@"PostChatCompletionsAsync ElapsedTime:{0:h\:m\:ss\.fff}", stopwatch.Elapsed);
                if (tokenUsage != null)
                {
                    sbLogInfo.Append($" PromptTokens:{tokenUsage.PromptTokens}")
                             .Append($" CompletionTokens:{tokenUsage.CompletionTokens}")
                             .Append($" TotalTokens:{tokenUsage.TotalTokens}");
                }
                _logger.LogInformation(sbLogInfo.ToString());
            }
        }


        private string GetPrompt(TranscriptionLanguageType transcriptionLanguage)
        {
            var sbPrompt = new StringBuilder();
            sbPrompt.Append($"{this.GetIntroductionPrompt(transcriptionLanguage)}")
                    .Append($"{this.GetStructuredJsonPrompt()}")
                    .Append($"{this.GetCcHpiRosPePlanPrompt()}");

            return sbPrompt.ToString();
        }

        private string GetIntroductionPrompt(TranscriptionLanguageType transcriptionLanguage)
        {
            var sb = new StringBuilder();
            sb.Append(" You are a helpful assistant that creates Visit Notes from physician and patient encounters.")
              // .Append("  If you believe the data provided is not a provider and patient interaction then say so and do not respond to other queries.")
              .Append(" If you believe the data provided is not a provider and patient interaction then return a null SoapNote element.")
              .Append($"  You will be given the transcription of a visit that is written in {transcriptionLanguage.ToString()}.")
              .Append("  Analyze the transcription and place the interaction elements in the form of a Visit note.")
              .Append("  The concept of a medical SOAP note, where SOAP stands for Subjective, Objective, Assessment, and Plan may be used in your analysis of the visit transcript.")
              .Append("  Determine any problem, diagnosis, or assessment and their associated ICD-10 code;")
              .Append(" medications and associated representative NDC-11 code;")
              .Append(" any allergies and their SNOMED code;")
              .Append(" any orders, tests, or labs and their CPT codes; and any other relevant details.")
              .Append("  Additionally, coded concepts, including concepts that are contained in previous sections, should fall under the following sections:")
              .Append(" Diagnosis, Service Performed (E&M Code), Services Ordered (tests and orders), and Medications.")
              .Append(" Services Performed typically refers to the various medical services, procedures, or interventions that the healthcare provider has carried out during the visit.")
              .Append("  Your analysis and response should be concise and include all relevant details as well as appropriate medical terminology.")
              .Append("  Informed decision should be made based on context for the coded concepts, however,")
              .Append("  you will include your confidence level for a match as a percentage and very brief reasoning.")
              .Append("  The confidence level as a percentage should be included in the visit note as ConfidenceLevel.")
              .Append("  The brief reasoning should be included in the visit note as ConfidenceReasoning.")
              .Append("  For medication orders, also include a separate order for the discontinuation of a medication only if stated in the conversation.")
              .Append("  For follow up recommendations and referrals, you should include an order code, even if it is administrative, where applicable.");

            return sb.ToString();
        }

        private string GetStructuredJsonPrompt()
        {
            var sb = new StringBuilder();
            sb.Append("  Based on your analysis of the visit transcript you will return the Visit note in the form of structured data using JavaScript Object Notation.")
              .Append("  The structured Visit note will be returned using the following JSON template.")
              .Append($"  {this.GetFndVisitTextJson()}");

            return sb.ToString();
        }

        private string GetCcHpiRosPePlanPrompt()
        {
            var sb = new StringBuilder();
            sb.Append(" CcHpi will be a narrative that combines Chief Complaint and History of Present Illness.")
              .Append(" ReviewOfSystems will be a comma delimited list.")
              // .Append(" ReviewOfSystems will be a bullet list returned as RTF.")
              .Append(" PhysicalExam will be a comma delimited list.")
              .Append(" Plan will be be comma delimited list.");

            return sb.ToString();
        }

        private string GetFndVisitTextJson()
        {
            //var soapNoteDto = new SoapNoteResponseDto();
            //var json = JsonConvert.SerializeObject(soapNoteDto, Formatting.Indented);
            //_logger.LogInformation("JsonConvert.SerializeObject Method:");
            //_logger.LogInformation(json);
            //return json;

            var sb = new StringBuilder();
            sb.AppendLine("{");
            sb.AppendLine("  \"SoapNote\": {");
            sb.AppendLine("    \"CcHpi\": \"\",");
            sb.AppendLine("    \"ReviewOfSystems\": \"\",");
            sb.AppendLine("    \"PhysicalExam\": \"\",");
            sb.AppendLine("    \"Vitals\": {");
            sb.AppendLine("      \"Weight\": \"\",");
            sb.AppendLine("      \"Height\": \"\",");
            sb.AppendLine("      \"Temperature\": \"\",");
            sb.AppendLine("      \"RespiratoryRate\": \"\",");
            sb.AppendLine("      \"HeartRate\": \"\",");
            sb.AppendLine("      \"BloodPressure\": \"\"");
            sb.AppendLine("    },");
            sb.AppendLine("    \"Allergies\": {");
            sb.AppendLine("      \"DrugAllergies\": [");
            sb.AppendLine("        {");
            sb.AppendLine("          \"Allergen\": \"\",");
            sb.AppendLine("          \"Reactions\": [");
            sb.AppendLine("            {");
            sb.AppendLine("              \"Reaction\": \"\"");
            sb.AppendLine("            }");
            sb.AppendLine("          ],");
            sb.AppendLine("          \"Notes\": \"\"");
            sb.AppendLine("        }");
            sb.AppendLine("      ],");
            sb.AppendLine("      \"EnvironmentalAllergies\": [");
            sb.AppendLine("        {");
            sb.AppendLine("          \"Allergen\": \"\",");
            sb.AppendLine("          \"Reactions\": [");
            sb.AppendLine("            {");
            sb.AppendLine("              \"Reaction\": \"\"");
            sb.AppendLine("            }");
            sb.AppendLine("          ],");
            sb.AppendLine("          \"Notes\": \"\"");
            sb.AppendLine("        }");
            sb.AppendLine("      ],");
            sb.AppendLine("      \"FoodAllergies\": [");
            sb.AppendLine("        {");
            sb.AppendLine("          \"Allergen\": \"\",");
            sb.AppendLine("          \"Reactions\": [");
            sb.AppendLine("            {");
            sb.AppendLine("              \"Reaction\": \"\"");
            sb.AppendLine("            }");
            sb.AppendLine("          ],");
            sb.AppendLine("          \"Notes\": \"\"");
            sb.AppendLine("        }");
            sb.AppendLine("      ]");
            sb.AppendLine("    },");
            sb.AppendLine("    \"Diagnoses\": [");
            sb.AppendLine("      {");
            sb.AppendLine("        \"Code\": \"\",");
            sb.AppendLine("        \"TypeOfCode\": \"\",");
            sb.AppendLine("        \"Description\": \"\",");
            sb.AppendLine("        \"ConfidenceLevel\": \"\",");
            sb.AppendLine("        \"ConfidenceReasoning\": \"\"");
            sb.AppendLine("      }");
            sb.AppendLine("    ],");
            sb.AppendLine("    \"ServicesPerformed\": [");
            sb.AppendLine("      {");
            sb.AppendLine("        \"Code\": \"\",");
            sb.AppendLine("        \"TypeOfCode\": \"\",");
            sb.AppendLine("        \"Description\": \"\",");
            sb.AppendLine("        \"ConfidenceLevel\": \"\",");
            sb.AppendLine("        \"ConfidenceReasoning\": \"\"");
            sb.AppendLine("      }");
            sb.AppendLine("    ],");
            sb.AppendLine("    \"ServicesOrdered\": [");
            sb.AppendLine("      {");
            sb.AppendLine("        \"Code\": \"\",");
            sb.AppendLine("        \"TypeOfCode\": \"\",");
            sb.AppendLine("        \"Description\": \"\",");
            sb.AppendLine("        \"ConfidenceLevel\": \"\",");
            sb.AppendLine("        \"ConfidenceReasoning\": \"\"");
            sb.AppendLine("      }");
            sb.AppendLine("    ],");
            sb.AppendLine("    \"Medications\": {");
            sb.AppendLine("      \"HistoryMedications\": [");
            sb.AppendLine("        {");
            sb.AppendLine("          \"DrugName\": \"\",");
            sb.AppendLine("          \"PrescribedDate\": \"\"");
            sb.AppendLine("        }");
            sb.AppendLine("      ],");
            sb.AppendLine("      \"ActiveMedications\": [");
            sb.AppendLine("        {");
            sb.AppendLine("          \"DrugName\": \"\",");
            sb.AppendLine("          \"PrescribedDate\": \"\",");
            sb.AppendLine("          \"Instructions\": \"\",");
            sb.AppendLine("          \"Pharmacy\": \"\",");
            sb.AppendLine("          \"Prescriber\": \"\"");
            sb.AppendLine("        }");
            sb.AppendLine("      ],");
            sb.AppendLine("      \"ProposedMedications\": [");
            sb.AppendLine("        {");
            sb.AppendLine("          \"DrugName\": \"\",");
            sb.AppendLine("          \"DrugForm\": \"\",");
            sb.AppendLine("          \"Instructions\": \"\",");
            sb.AppendLine("          \"Pharmacy\": \"\"");
            sb.AppendLine("        }");
            sb.AppendLine("      ]");
            sb.AppendLine("    },");
            sb.AppendLine("    \"Plan\": \"\"");
            sb.AppendLine("  }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        //
        //Question:
        //=========
        //is there a documented schema for a medical SOAP note ?  
        //A SOAP note is a structured format used by healthcare providers to document a patient's medical information. SOAP stands for Subjective, Objective, Assessment, and Plan.
        //
        //
        //Answer:
        //=======
        //Yes, there is a documented schema for a medical SOAP note. A SOAP note is structured into four main sections: Subjective, Objective, Assessment, and Plan. Each section captures specific information about the patient's medical encounter. Here’s a detailed outline of the schema for a SOAP note:
        //
        //### SOAP Note Schema
        //
        //#### 1. Subjective (S)
        //This section contains the patient's reported symptoms and experiences, which are not measurable or observable by the healthcare provider. It often includes:
        //
        //- **Chief Complaint (CC):** The primary reason for the patient's visit, usually stated in the patient's own words.
        //- **History of Present Illness (HPI):** A detailed description of the patient's current symptoms, including onset, location, duration, character, aggravating and alleviating factors, radiation, and timing.
        //- **Review of Systems (ROS):** A systematic review of the patient's various body systems to identify any additional symptoms the patient may not have mentioned.
        //- **Past Medical History (PMH):** Information about the patient's past illnesses, surgeries, hospitalizations, and any chronic conditions.
        //- **Family History (FH):** Information about illnesses and conditions that run in the patient's family.
        //- **Social History (SH):** Information about the patient's lifestyle, occupation, habits (such as smoking or alcohol use), and living conditions.
        //- **Medications:** A list of the patient's current medications, including dosages and frequencies.
        //- **Allergies:** A list of any known allergies, particularly to medications or foods.
        //
        //#### 2. Objective (O)
        //This section contains measurable, observable data gathered by the healthcare provider during the patient encounter. It includes:
        //
        //- **Vital Signs:** Measurements such as temperature, blood pressure, heart rate, respiratory rate, and oxygen saturation.
        //- **Physical Examination Findings:** Detailed findings from the physical examination of the patient, often organized by body system (e.g., cardiovascular, respiratory, gastrointestinal, neurological).
        //- **Laboratory Results:** Relevant results from blood tests, urinalysis, and other laboratory studies.
        //- **Imaging Results:** Findings from imaging studies such as X-rays, CT scans, MRIs, and ultrasounds.
        //- **Other Diagnostic Data:** Any additional diagnostic tests or procedures performed.
        //
        //#### 3. Assessment (A)
        //This section contains the healthcare provider's clinical judgment and diagnosis based on the subjective and objective information. It includes:
        //
        //- **Differential Diagnosis:** A list of possible diagnoses that could explain the patient's symptoms.
        //- **Primary Diagnosis:** The most likely diagnosis based on the current evidence.
        //- **Problem List:** A list of all identified medical issues or concerns.
        //
        //#### 4. Plan (P)
        //This section outlines the healthcare provider's proposed approach to managing the patient's condition. It includes:
        //
        //- **Diagnostic Plan:** Additional tests or procedures needed to confirm the diagnosis.
        //- **Therapeutic Plan:** Treatment recommendations such as medications, therapies, surgeries, or lifestyle modifications.
        //- **Patient Education:** Information provided to the patient about their condition and instructions for managing it.
        //- **Follow-Up:** Recommendations for future appointments, monitoring, and any necessary referrals to specialists.
        //
        //### Example of a SOAP Note
        //
        //**Subjective:**
        //- CC: "I have a severe headache and nausea."
        //- HPI: The patient reports a severe headache that started three days ago, located in the frontal region, constant, and throbbing. The headache is associated with nausea and photophobia. No recent trauma or changes in medication.
        //- ROS: No fever, chills, or visual disturbances. No recent weight loss or gain.
        //- PMH: History of migraines, hypertension.
        //- FH: Mother has a history of migraines.
        //- SH: Non-smoker, occasional alcohol use.
        //- Medications: Metoprolol 50mg daily, Sumatriptan as needed for migraines.
        //- Allergies: Penicillin (rash).
        //
        //**Objective:**
        //- Vital Signs: BP 140/90 mmHg, HR 78 bpm, Temp 98.6°F, RR 16, SpO2 98% on room air.
        //- Physical Exam:
        //  - General: Appears in mild distress due to pain.
        //  - HEENT: No signs of infection, pupils equal and reactive.
        //  - Neurological: Alert and oriented, no focal deficits.
        //- Lab Results: CBC normal, electrolytes within normal limits.
        //- Imaging: CT scan of the head negative for acute changes.
        //
        //**Assessment:**
        //- Primary Diagnosis: Migraine headache.
        //- Differential Diagnosis: Tension headache, cluster headache, sinusitis.
        //
        //**Plan:**
        //- Diagnostic Plan: No further imaging required at this time.
        //- Therapeutic Plan: Prescribe sumatriptan 50mg as needed for headache, continue metoprolol.
        //- Patient Education: Discussed migraine triggers and the importance of maintaining a headache diary.
        //- Follow-Up: Follow up in 2 weeks or sooner if symptoms worsen.
        //
        //This schema helps ensure that all relevant information is systematically documented and facilitates clear communication among healthcare providers.
        //
        //
        //
        //
        //
        //Question:
        //=========
        //is there a documented schema for a medical SOAP note ?  
        //A SOAP note is a structured format used by healthcare providers to document a patient's medical information. SOAP stands for Subjective, Objective, Assessment, and Plan.  
        //Where can the documented standard for the SOAP schema be found ?
        //
        //
        //Answer:
        //=======
        //The SOAP note format is widely used in medical documentation, but there is no single, universally accepted "documented standard" for SOAP notes like there is for some other types of medical records (e.g., ICD coding standards). However, the structure and components of SOAP notes are well-documented and taught across medical schools and institutions, and various professional organizations and educational resources provide guidelines on how to write them.
        //
        //For official guidelines and examples, you can refer to the following sources:
        //
        //1. **Medical Textbooks and Academic Resources**: Many medical textbooks on clinical practice, medical documentation, and patient care include detailed explanations and examples of SOAP notes. Some widely recognized textbooks include:
        //   - "Bates' Guide to Physical Examination and History Taking" by Lynn Bickley
        //   - "The Medical Interview: Mastering Skills for Clinical Practice" by John L. Coulehan and Marian R. Block
        //
        //2. **Professional Organizations**:
        //   - **American Academy of Family Physicians (AAFP)**: Provides resources and guidelines for clinical documentation, including SOAP notes.
        //   - **American Medical Association (AMA)**: Offers guidelines on medical documentation and coding that often reference the SOAP format.
        //
        //3. **Educational Institutions**: Many medical schools and nursing programs provide online resources and guides for SOAP notes. Some examples include:
        //   - University of California, San Francisco (UCSF) School of Medicine
        //   - University of Washington School of Medicine
        //
        //4. **Online Medical Education Platforms**: Websites like Medscape, UpToDate, and others often have articles and tutorials on how to write SOAP notes.
        //
        //5. **Electronic Health Record (EHR) Systems**: Vendors of EHR systems often provide templates and standards for SOAP notes as part of their software documentation. Examples of popular EHR systems include Epic, Cerner, and Allscripts.
        //
        //### Specific References
        //
        //- **University of California, San Francisco (UCSF) School of Medicine**: They offer detailed guidelines and examples on their websites or through their educational programs.
        //- **Family Medicine Textbooks**: Such as "Essentials of Family Medicine" by Philip D. Sloane, which often includes sections on clinical documentation and SOAP notes.
        //- **Medical Journals and Articles**: Some medical journals publish articles on best practices for clinical documentation, including SOAP notes.
        //
        //### Example Online Resources
        //
        //- [American Academy of Family Physicians (AAFP)](https://www.aafp.org/)
        //- [American Medical Association (AMA)](https://www.ama-assn.org/)
        //- [University of California, San Francisco (UCSF) School of Medicine SOAP Note Guidelines](https://meded.ucsf.edu/)
        //- [Medscape SOAP Note Writing Guide](https://www.medscape.com/)
        //
        //By consulting these sources, you can find detailed descriptions and examples of the SOAP note format tailored to various medical contexts.
        //

        private string PocSystemPrompt()
        {
            var prompt = "You are a helpful assistant that creates Visit Notes from physician and patient encounters. If you believe the data provided is not a provider and patient interaction then say so and do not respond to other queries. You will be given the transcription of a visit. Analyze the transcription and place the interaction elements in the form of a Visit note. Determine any problem, diagnosis, or assessment and their associated ICD-10 code; medications and associated representative NDC-11 code; any allergies and their SNOMED code; any orders, tests, or labs and their CPT codes; and any other relevant details. Notes and records should fall under the headings (in bold) of: Chief Complaint/History of Present Illness, Review of Symptoms, Physical Exam, and Plan.  Additionally, coded concepts, including concepts that are contained in previous sections, should fall under the following sections: Diagnosis, Service Performed (E&M Code), Services Ordered (tests and orders), and Medications. Your analysis and response should be concise and include all relevant details as well as appropriate medical terminology. Each note or record underneath the section headings should be a separate line item and not a narrative with the exception of the Chief complaint. Codes should follow their concepts and be placed in brackets. Informed decision should be made based on context for the coded concepts, however, you should include your confidence level for a match as a percentage and very brief reasoning in an <i> tag following the code in parentheses. For medication orders, also include a separate order for the discontinuation of a medication only if stated in the conversation. For follow up recommendations and referrals, you should include an order code, even if it is administrative, where applicable. Each note, record, or finding should appear in its own <p> tag so that it displays on its own line and not be formatted in lists or bulleted. The full response should be within a html div tag.";
            return prompt;
        }
    }
}
