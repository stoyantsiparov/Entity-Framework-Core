using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Artillery.Data.Models;

namespace Artillery.DataProcessor.ImportDto;

[XmlType(nameof(Shell))]
public class ImportShellDTO
{
    [XmlElement(nameof(ShellWeight))]
    [Required]
    [Range(2, 1680)]
    public double ShellWeight { get; set; }

    [XmlElement(nameof(Caliber))]
    [Required]
    [StringLength(30, MinimumLength = 4)]
    public string Caliber { get; set; } = null!;
}