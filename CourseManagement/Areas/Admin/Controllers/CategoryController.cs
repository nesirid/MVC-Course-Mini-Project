using CourseManagement.Models;
using CourseManagement.Services.Interfaces;
using CourseManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace CourseManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            var categoryVMs = categories.Select(c => new CategoryVM
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                ImagePath = c.Image,
                CourseCount = c.Courses.Count
            }).ToList();
            return View(categoryVMs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryVM categoryVM)
        {
            if (ModelState.IsValid)
            {
                string imagePath = null;

                if (categoryVM.ImageFile != null)
                {
                    var fileName = Path.GetFileName(categoryVM.ImageFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/img", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await categoryVM.ImageFile.CopyToAsync(stream);
                    }

                    imagePath = $"/assets/img/{fileName}";
                }

                var category = new Category
                {
                    Name = categoryVM.Name,
                    Description = categoryVM.Description,
                    Image = imagePath
                };

                await _categoryService.AddCategoryAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(categoryVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var categoryVM = new CategoryVM
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                ImagePath = category.Image
            };

            return View(categoryVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryVM categoryVM)
        {
            if (id != categoryVM.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    return NotFound();
                }

                category.Name = categoryVM.Name;
                category.Description = categoryVM.Description;

                if (categoryVM.ImageFile != null)
                {
                    var fileName = Path.GetFileName(categoryVM.ImageFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/img", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await categoryVM.ImageFile.CopyToAsync(stream);
                    }

                    category.Image = $"/assets/img/{fileName}";
                }

                await _categoryService.UpdateCategoryAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(categoryVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await _categoryService.DeleteCategoryAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
