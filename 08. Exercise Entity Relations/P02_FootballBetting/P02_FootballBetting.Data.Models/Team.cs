using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using P02_FootballBetting.Common;

namespace P02_FootballBetting.Data.Models;

public class Team
{
    public Team()
    {
        HomeTeamGoals = new List<Game>();
        AwayTeamGoals = new List<Game>();
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
    public int PrimaryColorId { get; set; }

    [ForeignKey(nameof(PrimaryColorId))]
    public virtual Color PrimaryColor { get; set; }

    public int SecondaryColorId { get; set; }
    [ForeignKey(nameof(SecondaryColorId))]
    public virtual Color SecondaryColor { get; set; }

    public int TownId { get; set; }
    [ForeignKey(nameof(TownId))]
    public virtual Town Town { get; set; }
    public virtual ICollection<Game> HomeTeamGoals { get; set; }
    public virtual ICollection<Game> AwayTeamGoals { get; set; }
    public virtual ICollection<Player> Players { get; set; }
}