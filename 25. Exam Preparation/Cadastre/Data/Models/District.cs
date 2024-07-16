namespace Cadastre.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Cadastre.Common;
    using Cadastre.Data.Enumerations;
    public class District
    {
        public District()
        {
            Properties = new HashSet<Property>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.DistrictNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]        
        public string PostalCode { get; set; } = null!;

        public Region Region { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
    }
}
