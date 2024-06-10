using CourseManagement.Services.Interfaces;
using CourseManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ICategoryService _categoryService;
        private readonly IInstructorService _instructorService;

        public HomeController(ICourseService courseService,
                              ICategoryService categoryService,
                              IInstructorService instructorService)
        {
            _courseService = courseService;
            _categoryService = categoryService;
            _instructorService = instructorService;
        }

        public async Task<IActionResult> Index()
        {
            var instructors = await _instructorService.GetAllAsync();
            var instructorVMs = instructors.Select(i => new InstructorVM
            {
                Id = i.Id,
                Name = i.Name,
                PhotoUrl = i.PhotoUrl,
                SocialMedia1 = i.SocialMedia1,
                SocialMedia2 = i.SocialMedia2,
                SocialMedia3 = i.SocialMedia3,
                Expertise = i.Expertise
            }).ToList();

            var viewModel = new HomeVM
            {
                Categories = await _categoryService.GetAllCategoriesAsync(),
                Courses = await _courseService.GetAllCoursesAsync(),
                Instructors = instructorVMs
            };

            return View(viewModel);
        }
    }
}
