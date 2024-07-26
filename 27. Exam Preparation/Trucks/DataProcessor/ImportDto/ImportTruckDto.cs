using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Trucks.Data.Models;
using Trucks.Data.Models.Enums;

namespace Trucks.DataProcessor.ImportDto;

[XmlType(nameof(Truck))]
public class ImportTruckDto
{
    [XmlElement(nameof(RegistrationNumber))]
    [Required]
    [StringLength(8, MinimumLength = 8)]
    [RegularExpression(@"^[A-Z]{2}\d{4}[A-Z]{2}$")]
    public string RegistrationNumber { get; set; } = null!;

    [XmlElement(nameof(VinNumber))]
    [Required]
    [StringLength(17, MinimumLength = 17)]
    public string VinNumber { get; set; } = null!;

    [XmlElement(nameof(TankCapacity))]
    [Range(950, 1420)]
    public int TankCapacity { get; set; }

    [XmlElement(nameof(CargoCapacity))]
    [Range(5000, 29000)]
    public int CargoCapacity { get; set; }

    [XmlElement(nameof(CategoryType))]
    [Required]
    [Range((int)Data.Models.Enums.CategoryType.Flatbed, (int)Data.Models.Enums.CategoryType.Semi)]
    public int CategoryType { get; set; }

    [XmlElement(nameof(MakeType))]
    [Required]
    [Range((int)Data.Models.Enums.MakeType.Daf, (int)Data.Models.Enums.MakeType.Volvo)]
    public int MakeType { get; set; }
}