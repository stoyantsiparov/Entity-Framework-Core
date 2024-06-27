using P02_FootballBetting.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models;

public class Color
{
    public Color()
    {
        PrimaryKitTeams = new List<Team>();
        SecondaryKitTeams = new List<Team>();
    }

    [Key]
    public int ColorId { get; set; }

    [MaxLength(ValidationConstants.ColorNameMaxLength)]
    public string Name { get; set; } = null!;

    [InverseProperty(nameof(Team.PrimaryColor))]
    public virtual ICollection<Team> PrimaryKitTeams { get; set; }

    [InverseProperty(nameof(Team.SecondaryColor))]
    public virtual ICollection<Team> SecondaryKitTeams { get; set; }
}