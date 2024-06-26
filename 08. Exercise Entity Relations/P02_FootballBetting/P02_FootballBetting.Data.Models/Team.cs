using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using P02_FootballBetting.Common;

namespace P02_FootballBetting.Data.Models;

public class Team
{
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
}