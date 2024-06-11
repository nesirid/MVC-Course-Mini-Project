using System.ComponentModel.DataAnnotations;

namespace CourseManagement.Models
{
    public class About : BaseEntity
    {
        [Required]
        public string? ImageUrl { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
    }
}
