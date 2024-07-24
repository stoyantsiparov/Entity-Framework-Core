namespace Medicines.Data.Models
{
    using Medicines.Common;
    using Medicines.Data.Models.Enums;
    using System.ComponentModel.DataAnnotations;
    public class Patient
    {
        public Patient()
        {
            this.PatientsMedicines = new List<PatientMedicine>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.PatientFullNameMaxLength)]
        public string FullName { get; set; } = null!;

        public AgeGroup AgeGroup { get; set; }

        public Gender Gender { get; set; }

        public virtual ICollection<PatientMedicine> PatientsMedicines { get; set; }
    }
}
