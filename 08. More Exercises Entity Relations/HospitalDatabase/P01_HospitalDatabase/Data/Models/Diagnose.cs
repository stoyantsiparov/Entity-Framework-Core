using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace P01_HospitalDatabase.Data.Models;

public class Diagnose
{
    [Key]
    public int DiagnoseId { get; set; }

    [Unicode]
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [Unicode]
    [MaxLength(250)]
    public string? Comments { get; set; }

    [ForeignKey(nameof(Patient))]
    public int PatientId { get; set; }
    public virtual Patient Patient { get; set; }
}