using Boardgames.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Boardgames.Data.Models;

namespace Boardgames.DataProcessor.ImportDto;

[XmlType(nameof(Boardgame))]
public class ImportBoardgamesDto
{
    [XmlElement(nameof(Name))]
    [Required]
    [StringLength(20, MinimumLength = 10)]
    public string Name { get; set; } = null!;

    [XmlElement(nameof(Rating))]
    [Required]
    [Range(1, 10.00)]
    public double Rating { get; set; }

    [XmlElement(nameof(YearPublished))]
    [Required]
    [Range(2018, 2023)]
    public int YearPublished { get; set; }

    [XmlElement(nameof(CategoryType))]
    [Required]
    [Range((int)Data.Models.Enums.CategoryType.Abstract, (int)Data.Models.Enums.CategoryType.Strategy)]
    public int CategoryType { get; set; }

    [XmlElement(nameof(Mechanics))]
    [Required]
    public string Mechanics { get; set; } = null!;
}