using System.Xml.Serialization;
using CarDealer.Models;

namespace CarDealer.DTOs.Import;

[XmlType("Car")]
public class CarImportDTO
{
    [XmlElement("make")]
    public string Make { get; set; }

    [XmlElement("model")]
    public string Model { get; set; }

    [XmlElement("traveledDistance")]
    public long TraveledDistance { get; set; }

    [XmlArray("parts")]
    public PartCarsImportDTO[] PartIds { get; set; }
}

[XmlType("partId")]
public class PartCarsImportDTO
{
    [XmlAttribute("id")]
    public int Id { get; set; }
}