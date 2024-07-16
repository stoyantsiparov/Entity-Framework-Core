namespace Cadastre.Data.Models
{

    using System.ComponentModel.DataAnnotations.Schema;
    public class PropertyCitizen
    {
        public int PropertyId { get; set; }

        [ForeignKey(nameof(PropertyId))]
        public virtual Property Property { get; set; } = null!;

        public int CitizenId { get; set; }

        [ForeignKey(nameof(CitizenId))]
        public virtual Citizen Citizen { get; set; } = null!;
    }
}
