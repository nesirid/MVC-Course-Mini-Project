using CourseManagement.Models;
using CourseManagement.Services.Interfaces;
using CourseManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }
        public async Task<IActionResult> Index()
        {
            var abouts = await _aboutService.GetAllAsync();
            return View(abouts);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutVM viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.ImageFile != null)
                {
                    string fileName = $"{Guid.NewGuid()}_{Path.GetFileName(viewModel.ImageFile.FileName)}";
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/img", fileName);

                    try
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await viewModel.ImageFile.CopyToAsync(stream);
                        }

                        viewModel.ImageUrl = "/assets/img/" + fileName;
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty, "An error occurred while saving the file.");
                        return View(viewModel);
                    }
                }

                var about = new About
                {
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    ImageUrl = viewModel.ImageUrl
                };

                await _aboutService.AddAsync(about);
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var about = await _aboutService.GetByIdAsync(id);
            if (about == null)
            {
                return NotFound();
            }

            var viewModel = new AboutVM
            {
                Id = about.Id,
                Title = about.Title,
                Description = about.Description,
                ImageUrl = about.ImageUrl
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AboutVM viewModel)
        {
            if (id != viewModel.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                if (viewModel.ImageFile != null)
                {
                    string fileName = $"{Guid.NewGuid()}_{Path.GetFileName(viewModel.ImageFile.FileName)}";
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/img", fileName);

                    try
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await viewModel.ImageFile.CopyToAsync(stream);
                        }

                        viewModel.ImageUrl = "/assets/img/" + fileName;
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty, "An error occurred while saving the file.");
                        return View(viewModel);
                    }
                }

                var about = new About
                {
                    Id = viewModel.Id,
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    ImageUrl = viewModel.ImageUrl
                };

                await _aboutService.UpdateAsync(about);
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _aboutService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
