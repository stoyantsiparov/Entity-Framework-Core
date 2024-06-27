using P02_FootballBetting.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models;

public class Game
{
    public Game()
    {
        Bets = new List<Bet>();
        PlayersStatistics = new List<PlayerStatistic>();
    }
    [Key]
    public int GameId { get; set; }
    public int HomeTeamId { get; set; }
    public int AwayTeamId { get; set; }
    public int HomeTeamGoals { get; set; }

    [ForeignKey(nameof(HomeTeamGoals))]
    public virtual Team HomeTeam { get; set; }
    public int AwayTeamGoals { get; set; }

    [ForeignKey(nameof(AwayTeamGoals))]
    public virtual Team AwayTeam { get; set; }
    public int HomeTeamBetRate { get; set; }
    public int AwayTeamBetRate { get; set; }
    public int DrawBetRate { get; set; }
    public DateTime DateTime { get; set; }
    public int Result { get; set; }
    public virtual ICollection<Bet> Bets { get; set; }
    public virtual ICollection<PlayerStatistic> PlayersStatistics { get; set; }
}