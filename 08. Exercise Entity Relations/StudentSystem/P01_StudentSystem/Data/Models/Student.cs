using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace P01_StudentSystem.Data.Models;

public class Student
{
    [Key] 
    public int StudentId { get; set; }

    [Unicode]
    [Required]
    [MaxLength(100)] 
    public string Name { get; set; }

    [MaxLength(10)]
    [MinLength(10)]
    [Unicode(false)]
    public string PhoneNumber { get; set; }
    public DateTime RegisteredOn { get; set; }
    public DateTime? Birthday  { get; set; }
    public virtual ICollection<StudentCourse> StudentsCourses { get; set; }
    public virtual ICollection<Homework> Homeworks { get; set; }
}