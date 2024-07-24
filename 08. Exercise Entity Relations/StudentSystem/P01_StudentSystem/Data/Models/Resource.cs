using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace P01_StudentSystem.Data.Models;

public class Resource
{
    [Key]   
    public int ResourceId { get; set; }

    [Unicode]
    [Required] // = null!;
    [MaxLength(50)]
    public string Name { get; set; }

    [Unicode(false)]
    public string Url { get; set; } 
    public ResourceType ResourceType { get; set; }
    public int CourseId { get; set; }

    [ForeignKey(nameof(CourseId))]
    public Course Course { get; set; }
}

public enum ResourceType
{
    Video,
    Presentation,
    Document,
    Other
}