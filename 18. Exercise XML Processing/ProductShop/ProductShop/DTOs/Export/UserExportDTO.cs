using System.Xml.Serialization;

namespace ProductShop.DTOs.Export;

[XmlType("User")]
public class UserExportDTO
{
    [XmlElement("firstName")]
    public string FirstName { get; set; }

    [XmlElement("lastName")]
    public string LastName { get; set; }

    [XmlArray("soldProducts")]
    [XmlArrayItem("Product")]
    public SoldProductsExportDTO[] SoldProducts { get; set; }
}