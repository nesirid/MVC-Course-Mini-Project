using System.ComponentModel.DataAnnotations;

namespace CourseManagement.ViewModels
{
    public class SocialMediaVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? IconUrl { get; set; }
        public string? Url { get; set; }
        [Display(Name = "Icon")]
        public IFormFile? IconFile { get; set; }
    }
}
