namespace Medicines.DataProcessor.ExportDtos
{
    using System.Xml.Serialization;
    [XmlType("Medicine")]
    public class ExportMedicineDto
    {
        [XmlElement("Name")]
        public string Name { get; set; } = null!;

        [XmlElement("Price")]
        public string Price { get; set; } = null!;

        [XmlAttribute("Category")]
        public string Category { get; set; } = null!;

        [XmlElement("Producer")]
        public string Producer { get; set; } = null!;

        [XmlElement("BestBefore")]
        public string ExpiryDate { get; set; } = null!;
    }
}
