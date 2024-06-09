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
    }
}
