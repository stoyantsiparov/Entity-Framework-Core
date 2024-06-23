using System.Text;
using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;

namespace SoftUni;

public class StartUp
{
    public static void Main()
    {
        var context = new SoftUniContext();
        Console.WriteLine(RemoveTown(context));
    }

    // 03. Employees Full Information
    public static string GetEmployeesFullInformation(SoftUniContext context)
    {
        var employees = string.Join(Environment.NewLine, context.Employees
                .Select(e => $"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:F2}")
                .ToList());

        return employees;
    }

    //04. Employees with Salary Over 50 000
    public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
    {
        var employees = context.Employees
            .Select(e => new
            {
                e.FirstName,
                e.Salary
            })
            .Where(e => e.Salary > 50000)
            .OrderBy(e => e.FirstName);

        var sb = new StringBuilder();

        foreach (var e in employees)
        {
            sb.AppendLine($"{e.FirstName} - {e.Salary:F2}");
        }

        return sb.ToString().TrimEnd();
    }

    //05. Employees from Research and Development
    public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
    {
        var employees = context.Employees
            .Select(e => new
            {
                e.FirstName,
                e.LastName,
                e.Department,
                e.Salary
            })
            .Where(e => e.Department.Name == "Research and Development")
            .OrderBy(e => e.Salary)
            .ThenByDescending(e => e.FirstName);

        var sb = new StringBuilder();

        foreach (var e in employees)
        {
            sb.AppendLine($"{e.FirstName} {e.LastName} from {e.Department.Name} - ${e.Salary:F2}");
        }

        return sb.ToString().TrimEnd();
    }

    //06. Adding a New Address and Updating Employee
    public static string AddNewAddressToEmployee(SoftUniContext context)
    {
        Address newAddress = new Address()
        {
            AddressText = "Vitoshka 15",
            TownId = 4
        };

        var nakov = context.Employees
            .FirstOrDefault(e => e.LastName == "Nakov");

        if (nakov != null)
        {
            nakov.Address = newAddress;
            context.SaveChanges();
        }

        var employees = context.Employees
            .OrderByDescending(e => e.AddressId)
            .Take(10)
            .Select(e => e.Address.AddressText);

        return string.Join(Environment.NewLine, employees);
    }

    //07. Employees and Projects
    public static string GetEmployeesInPeriod(SoftUniContext context)
    {
        var result = context.Employees
            .Take(10)
            .Select(e => new
            {
                EmployeeNames = $"{e.FirstName} {e.LastName}",
                ManagerNames = $"{e.Manager.FirstName} {e.Manager.LastName}",
                Projects = e.EmployeesProjects
                    .Where(ep => ep.Project.StartDate.Year >= 2001 && ep.Project.StartDate.Year <= 2003)
                    .Select(ep => new
                    {
                        ProjectName = ep.Project.Name,
                        ep.Project.StartDate,
                        ep.Project.EndDate
                    })
            });

        var sb = new StringBuilder();

        foreach (var e in result)
        {
            sb.AppendLine($"{e.EmployeeNames} - Manager: {e.ManagerNames}");
            if (e.Projects.Any())
            {
                foreach (var p in e.Projects)
                {
                    sb.AppendLine(p.EndDate.HasValue
                        ? $"--{p.ProjectName} - {p.StartDate:M/d/yyyy h:mm:ss tt} - {p.EndDate:M/d/yyyy h:mm:ss tt}"
                        : $"--{p.ProjectName} - {p.StartDate:M/d/yyyy h:mm:ss tt} - not finished");
                }
            }
        }

        return sb.ToString().TrimEnd();
    }

    //08. Addresses by Town
    public static string GetAddressesByTown(SoftUniContext context)
    {
        var addressesByTown = context.Addresses
            .Take(10)
            .Select(a => new
            {
                a.AddressText,
                TownName = a.Town.Name,
                EmployeesCount = a.Employees.Count
            })
            .OrderByDescending(a => a.EmployeesCount)
            .ThenBy(a => a.TownName)
            .ThenBy(a => a.AddressText);

        var sb = new StringBuilder();
        foreach (var a in addressesByTown)
        {
            sb.AppendLine($"{a.AddressText}, {a.TownName} - {a.EmployeesCount} employees");
        }

        return sb.ToString().TrimEnd();
    }

    //09. Employee 147
    public static string GetEmployee147(SoftUniContext context)
    {
        var employee = context.Employees
            .Select(e => new
            {
                e.EmployeeId,
                e.FirstName,
                e.LastName,
                e.JobTitle
            })
            .FirstOrDefault(e => e.EmployeeId == 147);

        var projectNames = context.EmployeesProjects
            .Where(ep => ep.EmployeeId == 147)
            .Select(ep => ep.Project.Name)
            .OrderBy(pn => pn)
            .ToList();

        var sb = new StringBuilder();

        sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
        foreach (var projectName in projectNames)
        {
            sb.AppendLine(projectName);
        }

        return sb.ToString().TrimEnd();
    }

    //10. Departments with More Than 5 Employees
    public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
    {
        var departments = context.Departments
            .Where(d => d.Employees.Count > 5)
            .Select(d => new
            {
                d.Name,
                ManagerNames = $"{d.Manager.FirstName} {d.Manager.LastName}",
                Employees = d.Employees
                    .Select(e => new
                    {
                        e.FirstName,
                        e.LastName,
                        e.JobTitle
                    })
                    .OrderBy(e => e.FirstName)
                    .ThenBy(e => e.LastName)
                    .ToList()
            })
            .OrderBy(d => d.Employees.Count)
            .ThenBy(d => d.Name);

        var sb = new StringBuilder();

        foreach (var d in departments)
        {
            sb.AppendLine($"{d.Name} - {d.ManagerNames}");

            foreach (var e in d.Employees)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle}");
            }
        }

        return sb.ToString().TrimEnd();
    }

    //11. Find Latest 10 Projects
    public static string GetLatestProjects(SoftUniContext context)
    {
        var lateProjects = context.Projects
            .OrderByDescending(p => p.StartDate)
            .Take(10)
            .OrderBy(p => p.Name)
            .Select(p => new
            {
                p.Name,
                p.Description,
                p.StartDate
            });

        var sb = new StringBuilder();

        foreach (var p in lateProjects)
        {
            sb.AppendLine(p.Name);
            sb.AppendLine(p.Description);
            sb.AppendLine($"{p.StartDate:M/d/yyyy h:mm:ss tt}");
        }

        return sb.ToString().TrimEnd();
    }

    //12. Increase Salaries
    public static string IncreaseSalaries(SoftUniContext context)
    {
        string[] departments = { "Engineering", "Tool Design", "Marketing", "Information Services" };

        var employees = context.Employees
            .Where(e => departments.Contains(e.Department.Name))
            .OrderBy(e => e.FirstName)
            .ThenBy(e => e.LastName);

        var sb = new StringBuilder();

        foreach (var e in employees)
        {
            decimal salary = e.Salary;
            decimal bonus = salary * 0.12m;
            decimal newSalary = salary + bonus;

            e.Salary = newSalary;

            sb.AppendLine($"{e.FirstName} {e.LastName} (${e.Salary:F2})");
        }

        return sb.ToString().TrimEnd();
    }

    //13. Find Employees by First Name Starting With Sa
    public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
    {
        var employees = context.Employees
            .Select(e => new
            {
                e.FirstName,
                e.LastName,
                e.JobTitle,
                e.Salary
            })
            .Where(e => e.FirstName.StartsWith("Sa"))
            .OrderBy(e => e.FirstName)
            .ThenBy(e => e.LastName);

        var sb = new StringBuilder();

        foreach (var e in employees)
        {
            sb.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:F2})");
        }

        return sb.ToString().TrimEnd();
    }

    //14. Delete Project by Id
    //TODO
    public static string DeleteProjectById(SoftUniContext context)
    {
        return "";
    }

    //15. Remove Town
    //TODO
    public static string RemoveTown(SoftUniContext context)
    {
        return "";
    }
}