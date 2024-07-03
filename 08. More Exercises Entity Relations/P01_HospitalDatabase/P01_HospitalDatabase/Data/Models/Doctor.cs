using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace P01_HospitalDatabase.Data.Models;

public class Doctor
{
    public Doctor()
    {
        Visitations = new HashSet<Visitation>();
    }

    [Key]
    public int DoctorId { get; set; }

    [Unicode]
    [Required]
    [MaxLength(30)]
    public string Name { get; set; }
    public string? Specialty { get; set; }

    public virtual ICollection<Visitation> Visitations { get; set; }
}