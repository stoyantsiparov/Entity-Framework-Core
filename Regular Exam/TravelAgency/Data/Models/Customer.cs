using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Data.Models;

public class Customer
{
    [Key] public int Id { get; set; }

    [Required]
    [StringLength(60, MinimumLength = 4)]
    public string FullName { get; set; } = null!;

    [Required]
    [StringLength(50, MinimumLength = 6)]
    public string Email { get; set; } = null!;

    [Required]
    [RegularExpression(@"^\+\d{12}$")]
    public string PhoneNumber { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
}