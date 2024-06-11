using System.ComponentModel.DataAnnotations;

namespace CourseManagement.Models
{
    public class Testimonial : BaseEntity
    {
        [Required]
        public string ClientName { get; set; }
        [Required]
        public string Profession { get; set; }
        [Required]
        public string Text { get; set; }
        public string? ImageUrl { get; set; }
    }
}
