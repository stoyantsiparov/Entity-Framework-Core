using System.Xml.Serialization;
using Trucks.Data.Models;

namespace Trucks.DataProcessor.ExportDto;

[XmlType(nameof(Truck))]
public class ExportTruckDto
{
    [XmlElement(nameof(RegistrationNumber))]
    public string RegistrationNumber { get; set; } = null!;
    
    [XmlElement(nameof(Make))]
    public string Make { get; set; } = null!;
}