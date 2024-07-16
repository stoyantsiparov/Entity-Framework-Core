namespace Cadastre.DataProcessor.ImportDtos
{
    using Cadastre.Common;
    using Cadastre.Data.Models;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType(nameof(Property))]
    public class ImportPropertyDto
    {
        [Required]
        [MinLength(ValidationConstants.PropertyIdentifierMinLength)]
        [MaxLength(ValidationConstants.PropertyIdentifierMaxLength)]
        [XmlElement(nameof(PropertyIdentifier))]
        public string PropertyIdentifier { get; set; } = null!;

        [Required]
        [Range(0, int.MaxValue)]
        [XmlElement(nameof(Area))]
        public int Area { get; set; }

        [MinLength(ValidationConstants.PropertyDetailsMinLength)]
        [MaxLength(ValidationConstants.PropertyDetailsMaxLength)]
        [XmlElement(nameof(Details))]
        public string? Details { get; set; }

        [Required]
        [MinLength(ValidationConstants.PropertyAddressMinLength)]
        [MaxLength(ValidationConstants.PropertyAddressMaxLength)]
        [XmlElement(nameof(Address))]
        public string Address { get; set; } = null!;

        [Required]
        [XmlElement(nameof(DateOfAcquisition))]
        public string DateOfAcquisition { get; set; } = null!;
    }
}
