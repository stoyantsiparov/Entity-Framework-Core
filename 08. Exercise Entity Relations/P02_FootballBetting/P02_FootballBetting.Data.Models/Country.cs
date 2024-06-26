using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using P02_FootballBetting.Common;

namespace P02_FootballBetting.Data.Models;

public class Country
{
    [Key]
    public int CountryId { get; set; }

    [Required]
    [MaxLength(ValidationConstants.CountryMaxNameLength)]
    public string Name { get; set; }

    public ICollection<Town> Towns { get; set; }
}