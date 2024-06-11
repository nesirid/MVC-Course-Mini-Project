using System.ComponentModel.DataAnnotations;

namespace CourseManagement.Models
{
    public class ServiceItem : BaseEntity
    {
        public string? IconUrl { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
    }
}
