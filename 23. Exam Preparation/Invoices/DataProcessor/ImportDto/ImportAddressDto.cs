using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Invoices.Data.Models;

namespace Invoices.DataProcessor.ImportDto;

[XmlType(nameof(Address))]
public class ImportAddressDto
{

    [XmlElement(nameof(StreetName))]
    [Required]
    [StringLength(20, MinimumLength = 10)]
    public string StreetName { get; set; } = null!;

    [XmlElement(nameof(StreetNumber))]
    [Required]
    public int StreetNumber { get; set; }

    [XmlElement(nameof(PostCode))] 
    [Required] 
    public string PostCode { get; set; } = null!;

    [XmlElement(nameof(City))]
    [Required]
    [StringLength(15, MinimumLength = 5)]
    public string City { get; set; } = null!;

    [XmlElement(nameof(Country))]
    [Required]
    [StringLength(15, MinimumLength = 5)]
    public string Country { get; set; } = null!;
}