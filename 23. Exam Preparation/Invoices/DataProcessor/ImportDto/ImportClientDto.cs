using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Invoices.Data.Models;

namespace Invoices.DataProcessor.ImportDto;

[XmlType(nameof(Client))]
public class ImportClientDto
{
    [XmlElement(nameof(Name))]
    [Required]
    [StringLength(25, MinimumLength = 10)]
    public string Name { get; set; } = null!;

    [XmlElement(nameof(NumberVat))]
    [Required]
    [StringLength(15, MinimumLength = 10)]
    public string NumberVat { get; set; } = null!;

    [XmlArray(nameof(Addresses))]
    public ImportAddressDto[] Addresses { get; set; } = null!;
}