using System.ComponentModel.DataAnnotations;

namespace CourseManagement.ViewModels
{
    public class CourseVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public IFormFile ImageFile { get; set; }
        public string? ImageUrl { get; set; }
        public int Duration { get; set; } 
        public int Rating { get; set; }
        [Required(ErrorMessage = "Instructor is required.")]
        public int InstructorId { get; set; }
        public int StudentCount { get; set; }
    }
}
