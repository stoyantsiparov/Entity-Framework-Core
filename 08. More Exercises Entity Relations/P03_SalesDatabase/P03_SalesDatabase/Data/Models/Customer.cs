using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace P03_SalesDatabase.Data.Models;

public class Customer
{
    public Customer()
    {
        Sales = new HashSet<Sale>();
    }

    [Key]
    public int CustomerId { get; set; }

    [Unicode]
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [EmailAddress]
    [MaxLength(80)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Required]
    public string CreditCardNumber { get; set; }

    public virtual ICollection<Sale> Sales { get; set; }
}