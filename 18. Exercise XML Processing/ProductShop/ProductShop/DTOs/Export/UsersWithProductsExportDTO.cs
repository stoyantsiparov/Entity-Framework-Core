using System.Xml.Serialization;

namespace ProductShop.DTOs.Export;

public class UsersWithProductsExportDTO
{
    [XmlElement("count")]
    public int Count { get; set; }

    [XmlArray("users")]
    public UserWithProductsExportDTO[] Users { get; set; }
}