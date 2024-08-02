using System.ComponentModel.DataAnnotations;

namespace Artillery.Data.Models;

public class Shell
{
    [Key] public int Id { get; set; }

    [Required]
    [Range(2, 1680)]
    public double ShellWeight { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 4)]
    public string Caliber { get; set; } = null!;

    public virtual ICollection<Gun> Guns { get; set; } = new HashSet<Gun>();
}