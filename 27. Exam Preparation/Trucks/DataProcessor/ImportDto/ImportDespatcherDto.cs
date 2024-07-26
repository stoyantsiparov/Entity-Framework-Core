using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Trucks.Data.Models;

namespace Trucks.DataProcessor.ImportDto;

[XmlType(nameof(Despatcher))]
public class ImportDespatcherDto
{
    [XmlElement(nameof(Name))]
    [Required]
    [StringLength(40, MinimumLength = 2)]
    public string Name { get; set; } = null!;

    [XmlElement(nameof(Position))]
    public string Position { get; set; } = null!;

    [XmlArray(nameof(Trucks))] 
    public ImportTruckDto[] Trucks { get; set; } = null!;
}