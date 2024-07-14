using System.Xml.Serialization;

namespace CarDealer.DTOs.Export;

[XmlType("part")]
public class PartsExportDTO
{
    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlAttribute("price")]
    public decimal Price { get; set; }
}