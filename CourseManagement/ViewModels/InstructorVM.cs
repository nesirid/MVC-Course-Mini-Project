using System.ComponentModel.DataAnnotations;

namespace CourseManagement.ViewModels
{
    public class InstructorVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        public string? PhotoUrl { get; set; }
        [Display(Name = "Photo")]
        public IFormFile? PhotoFile { get; set; }
        public string? SocialMedia1 { get; set; }
        public string? SocialMedia2 { get; set; }
        public string? SocialMedia3 { get; set; }
        public string? Expertise { get; set; }
    }
}