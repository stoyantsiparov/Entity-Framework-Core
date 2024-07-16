namespace Cadastre.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Cadastre.Common;
    using Cadastre.Data.Enumerations;
    public class Citizen
    {
        public Citizen()
        {
            PropertiesCitizens = new HashSet<PropertyCitizen>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.CitizenNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(ValidationConstants.CitizenNameMaxLength)]
        public string LastName { get; set; } = null!;

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public MaritalStatus MaritalStatus { get; set; }

        public virtual ICollection<PropertyCitizen> PropertiesCitizens { get; set; }
    }
}
