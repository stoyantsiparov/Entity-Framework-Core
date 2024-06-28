using P02_FootballBetting.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models;

public class Town
{
    public Town()
    {
        Teams = new HashSet<Team>();
        Players = new HashSet<Player>();
    }

    [Key]
    public int TownId { get; set; }

    [MaxLength(ValidationConstants.TownMaxNameLength)]
    public string Name { get; set; } = null!;

    [ForeignKey(nameof(CountryId))]
    public int CountryId { get; set; }
    public virtual Country Country { get; set; }
    public virtual ICollection<Team> Teams { get; set; }
    public virtual ICollection<Player> Players { get; set; }
}