using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace P01_HospitalDatabase.Data.Models;

public class Medicament
{
    public Medicament()
    {
        Prescriptions = new HashSet<PatientMedicament>();
    }

    [Key]
    public int MedicamentId { get; set; }

    [Unicode]
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    public virtual ICollection<PatientMedicament> Prescriptions { get; set; }
}