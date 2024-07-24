namespace Medicines.DataProcessor.ImportDtos
{
    using System.ComponentModel.DataAnnotations;
    using Medicines.Common;
    using Newtonsoft.Json;

    public class ImportPatientDto
    {
        [JsonProperty("FullName")]
        [MinLength(ValidationConstants.PatientFullNameMinLength)]
        [MaxLength(ValidationConstants.PatientFullNameMaxLength)]
        [Required]
        public string FullName { get; set; } = null!;

        [JsonProperty("AgeGroup")]
        [Required]
        [Range(ValidationConstants.PatientAgeGroupMinValue, ValidationConstants.PatientAgeGroupMaxValue)]
        public int AgeGroup { get; set; }

        [JsonProperty("Gender")]
        [Required]
        [Range(ValidationConstants.PatientGenderMinValue, ValidationConstants.PatientGenderMaxValue)]
        public int Gender { get; set; }

        [JsonProperty("Medicines")]
        [Required]  
        public int[] Medicines { get; set; }
    }
}
