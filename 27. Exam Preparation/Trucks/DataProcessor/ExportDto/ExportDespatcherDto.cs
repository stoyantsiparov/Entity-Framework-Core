using System.Xml.Serialization;
using Trucks.Data.Models;

namespace Trucks.DataProcessor.ExportDto;

[XmlType(nameof(Despatcher))]
public class ExportDespatcherDto
{
    [XmlElement(nameof(DespatcherName))]
    public string DespatcherName { get; set; } = null!;

    [XmlAttribute(nameof(TrucksCount))]
    public int TrucksCount { get; set; }

    [XmlArray(nameof(Trucks))]
    public ExportTruckDto[] Trucks { get; set; } = null!;
}