using P02_FootballBetting.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models;

public class PlayerStatistic
{
    [ForeignKey(nameof(GameId))]
    public int GameId { get; set; }
    public virtual Game Game { get; set; }

    [ForeignKey(nameof(PlayerId))]
    public int PlayerId { get; set; }
    public virtual Player Player { get; set; }
    public byte ScoredGoals { get; set; }
    public byte Assists { get; set; }

    [MaxLength(ValidationConstants.MaxMinutesPlayedLength)]
    public int MinutesPlayed { get; set; }
}