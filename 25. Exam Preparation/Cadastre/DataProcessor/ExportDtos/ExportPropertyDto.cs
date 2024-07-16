namespace Cadastre.DataProcessor.ExportDtos
{
    using System.Xml.Serialization;

    [XmlType("Property")]
    public class ExportPropertyDto
    {
        [XmlAttribute("postal-code")]
        public string PostalCode { get; set; } = null!;

        [XmlElement(nameof(PropertyIdentifier))]
        public string PropertyIdentifier { get; set; } = null!;

        [XmlElement(nameof(Area))]
        public int Area { get; set; }

        [XmlElement(nameof(Details))]
        public string Details { get; set; } = null!;

        [XmlElement(nameof(DateOfAcquisition))]
        public string DateOfAcquisition { get; set; } = null!;
    }
}
