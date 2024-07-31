using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Footballers.Data.Models.Enums;

namespace Footballers.Data.Models;

public class Footballer
{
    [Key] public int Id { get; set; }

    [StringLength(40, MinimumLength = 2)]
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public DateTime ContractStartDate { get; set; }

    [Required]
    public DateTime ContractEndDate { get; set; }

    [Required]
    public PositionType PositionType { get; set; }

    [Required]
    public BestSkillType BestSkillType { get; set; }

    [ForeignKey(nameof(Coach))]
    public int CoachId { get; set; }
    public Coach Coach { get; set; }

    public virtual ICollection<TeamFootballer> TeamsFootballers { get; set; } = new HashSet<TeamFootballer>();
}