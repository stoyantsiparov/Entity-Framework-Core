using System.ComponentModel.DataAnnotations;

namespace Footballers.Data.Models;

public class Team
{
    [Key] public int Id { get; set; }

    [RegularExpression(@"^[a-zA-Z0-9 .-]*$")]
    [StringLength(40, MinimumLength = 3)]
    [Required]
    public string Name { get; set; } = null!;

    [StringLength(40, MinimumLength = 2)]
    [Required]
    public string Nationality { get; set; } = null!;

    [Required]
    public int Trophies { get; set; }

    public virtual ICollection<TeamFootballer> TeamsFootballers { get; set; } = new HashSet<TeamFootballer>();
}