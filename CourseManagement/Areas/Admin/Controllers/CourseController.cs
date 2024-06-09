using CourseManagement.Models;
using CourseManagement.Services.Interfaces;
using CourseManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseManagement.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ICategoryService _categoryService;

        public CourseController(ICourseService courseService, ICategoryService categoryService)
        {
            _courseService = courseService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return View(courses);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["Categories"] = new SelectList(await _categoryService.GetAllCategoriesAsync(), "Id", "Name");
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
                ImageUrl = course.ImageUrl
            };

            ViewData["Categories"] = new SelectList(await _categoryService.GetAllCategoriesAsync(), "Id", "Name", courseVM.CategoryId);
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

            await _courseService.UpdateCourseAsync(course);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Console.WriteLine($"Attempting to delete course with ID {id}");
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
            {
                Console.WriteLine($"Course with ID {id} not found");
                return NotFound();
            }

            await _courseService.DeleteCourseAsync(id);

            Console.WriteLine($"Course with ID {id} was deleted.");
            return RedirectToAction(nameof(Index));
        }







    }
}
