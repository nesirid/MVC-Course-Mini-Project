using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CourseManagement.ViewModels
{
    public class AboutVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Image is required.")]
        public IFormFile ImageFile { get; set; }
        public string? ImageUrl { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
    }
}
