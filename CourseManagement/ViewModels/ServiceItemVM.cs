using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CourseManagement.ViewModels
{
    public class ServiceItemVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Icon is required.")]
        public IFormFile IconFile { get; set; }
        public string? IconUrl { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
    }
}
