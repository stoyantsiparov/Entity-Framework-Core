using System.Xml.Serialization;
using Boardgames.Data.Models;

namespace Boardgames.DataProcessor.ExportDto;

[XmlType(nameof(Creator))]
public class ExportCreatorDto
{
    [XmlElement(nameof(CreatorName))]
    public string CreatorName { get; set; } = null!;

    [XmlAttribute(nameof(BoardgamesCount))]
    public int BoardgamesCount { get; set; }

    [XmlArray(nameof(Boardgames))]
    public ExportBoardgameDto[] Boardgames { get; set; } = null!;
}