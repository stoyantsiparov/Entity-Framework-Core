using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using TravelAgency.Data.Models;

namespace TravelAgency.DataProcessor.ImportDtos;

[XmlType(nameof(Customer))]
public class ImportCustomerDTO
{
    [XmlElement(nameof(FullName))]
    [Required]
    [StringLength(60, MinimumLength = 4)]
    public string FullName { get; set; } = null!;

    [XmlElement(nameof(Email))]
    [Required]
    [StringLength(50, MinimumLength = 6)]
    public string Email { get; set; } = null!;

    [XmlAttribute("phoneNumber")]
    [Required]
    [RegularExpression(@"^\+\d{12}$")]
    public string PhoneNumber { get; set; } = null!;
}