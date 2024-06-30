using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using P02_FootballBetting.Common;

namespace P02_FootballBetting.Data.Models;

public class Country
{
    public Country()
    {
        Towns = new HashSet<Town>();
    }

    [Key]
    public int CountryId { get; set; }

    [MaxLength(ValidationConstants.CountryNameLength)]
    public string Name { get; set; }

    public virtual ICollection<Town> Towns { get; set; }
}