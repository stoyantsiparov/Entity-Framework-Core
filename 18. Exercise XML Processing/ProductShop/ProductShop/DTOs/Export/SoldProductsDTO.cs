using System.Xml.Serialization;

namespace ProductShop.DTOs.Export;

[XmlType("SoldProducts")]
public class SoldProductsDTO
{
    [XmlElement("count")]
    public int Count { get; set; }

    [XmlArray("products")]
    public ProductExportDTO[] Products { get; set; }
}