using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Trucks.DataProcessor.ImportDto;

public class ImportClientDto
{
    [Required]
    [StringLength(40, MinimumLength = 3)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(40, MinimumLength = 2)]
    public string Nationality { get; set; } = null!;

    [Required]
    public string Type { get; set; } = null!;

    [JsonProperty(nameof(Trucks))]
    [Required]
    public int[] TrucksIds { get; set; } = null!;
}