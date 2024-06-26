using P02_FootballBetting.Common;
using System.ComponentModel.DataAnnotations;

namespace P02_FootballBetting.Data.Models;

public class Position
{
    [Key]
    public int PositionId { get; set; }

    [MaxLength(ValidationConstants.PositionNameMaxLength)]
    public string Name { get; set; } = null!;
}