using System.ComponentModel.DataAnnotations;

namespace Trucks.Data.Models;

public class Client
{
    public Client()
    {
        ClientsTrucks = new HashSet<ClientTruck>();
    }

    [Key] public int Id { get; set; }

    [Required]
    [StringLength(40, MinimumLength = 3)]
    public string Name { get; set; }

    [Required]
    [StringLength(40, MinimumLength = 2)]
    public string Nationality { get; set; }

    [Required]
    public string Type { get; set; }

    public virtual ICollection<ClientTruck> ClientsTrucks { get; set; }
}