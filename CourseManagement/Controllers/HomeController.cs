using CourseManagement.Services.Interfaces;
using CourseManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ICategoryService _categoryService;
        private readonly IInstructorService _instructorService;
        private readonly ISliderService _sliderService;
        private readonly IServiceItemService _serviceItemService;
        private readonly IAboutService _aboutService;
        private readonly ITestimonialService _testimonialService;


        public HomeController(ICourseService courseService,
                              ICategoryService categoryService,
                              IInstructorService instructorService,
                              ISliderService sliderService,
                              IServiceItemService serviceItemService,
                              IAboutService aboutService,
                              ITestimonialService testimonialService)
        {
            _courseService = courseService;
            _categoryService = categoryService;
            _instructorService = instructorService;
            _sliderService = sliderService;
            _serviceItemService = serviceItemService;
            _aboutService = aboutService;
            _testimonialService = testimonialService;
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
                Instructors = instructorVMs,
                Sliders = await _sliderService.GetAllAsync(),
                ServiceItems = await _serviceItemService.GetAllAsync(),
                Abouts = await _aboutService.GetAllAsync(),
                Testimonials = await _testimonialService.GetAllAsync(),


            };

            return View(viewModel);
        }
    }
}
