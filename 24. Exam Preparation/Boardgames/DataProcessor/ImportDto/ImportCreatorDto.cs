using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Boardgames.Data.Models;

namespace Boardgames.DataProcessor.ImportDto;

[XmlType(nameof(Creator))]
public class ImportCreatorDto
{
    [XmlElement(nameof(FirstName))]
    [Required]
    [StringLength(7, MinimumLength = 2)]
    public string FirstName { get; set; } = null!;

    [XmlElement(nameof(LastName))]
    [Required]
    [StringLength(7, MinimumLength = 2)]
    public string LastName { get; set; } = null!;

    [XmlArray(nameof(Boardgames))]
    public ImportBoardgamesDto[] Boardgames { get; set; } = null!;
}