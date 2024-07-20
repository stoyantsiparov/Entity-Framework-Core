namespace Medicines.DataProcessor.ExportDtos
{
    using System.Xml.Serialization;

    [XmlType("Patient")]
    public class ExportPatientDto
    {

        [XmlElement("Name")]
        public string FullName { get; set; } = null!;

        [XmlElement("AgeGroup")]
        public string AgeGroup { get; set; } = null!;

        [XmlAttribute("Gender")]
        public string Gender { get; set; } = null!;

        [XmlArray("Medicines")]
        public ExportMedicineDto[] Medicines { get; set; } = new ExportMedicineDto[0];
    }
}
