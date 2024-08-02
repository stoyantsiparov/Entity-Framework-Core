using System.ComponentModel.DataAnnotations;
using Artillery.Data.Models.Enums;
using Newtonsoft.Json;

namespace Artillery.DataProcessor.ImportDto;

public class ImportGunDTO
{
    public int ManufacturerId { get; set; }
    [Range(100, 1_350_000)]
    public int GunWeight { get; set; }

    [Range(2.00, 35.00)]
    public double BarrelLength { get; set; }

    public int? NumberBuild { get; set; }

    [Range(1, 100_000)]
    public int Range { get; set; }

    [Required]
    public string GunType { get; set; }

    public int ShellId { get; set; }

    public ImportCountryGunDTO[] Countries { get; set; }
}