using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace P03_SalesDatabase.Data.Models;

public class Store
{
    public Store()
    {
        Sales = new HashSet<Sale>();
    }

    [Key]
    public int StoreId { get; set; }

    [Unicode]
    [Required]
    [MaxLength(80)]
    public string Name { get; set; }

    public virtual ICollection<Sale> Sales { get; set; }    
}