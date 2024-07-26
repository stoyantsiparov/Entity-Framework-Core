using System.ComponentModel.DataAnnotations.Schema;

namespace Trucks.Data.Models;

public class ClientTruck
{
    [ForeignKey(nameof(Client))]
    public int ClientId { get; set; }
    public Client Client { get; set; }

    [ForeignKey(nameof(Truck))]
    public int TruckId { get; set; }
    public Truck Truck { get; set; }
}