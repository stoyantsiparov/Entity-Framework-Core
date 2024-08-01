using System.Xml.Serialization;
using Footballers.Data.Models;

namespace Footballers.DataProcessor.ExportDto;

[XmlType(nameof(Footballer))]
public class ExportFootballerDto
{
    [XmlElement(nameof(Name))]
    public string Name { get; set; } = null!;

    [XmlElement(nameof(Position))]
    public string Position { get; set; } = null!;
}