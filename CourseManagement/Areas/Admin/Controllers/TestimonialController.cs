using CourseManagement.Models;
using CourseManagement.Services.Interfaces;
using CourseManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TestimonialController : Controller
    {
        private readonly ITestimonialService _testimonialService;

        public TestimonialController(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }

        public async Task<IActionResult> Index()
        {
            var testimonials = await _testimonialService.GetAllAsync();
            return View(testimonials);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TestimonialVM viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.ImageFile != null)
                {
                    string fileName = Path.GetFileName(viewModel.ImageFile.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/img", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await viewModel.ImageFile.CopyToAsync(stream);
                    }

                    viewModel.ImageUrl = "/assets/img/" + fileName;
                }

                var testimonial = new Testimonial
                {
                    ClientName = viewModel.ClientName,
                    Profession = viewModel.Profession,
                    Text = viewModel.Text,
                    ImageUrl = viewModel.ImageUrl
                };

                await _testimonialService.AddAsync(testimonial);
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var testimonial = await _testimonialService.GetByIdAsync(id);
            if (testimonial == null)
            {
                return NotFound();
            }

            var viewModel = new TestimonialVM
            {
                Id = testimonial.Id,
                ClientName = testimonial.ClientName,
                Profession = testimonial.Profession,
                Text = testimonial.Text,
                ImageUrl = testimonial.ImageUrl
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TestimonialVM viewModel)
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

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await viewModel.ImageFile.CopyToAsync(stream);
                    }

                    viewModel.ImageUrl = "/assets/img/" + fileName;
                }

                var testimonial = new Testimonial
                {
                    Id = viewModel.Id,
                    ClientName = viewModel.ClientName,
                    Profession = viewModel.Profession,
                    Text = viewModel.Text,
                    ImageUrl = viewModel.ImageUrl
                };

                await _testimonialService.UpdateAsync(testimonial);
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _testimonialService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
