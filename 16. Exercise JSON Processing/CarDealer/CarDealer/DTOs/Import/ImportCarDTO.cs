namespace CarDealer.DTOs.Import;

public class ImportCarDTO
{
    public string Make { get; set; }
    public string Model { get; set; }
    public long TraveledDistance { get; set; }
    public ICollection<int> PartsId { get; set; }
}