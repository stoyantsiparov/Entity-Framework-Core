using System.Xml.Serialization;
using Footballers.Data.Models;

namespace Footballers.DataProcessor.ExportDto;

[XmlType(nameof(Coach))]
public class ExportCoachDto
{
    [XmlElement(nameof(CoachName))]
    public string CoachName { get; set; } = null!;

    [XmlAttribute(nameof(FootballersCount))]
    public int FootballersCount { get; set; }

    [XmlArray(nameof(Footballers))]
    public ExportFootballerDto[] Footballers { get; set; } = null!;
}