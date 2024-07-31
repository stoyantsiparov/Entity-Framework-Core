using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Footballers.Data.Models;

namespace Footballers.DataProcessor.ImportDto;

[XmlType(nameof(Coach))]
public class ImportCoachDto
{
    [XmlElement(nameof(Name))]
    [StringLength(40, MinimumLength = 2)]
    [Required]
    public string Name { get; set; } = null!;

    [XmlElement(nameof(Nationality))]
    [Required]
    public string Nationality { get; set; } = null!;
    
    [XmlArray(nameof(Footballers))]
    public ImportFootballerDto[] Footballers { get; set; } = null!;
}