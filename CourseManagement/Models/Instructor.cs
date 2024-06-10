using System.ComponentModel.DataAnnotations;

namespace CourseManagement.Models
{
    public class Instructor : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string? PhotoUrl { get; set; }
        public string? SocialMedia1 { get; set; }
        public string? SocialMedia2 { get; set; }
        public string? SocialMedia3 { get; set; }
        public string? Expertise { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}