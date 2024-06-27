using Microsoft.EntityFrameworkCore;
using P02_FootballBetting.Data.Models;

namespace P02_FootballBetting.Data;

public class FootballBettingContext : DbContext
{
    private const string ConnectionString = "Server=OMEN\\SQLEXPRESS;Database=FootballBetting;Integrated Security=True;";

    public FootballBettingContext()
    {
        
    }
    public FootballBettingContext(DbContextOptions dbContextOptions)
    : base(dbContextOptions)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString);
    }

    public DbSet<Country> Countries { get; set; }
    public DbSet<Town> Towns { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Bet> Bets { get; set; }
    public DbSet<PlayerStatistic> PlayersStatistics { get; set; }
    public DbSet<Player> Players { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Composite primary key (GameId, PlayerId)
        modelBuilder.Entity<PlayerStatistic>()
            .HasKey(ps => new { ps.GameId, ps.PlayerId });

        modelBuilder.Entity<Game>()
            .HasOne(g => g.HomeTeam)
            .WithMany(t => t.HomeTeamGoals)
            .HasForeignKey(g => g.HomeTeamId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Game>()
            .HasOne(g => g.AwayTeam)
            .WithMany(t => t.AwayTeamGoals)
            .HasForeignKey(g => g.AwayTeamId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}