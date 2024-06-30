using P01_HospitalDatabase.Data.Models;
using P01_HospitalDatabase.Data;

namespace P01_HospitalDatabase;

internal class StartUp
{
    public static void Main()
    {
        using var context = new HospitalContext();
        context.Database.EnsureCreated();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Hospital Database Management System");
            Console.WriteLine("1. Add Doctor");
            Console.WriteLine("2. Add Patient");
            Console.WriteLine("3. View All Patients");
            Console.WriteLine("4. Add Visitation");
            Console.WriteLine("5. View Visitations");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddDoctor(context);
                    break;
                case "2":
                    AddPatient(context);
                    break;
                case "3":
                    ViewAllPatients(context);
                    break;
                case "4":
                    AddVisitation(context);
                    break;
                case "5":
                    ViewVisitations(context);
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }
    }
    static void AddDoctor(HospitalContext context)
    {
        Console.Clear();
        Console.WriteLine("Add New Doctor");

        Console.Write("Name: ");
        string name = Console.ReadLine();

        Console.Write("Specialty: ");
        string specialty = Console.ReadLine();

        var doctor = new Doctor
        {
            Name = name,
            Specialty = specialty
        };

        context.Doctor.Add(doctor);
        context.SaveChanges();

        Console.WriteLine("Doctor added successfully. Press any key to continue...");
        Console.ReadKey();
    }

    static void AddPatient(HospitalContext context)
    {
        Console.Clear();
        Console.WriteLine("Add New Patient");
        Console.Write("First Name: ");
        string firstName = Console.ReadLine();
        Console.Write("Last Name: ");
        string lastName = Console.ReadLine();
        Console.Write("Address: ");
        string address = Console.ReadLine();
        Console.Write("Email: ");
        string email = Console.ReadLine();
        Console.Write("Has Insurance (true/false): ");
        bool hasInsurance = bool.Parse(Console.ReadLine());

        var patient = new Patient
        {
            FirstName = firstName,
            LastName = lastName,
            Address = address,
            Email = email,
            HasInsurance = hasInsurance
        };

        context.Patients.Add(patient);
        context.SaveChanges();
        Console.WriteLine("Patient added successfully. Press any key to continue...");
        Console.ReadKey();
    }

    static void ViewAllPatients(HospitalContext context)
    {
        Console.Clear();
        Console.WriteLine("List of Patients");
        var patients = context.Patients.ToList();
        foreach (var patient in patients)
        {
            Console.WriteLine($"ID: {patient.PatientId}, Name: {patient.FirstName} {patient.LastName}, Email: {patient.Email}");
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    static void AddVisitation(HospitalContext context)
    {
        Console.Clear();
        Console.WriteLine("Add New Visitation");

        Console.Write("Patient ID: ");
        int patientId = int.Parse(Console.ReadLine());

        Console.Write("Doctor ID: ");
        int doctorId = int.Parse(Console.ReadLine());

        Console.Write("Comments: ");
        string comments = Console.ReadLine();

        var visitation = new Visitation
        {
            PatientId = patientId,
            DoctorId = doctorId,
            Date = DateTime.Now,
            Comments = comments
        };

        context.Visitations.Add(visitation);
        context.SaveChanges();

        Console.WriteLine("Visitation added successfully. Press any key to continue...");
        Console.ReadKey();
    }


    static void ViewVisitations(HospitalContext context)
    {
        Console.Clear();
        Console.WriteLine("List of Visitations");
        var visitations = context.Visitations
            .Select(v => new
            {
                v.VisitationId,
                v.Date,
                v.Comments,
                PatientName = v.Patient.FirstName + " " + v.Patient.LastName
            })
            .ToList();

        foreach (var visitation in visitations)
        {
            Console.WriteLine($"ID: {visitation.VisitationId}, Date: {visitation.Date}, Patient: {visitation.PatientName}, Comments: {visitation.Comments}");
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}