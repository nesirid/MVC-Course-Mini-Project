using System.ComponentModel.DataAnnotations;

namespace CourseManagement.Models
{
    public class SocialMedia : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Icon { get; set; }
        [Required]
        public string Url { get; set; }
    }
}
