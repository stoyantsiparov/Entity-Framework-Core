using System.ComponentModel.DataAnnotations;
using P02_FootballBetting.Common;

namespace P02_FootballBetting.Data.Models;

public class User
{
    public User()
    {
        Bets = new HashSet<Bet>();
    }
    [Key]
    public int UserId { get; set; }

    [MaxLength(ValidationConstants.UsernameMaxLength)]
    public string Username { get; set; } = null!;

    [MaxLength(ValidationConstants.PasswordMaxLength)]
    public string Password { get; set; } = null!;

    [MaxLength(ValidationConstants.EmailMaxLength)]
    public string Email { get; set; }

    [MaxLength(ValidationConstants.NameMaxLength)]
    public string Name { get; set; } = null!;
    public decimal Balance { get; set; }
    public virtual ICollection<Bet> Bets { get; set; }
}