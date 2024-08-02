using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Artillery.Data.Models;

namespace Artillery.DataProcessor.ImportDto;

[XmlType(nameof(Manufacturer))]
public class ImportManufacturerDTO
{
    [XmlElement(nameof(ManufacturerName))]
    [Required]
    [StringLength(40, MinimumLength = 4)]
    public string ManufacturerName { get; set; } = null!;

    [XmlElement(nameof(Founded))]
    [Required]
    [StringLength(100, MinimumLength = 10)]
    public string Founded { get; set; } = null!;
}