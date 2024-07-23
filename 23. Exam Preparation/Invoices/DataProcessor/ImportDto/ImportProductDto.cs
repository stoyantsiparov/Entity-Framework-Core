using Invoices.Data.Models.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Invoices.DataProcessor.ImportDto;

public class ImportProductDto
{
    [Required]
    [StringLength(30, MinimumLength = 9)]
    public string Name { get; set; } = null!;

    [Required]
    [Range(typeof(decimal), "5.00", "1000.00")]
    public decimal Price { get; set; }

    [Required]
    [Range((int)Data.Models.Enums.CategoryType.ADR, (int)Data.Models.Enums.CategoryType.Tyres)]
    public int CategoryType { get; set; }

    [Required]
    public int[] Clients { get; set; } = null!;
}