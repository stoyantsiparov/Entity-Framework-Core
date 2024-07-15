using System.Xml.Serialization;

namespace CarDealer.DTOs.Export;

[XmlType("sale")]
public class SaleWithDiscountExportDTO
{
    [XmlElement("car")]
    public CarDto Car { get; set; }
    [XmlElement("discount")]
    public int Discount { get; set; }
    [XmlElement("customer-name")]
    public string CustomerName { get; set; }
    [XmlElement("price")]
    public decimal Price { get; set; }
    [XmlElement("price-with-discount")]
    public decimal PriceWithDiscount { get; set; }
}

[XmlType("car")]
public class CarDto
{
    [XmlAttribute("make")]
    public string Make { get; set; }
    [XmlAttribute("model")]
    public string Model { get; set; }
    [XmlAttribute("traveled-distance")]
    public long TraveledDistance { get; set; }
}