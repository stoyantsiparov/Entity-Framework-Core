using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Data.Models;

public class TourPackage
{
    [Key] public int Id { get; set; }

    [Required]
    [StringLength(40, MinimumLength = 2)]
    public string PackageName { get; set; } = null!;

    [StringLength(200)]
    public string Description { get; set; } = null!;

    [Required]
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
    public virtual ICollection<TourPackageGuide> TourPackagesGuides { get; set; } = new HashSet<TourPackageGuide>();
}