using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Footballers.DataProcessor.ImportDto;

public class ImportTeamDto
{
    [RegularExpression(@"^[a-zA-Z0-9 .-]*$")]
    [StringLength(40, MinimumLength = 3)]
    [Required]
    public string Name { get; set; } = null!;

    [StringLength(40, MinimumLength = 2)]
    [Required]
    public string Nationality { get; set; } = null!;

    [Required]
    public int Trophies { get; set; }

    [JsonProperty(nameof(Footballers))]
    [Required]
    public int[] FootballersIds { get; set; } = null!;
}