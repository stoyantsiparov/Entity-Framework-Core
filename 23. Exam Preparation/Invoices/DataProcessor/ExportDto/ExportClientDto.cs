using Invoices.DataProcessor.ImportDto;
using System.Xml.Serialization;

namespace Invoices.DataProcessor.ExportDto;

[XmlType("Client")]
public class ExportClientDto
{
    [XmlElement("ClientName")]
    public string ClientName { get; set; } = null!;

    [XmlElement("VatNumber")]
    public string VatNumber { get; set; } = null!;

    [XmlAttribute("InvoicesCount")]
    public int InvoicesCount { get; set; }

    [XmlArray("Invoices")]
    public ExportInvoiceDto[] Invoices { get; set; } = null!;
}