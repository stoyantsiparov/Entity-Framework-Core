using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using P02_FootballBetting.Common;

namespace P02_FootballBetting.Data.Models;

public class Team
{
    public Team()
    {
        HomeGames = new HashSet<Game>();
        AwayGames = new HashSet<Game>();
        Players = new HashSet<Player>();
    }

    [Key]
    public int TeamId { get; set; }

    [MaxLength(ValidationConstants.TeamNameLength)]
    public string Name { get; set; }

    [MaxLength(ValidationConstants.TeamUrlLogoLength)]
    public string LogoUrl { get; set; }

    [MaxLength(ValidationConstants.TeamInitialsLength)]
    public string Initials { get; set; }
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