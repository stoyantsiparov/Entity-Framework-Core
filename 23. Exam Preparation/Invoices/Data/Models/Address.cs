using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Invoices.Data.Models;

public class Address
{
    [Key] public int Id { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 10)]
    public string StreetName { get; set; }

    [Required]
    public int StreetNumber { get; set; }

    [Required] 
    public string PostCode { get; set; }

    [Required]
    [StringLength(15, MinimumLength = 5)]
    public string City { get; set; }

    [Required]
    [StringLength(15, MinimumLength = 5)]
    public string Country { get; set; }

    [ForeignKey(nameof(Client))]
    public int ClientId { get; set; }
    public Client Client { get; set; }
}