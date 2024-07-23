using System.ComponentModel.DataAnnotations;
using Invoices.Data.Models.Enums;

namespace Invoices.Data.Models;

public class Product
{
    public Product()
    {
        ProductsClients = new HashSet<ProductClient>();
    }

    [Key] public int Id { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 9)]
    public string Name { get; set; }

    [Required]
    [Range(5.00, 1000.00)]
    public decimal Price { get; set; }

    [Required]
    public CategoryType CategoryType { get; set; }

    public virtual ICollection<ProductClient> ProductsClients { get; set; }
}