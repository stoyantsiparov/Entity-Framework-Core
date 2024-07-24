namespace Medicines.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Medicines.Common;
    public class Pharmacy
    {
        public Pharmacy()
        {
            this.Medicines = new List<Medicine>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.PharmacyNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        public string PhoneNumber { get; set; } = null!;

        public bool IsNonStop { get; set; }

        public virtual ICollection<Medicine> Medicines { get; set; }
    }
}
