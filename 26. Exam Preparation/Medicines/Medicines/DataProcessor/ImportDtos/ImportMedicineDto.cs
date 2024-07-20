namespace Medicines.DataProcessor.ImportDtos
{
    using Medicines.Common;
    using Medicines.Data.Models;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    [XmlType(nameof(Medicine))]
    public class ImportMedicineDto
    {
        [XmlAttribute("category")]
        [Range(ValidationConstants.MedicineCategoryMinValue, ValidationConstants.MedicineCategoryMaxValue)]
        public int Category { get; set; }

        [XmlElement("Name")]
        [Required]
        [MinLength(ValidationConstants.MedicineNameMinLength)]
        [MaxLength(ValidationConstants.MedicineNameMaxLength)]
        public string Name { get; set; } = null!;

        [XmlElement("Price")]
        [Range(ValidationConstants.MedicinePriceMinValue, ValidationConstants.MedicinePriceMaxValue)]
        public double Price { get; set; }

        [XmlElement("ProductionDate")]
        [Required]
        public string ProductionDate { get; set; } = null!;

        [XmlElement("ExpiryDate")]
        [Required]
        public string ExpiryDate { get; set; } = null!;

        [XmlElement("Producer")]
        [Required]
        [MinLength(ValidationConstants.MedicineProducerNameMinLength)]
        [MaxLength(ValidationConstants.MedicineProducerNameMaxLength)]
        public string Producer { get; set; } = null!;
    }
}
