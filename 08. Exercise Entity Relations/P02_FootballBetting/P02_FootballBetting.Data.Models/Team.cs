using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using P02_FootballBetting.Common;

namespace P02_FootballBetting.Data.Models;

public class Team
{
    public Team()
    {
        HomeGames = new List<Game>();
        AwayGames = new List<Game>();
        Players = new HashSet<Player>();
    }

    [Key]
    public int TeamId { get; set; }
    [MaxLength(ValidationConstants.TeamNameMaxLength)]
    public string Name { get; set; } = null!;

    [MaxLength(ValidationConstants.LogoUrlMaxLength)]
    public string LogoUrl { get; set; }

    [MaxLength(ValidationConstants.InitialsMaxLength)]
    public string Initials { get; set; } = null!;
    public decimal Budget { get; set; }

    [ForeignKey(nameof(PrimaryKitColorId))]
    public int PrimaryKitColorId { get; set; }
    public virtual Color PrimaryKitColor { get; set; }

    [ForeignKey(nameof(SecondaryKitColorId))]
    public int SecondaryKitColorId { get; set; }
    public virtual Color SecondaryKitColor { get; set; }

    [ForeignKey(nameof(TownId))]
    public int TownId { get; set; }
    public virtual Town Town { get; set; }
    public virtual ICollection<Game> HomeGames { get; set; }
    public virtual ICollection<Game> AwayGames { get; set; }
    public virtual ICollection<Player> Players { get; set; }
}