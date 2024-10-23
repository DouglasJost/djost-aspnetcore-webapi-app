using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAiChatCompletions.Models.MedicalVisitNote
{
    // Represents the SOAP note details
    public class SoapNoteDto
    {
        public string CcHpi { get; set; }
        public string ReviewOfSystems { get; set; }
        public string PhysicalExam { get; set; }
        public VitalSignsDto Vitals { get; set; }
        public AllergiesDto Allergies { get; set; }
        public List<DiagnosisDto> Diagnoses { get; set; }
        public List<ServicePerformedDto> ServicesPerformed { get; set; }
        public List<ServiceOrderedDto> ServicesOrdered { get; set; }
        public MedicationsDto Medications { get; set; }
        public string Plan { get; set; }
    }

    // Represents the vitals section of the SOAP note
    public class VitalSignsDto
    {
        public string Weight { get; set; }
        public string Height { get; set; }
        public string Temperature { get; set; }
        public string RespiratoryRate { get; set; }
        public string HeartRate { get; set; }
        public string BloodPressure { get; set; }
    }

    // Represents the allergies section
    public class AllergiesDto
    {
        public List<AllergyDto> DrugAllergies { get; set; }
        public List<AllergyDto> EnvironmentalAllergies { get; set; }
        public List<AllergyDto> FoodAllergies { get; set; }
    }

    // Represents an allergy
    public class AllergyDto
    {
        public string Allergen { get; set; }
        public List<AllergyReactionDto> Reactions { get; set; }
        public string Notes { get; set; }
    }

    // Represents a reaction to an allergy
    public class AllergyReactionDto
    {
        public string Reaction { get; set; }
    }

    // Represents a diagnosis
    public class DiagnosisDto
    {
        public string Code { get; set; }  
        public string TypeOfCode { get; set; }
        public string Description { get; set; }
        public string ConfidenceLevel { get; set; }
        public string ConfidenceReasoning { get; set; }
    }

    // Represents a service preformed
    public class ServicePerformedDto
    {
        public string Code { get; set; }
        public string TypeOfCode { get; set; }
        public string Description { get; set; }
        public string ConfidenceLevel { get; set; }
        public string ConfidenceReasoning { get; set; }
    }

    // Represents a service ordered
    public class ServiceOrderedDto
    {
        public string Code { get; set; }
        public string TypeOfCode { get; set; }
        public string Description { get; set; }
        public string ConfidenceLevel { get; set; }
        public string ConfidenceReasoning { get; set; }
    }

    // Represents medications
    public class MedicationsDto
    {
        public List<HistoryMedicationDto> HistoryMedications { get; set; }
        public List<ActiveMedicationDto> ActiveMedications { get; set; }
        public List<ProposedMedicationDto> ProposedMedications { get; set; }
    }

    // Represents a medication history
    public class HistoryMedicationDto
    {
        public string DrugName { get; set; }
        public string PrescribedDate { get; set; }
    }

    // Represents an active medication
    public class ActiveMedicationDto
    {
        public string DrugName { get; set; }
        public string PrescribedDate { get; set; }
        public string Instructions { get; set; }
        public string Pharmacy { get; set; }
        public string Prescriber { get; set; }
    }

    // Represents a proposed medication
    public class ProposedMedicationDto
    {
        public string DrugName { get; set; }
        public string DrugForm { get; set; }
        public string Instructions { get; set; }
        public string Pharmacy { get; set; }
    }
}
