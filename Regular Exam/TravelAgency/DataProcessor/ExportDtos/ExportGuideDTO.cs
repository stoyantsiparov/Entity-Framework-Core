using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using TravelAgency.Data.Models;

namespace TravelAgency.DataProcessor.ExportDtos;

[XmlType(nameof(Guide))]
public class ExportGuideDTO
{
    [XmlElement(nameof(FullName))]
    public string FullName { get; set; } = null!;

    [XmlArray(nameof(TourPackages))]
    public ExportTourPackageDTO[] TourPackages { get; set; } = null!;
}