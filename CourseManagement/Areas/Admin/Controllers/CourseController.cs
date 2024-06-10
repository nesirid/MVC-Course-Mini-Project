using CourseManagement.Models;
using CourseManagement.Services.Interfaces;
using CourseManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ICategoryService _categoryService;
        private readonly IInstructorService _instructorService;

        public CourseController(ICourseService courseService,
                                ICategoryService categoryService,
                                IInstructorService instructorService)
        {
            _courseService = courseService;
            _categoryService = categoryService;
            _instructorService = instructorService;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return View(courses);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["Categories"] = new SelectList(await _categoryService.GetAllCategoriesAsync(), "Id", "Name");
            ViewData["Instructors"] = new SelectList(await _instructorService.GetAllAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseVM courseVM)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                ViewData["Categories"] = new SelectList(await _categoryService.GetAllCategoriesAsync(), "Id", "Name", courseVM.CategoryId);
                ViewData["Instructors"] = new SelectList(await _instructorService.GetAllAsync(), "Id", "Name", courseVM.InstructorId);
                return View(courseVM);
            }

            string imageUrl = null;

            if (courseVM.ImageFile != null && courseVM.ImageFile.Length > 0)
            {
                var fileName = Path.GetFileName(courseVM.ImageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/img", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await courseVM.ImageFile.CopyToAsync(stream);
                }

                imageUrl = $"/assets/img/{fileName}";
            }

            var course = new Course
            {
                Title = courseVM.Title,
                Description = courseVM.Description,
                Price = courseVM.Price,
                CategoryId = courseVM.CategoryId,
                Duration = courseVM.Duration,
                Rating = courseVM.Rating,
                InstructorId = courseVM.InstructorId,
                StudentCount = courseVM.StudentCount,
                ImageUrl = imageUrl
            };

            await _courseService.AddCourseAsync(course);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            var courseVM = new CourseVM
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Price = course.Price,
                CategoryId = course.CategoryId,
                Duration = course.Duration,
                Rating = course.Rating,
                InstructorId = course.InstructorId,
                StudentCount = course.StudentCount,
                ImageUrl = course.ImageUrl
            };

            ViewData["Categories"] = new SelectList(await _categoryService.GetAllCategoriesAsync(), "Id", "Name", courseVM.CategoryId);
            ViewData["Instructors"] = new SelectList(await _instructorService.GetAllAsync(), "Id", "Name", courseVM.InstructorId);
            return View(courseVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseVM courseVM)
        {
            if (id != courseVM.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                ViewData["Categories"] = new SelectList(await _categoryService.GetAllCategoriesAsync(), "Id", "Name", courseVM.CategoryId);
                ViewData["Instructors"] = new SelectList(await _instructorService.GetAllAsync(), "Id", "Name", courseVM.InstructorId);
                return View(courseVM);
            }

            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            if (courseVM.ImageFile != null && courseVM.ImageFile.Length > 0)
            {
                var fileName = Path.GetFileName(courseVM.ImageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/img", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await courseVM.ImageFile.CopyToAsync(stream);
                }

                course.ImageUrl = $"/assets/img/{fileName}";
            }

            course.Title = courseVM.Title;
            course.Description = courseVM.Description;
            course.Price = courseVM.Price;
            course.CategoryId = courseVM.CategoryId;
            course.Duration = courseVM.Duration;
            course.Rating = courseVM.Rating;
            course.InstructorId = courseVM.InstructorId;
            course.StudentCount = courseVM.StudentCount;

            await _courseService.UpdateCourseAsync(course);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            await _courseService.DeleteCourseAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
