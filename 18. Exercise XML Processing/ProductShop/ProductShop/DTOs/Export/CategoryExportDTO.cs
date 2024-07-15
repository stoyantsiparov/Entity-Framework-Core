using System.Xml.Serialization;

namespace ProductShop.DTOs.Export;

[XmlType("Category")]
public class CategoryExportDTO
{
    [XmlElement("name")]
    public string Name { get; set; }

    [XmlElement("count")]
    public int ProductCount { get; set; }

    [XmlElement("averagePrice")]
    public decimal AveragePrice { get; set; }

    [XmlElement("totalRevenue")]
    public decimal TotalRevenue { get; set; }
}