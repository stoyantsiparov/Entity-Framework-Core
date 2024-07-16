namespace Cadastre.DataProcessor.ImportDtos
{
    using Cadastre.Common;
    using Cadastre.Data.Enumerations;
    using Cadastre.Data.Models;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    [JsonObject(nameof(Citizen))]
    public class ImportCitizenDto
    {
        [Required]
        [MinLength(ValidationConstants.CitizenNameMinLength)]
        [MaxLength(ValidationConstants.CitizenNameMaxLength)]
        [JsonProperty(nameof(FirstName))]
        public string FirstName { get; set; } = null!;

        [Required]
        [MinLength(ValidationConstants.CitizenNameMinLength)]
        [MaxLength(ValidationConstants.CitizenNameMaxLength)]
        [JsonProperty(nameof(LastName))]
        public string LastName { get; set; } = null!;

        [Required]
        [JsonProperty(nameof(BirthDate))]
        public string BirthDate { get; set; } = null!;

        [Required]
        [EnumDataType(typeof(MaritalStatus))]
        [JsonProperty(nameof(MaritalStatus))]
        public string MaritalStatus { get; set; } = null!;

        [Required]
        [JsonProperty(nameof(Properties))]
        public int[] Properties { get; set; } = null!;
    }
}
