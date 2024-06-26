using P02_FootballBetting.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models;

public class Town
{
    public Town()
    {
        Teams = new HashSet<Team>();
    }

    [Key]
    public int TownId { get; set; }

    [MaxLength(ValidationConstants.TownMaxNameLength)]
    public string Name { get; set; } = null!;
    public int CountryId { get; set; }
    [ForeignKey(nameof(CountryId))]
    public Country Country { get; set; }
    public ICollection<Team> Teams { get; set; }
}