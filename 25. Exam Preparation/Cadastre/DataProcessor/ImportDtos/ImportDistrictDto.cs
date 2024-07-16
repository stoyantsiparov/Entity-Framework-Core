namespace Cadastre.DataProcessor.ImportDtos
{
    using Cadastre.Common;
    using Cadastre.Data.Enumerations;
    using Cadastre.Data.Models;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType(nameof(District))]
    public class ImportDistrictDto
    {
        [Required]
        [XmlElement(nameof(Name))]
        [MinLength(ValidationConstants.DistrictNameMinLength)]
        [MaxLength(ValidationConstants.DistrictNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [XmlElement(nameof(PostalCode))]
        [RegularExpression(ValidationConstants.DistrictPostalCodeRegex)]
        public string PostalCode { get; set; } = null!;

        [Required]
        [XmlAttribute(nameof(Region))]
        public string Region { get; set; } = null!;

        [XmlArray(nameof(Properties))]
        [XmlArrayItem(nameof(Property))]
        public ImportPropertyDto[] Properties { get; set; } = null!;
    }
}
