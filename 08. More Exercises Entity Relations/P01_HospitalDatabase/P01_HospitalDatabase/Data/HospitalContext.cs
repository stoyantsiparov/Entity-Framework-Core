using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data;

public class HospitalContext : DbContext
{
    private const string ConnectionString = "Server=OMEN\\SQLEXPRESS;Database=HospitalDatabase;Integrated Security=True;";

    public HospitalContext()
    {

    }

    public HospitalContext(DbContextOptions options)
        : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString);
    }

    public DbSet<Doctor> Doctor { get; set; }
    public DbSet<Visitation> Visitations { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Diagnose> Diagnoses { get; set; }
    public DbSet<PatientMedicament> PatientsMedicaments { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PatientMedicament>()
            .HasKey(pm => new { pm.PatientId, pm.MedicamentId });
    }
}