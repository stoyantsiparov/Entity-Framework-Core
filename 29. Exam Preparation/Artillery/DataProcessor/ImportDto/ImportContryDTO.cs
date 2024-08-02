using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Artillery.Data.Models;

namespace Artillery.DataProcessor.ImportDto;

[XmlType(nameof(Country))]
public class ImportContryDTO
{
    [XmlElement(nameof(CountryName))]
    [Required]
    [StringLength(60, MinimumLength = 4)]
    public string CountryName { get; set; } = null!;

    [XmlElement(nameof(ArmySize))]
    [Required]
    [Range(50000, 10000000)]
    public int ArmySize { get; set; }
}