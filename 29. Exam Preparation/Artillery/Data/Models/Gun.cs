using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Artillery.Data.Models.Enums;

namespace Artillery.Data.Models;

public class Gun
{
    [Key] public int Id { get; set; }

    [ForeignKey(nameof(Manufacturer))]
    public int ManufacturerId { get; set; }
    public Manufacturer Manufacturer { get; set; }

    [Required]
    [Range(100, 1350000)]
    public int GunWeight { get; set; }

    [Required]
    [Range(2.00, 35.00)]
    public double BarrelLength { get; set; }
    
    public int? NumberBuild { get; set; }

    [Required]
    [Range(1, 100000)]
    public int Range { get; set; }

    [Required]
    public GunType GunType { get; set; }

    [ForeignKey(nameof(Shell))]
    public int ShellId { get; set; }
    public Shell Shell { get; set; }

    public virtual ICollection<CountryGun> CountriesGuns { get; set; } = new HashSet<CountryGun>();
}