using System.Xml.Serialization;
using Artillery.Data.Models;

namespace Artillery.DataProcessor.ExportDto;

[XmlType(nameof(Country))]
public class ExportCountryDTO
{
    [XmlAttribute(nameof(Country))]
    public string Country { get; set; } = null!;

    [XmlAttribute(nameof(ArmySize))]
    public int ArmySize { get; set; }
}