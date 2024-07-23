using Invoices.Data.Models.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Invoices.DataProcessor.ImportDto;

public class ImportInvoiceDto
{
    [Required]
    [Range(1000000000, 1500000000)]
    public int Number { get; set; }

    //DateTime -> Deserialize as a string 
    [Required]
    public string IssueDate { get; set; } = null!;

    [Required]
    public string DueDate { get; set; } = null!;

    [Required]
    public decimal Amount { get; set; }

    //Enum -> Deserialize as a int
    [Required]
    [Range((int)Data.Models.Enums.CurrencyType.BGN, (int)Data.Models.Enums.CurrencyType.USD)]
    public int CurrencyType { get; set; }

    [Required]
    public int ClientId { get; set; }
}