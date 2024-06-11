using Microsoft.AspNetCore.Http;

namespace CourseManagement.ViewModels
{
    public class TestimonialVM
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string Profession { get; set; }
        public string Text { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
