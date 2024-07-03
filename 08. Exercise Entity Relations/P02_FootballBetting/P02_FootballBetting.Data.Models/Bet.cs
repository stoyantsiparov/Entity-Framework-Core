using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using P02_FootballBetting.Common;

namespace P02_FootballBetting.Data.Models;

public class Bet
{
    [Key]
    public int BetId { get; set; }
    public decimal Amount { get; set; }

    [MaxLength(ValidationConstants.BetPredictionLength)]
    public string Prediction { get; set; }
    public DateTime DateTime { get; set; }

    [ForeignKey(nameof(UserId))]
    public int UserId { get; set; }
    public virtual User User { get; set; }

    [ForeignKey(nameof(GameId))]
    public int GameId { get; set; }
    public virtual Game Game { get; set; }
}