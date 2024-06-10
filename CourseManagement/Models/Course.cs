using CourseManagement.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagement.Models
{
    public class Course : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public string? ImageUrl { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public int InstructorId { get; set; }
        public int StudentCount { get; set; }
        public Category Category { get; set; }
        public Instructor Instructor { get; set; }

    }
}