using System.ComponentModel.DataAnnotations;

namespace TravelAgency.DataProcessor.ImportDtos;

public class ImportBookingDTO
{
    [Required]
    public string BookingDate { get; set; } = null!;

    [Required]
    [StringLength(60, MinimumLength = 4)]
    public string CustomerName { get; set; } = null!;

    [Required]
    [StringLength(40, MinimumLength = 2)]
    public string TourPackageName { get; set; } = null!;
}