using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace P01_StudentSystem.Data.Models;

public class Course
{
    [Key] 
    public int CourseId { get; set; }

    [Unicode]
    [Required] 
    [MaxLength(80)] 
    public string Name { get; set; }

    [Unicode]
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Price { get; set; }
    public virtual ICollection<StudentCourse> StudentsCourses { get; set; }
    public virtual ICollection<Resource> Resources { get; set; }
    public virtual ICollection<Homework> Homeworks { get; set; }
}