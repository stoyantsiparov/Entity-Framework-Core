using System.ComponentModel.DataAnnotations;

namespace Footballers.Data.Models;

public class Coach
{
    [Key] public int Id { get; set; }

    [StringLength(40, MinimumLength = 2)]
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Nationality { get; set; } = null!;

    public virtual ICollection<Footballer> Footballers { get; set; } = new HashSet<Footballer>();
}