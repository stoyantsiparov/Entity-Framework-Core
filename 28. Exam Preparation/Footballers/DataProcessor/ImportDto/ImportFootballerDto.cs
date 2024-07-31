using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Footballers.Data.Models;
using Footballers.Data.Models.Enums;

namespace Footballers.DataProcessor.ImportDto;

[XmlType(nameof(Footballer))]
public class ImportFootballerDto
{
    [XmlElement(nameof(Name))]
    [StringLength(40, MinimumLength = 2)]
    [Required]
    public string Name { get; set; } = null!;

    [XmlElement(nameof(ContractStartDate))]
    [Required]
    public string ContractStartDate { get; set; } = null!;

    [XmlElement(nameof(ContractEndDate))]
    [Required]
    public string ContractEndDate { get; set; } = null!;

    [XmlElement(nameof(PositionType))]
    [Required]
    [Range((int)Data.Models.Enums.PositionType.Goalkeeper, (int)Data.Models.Enums.PositionType.Forward)]
    public int PositionType { get; set; }

    [XmlElement(nameof(BestSkillType))]
    [Required]
    [Range((int)Data.Models.Enums.BestSkillType.Defence, (int)Data.Models.Enums.BestSkillType.Speed)]
    public int BestSkillType { get; set; }
}