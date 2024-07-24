namespace Medicines.DataProcessor.ImportDtos
{
    using Medicines.Common;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    [XmlType("Pharmacy")]
    public class ImportPharmacyDto
    {
        [Required]
        [XmlElement("Name")]
        [MaxLength(ValidationConstants.PharmacyNameMaxLength)]
        [MinLength(ValidationConstants.PharmacyNameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [XmlElement("PhoneNumber")]
        [RegularExpression(ValidationConstants.PharmacyPhoneNumberRegex)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [XmlAttribute("non-stop")]
        [RegularExpression(ValidationConstants.PharmacyBooleanRegex)]
        public string IsNonStop { get; set; } = null!;

        [XmlArray("Medicines")]
        [XmlArrayItem("Medicine")]
        public ImportMedicineDto[] Medicines { get; set; }
    }
}
