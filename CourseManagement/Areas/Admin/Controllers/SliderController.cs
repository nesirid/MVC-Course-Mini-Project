using CourseManagement.Models;
using CourseManagement.Services.Interfaces;
using CourseManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }
        public async Task<IActionResult> Index()
        {
            var sliders = await _sliderService.GetAllAsync();
            return View(sliders);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderVM viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.ImageFile != null)
                {
                    string fileName = Path.GetFileName(viewModel.ImageFile.FileName);
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

                var slider = new Slider
                {
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    ImageUrl = viewModel.ImageUrl
                };

                await _sliderService.AddAsync(slider);
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var slider = await _sliderService.GetByIdAsync(id);
            if (slider == null)
            {
                return NotFound();
            }

            var viewModel = new SliderVM
            {
                Id = slider.Id,
                Title = slider.Title,
                Description = slider.Description,
                ImageUrl = slider.ImageUrl
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SliderVM viewModel)
        {
            if (id != viewModel.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                if (viewModel.ImageFile != null)
                {
                    string fileName = Path.GetFileName(viewModel.ImageFile.FileName);
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

                var slider = new Slider
                {
                    Id = viewModel.Id,
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    ImageUrl = viewModel.ImageUrl
                };

                await _sliderService.UpdateAsync(slider);
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _sliderService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
