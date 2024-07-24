using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Boardgames.Data.Models;

public class Seller
{
    public Seller()
    {
        BoardgamesSellers = new HashSet<BoardgameSeller>();
    }

    [Key] public int Id { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 5)]
    public string Name { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 2)]
    public string Address { get; set; }

    [Required] 
    public string Country { get; set; }

    [Required]
    public string Website { get; set; }
    
    public virtual ICollection<BoardgameSeller> BoardgamesSellers { get; set; }
}