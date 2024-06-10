using Microsoft.AspNetCore.Mvc;
using CourseManagement.Models;
using CourseManagement.Services.Interfaces;
using CourseManagement.ViewModels;

namespace CourseManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InstructorController : Controller
    {
        private readonly IInstructorService _instructorService;

        public InstructorController(IInstructorService instructorService)
        {
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
            return View(instructorVMs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InstructorVM instructorVM)
        {
            if (ModelState.IsValid)
            {
                string photoUrl = null;

                if (instructorVM.PhotoFile != null)
                {
                    var fileName = Path.GetFileName(instructorVM.PhotoFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/img", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await instructorVM.PhotoFile.CopyToAsync(stream);
                    }

                    photoUrl = $"/assets/img/{fileName}";
                }

                var instructor = new Instructor
                {
                    Name = instructorVM.Name,
                    PhotoUrl = photoUrl,
                    SocialMedia1 = instructorVM.SocialMedia1,
                    SocialMedia2 = instructorVM.SocialMedia2,
                    SocialMedia3 = instructorVM.SocialMedia3,
                    Expertise = instructorVM.Expertise
                };
                await _instructorService.AddAsync(instructor);
                return RedirectToAction(nameof(Index));
            }
            return View(instructorVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var instructor = await _instructorService.GetByIdAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }

            var instructorVM = new InstructorVM
            {
                Id = instructor.Id,
                Name = instructor.Name,
                PhotoUrl = instructor.PhotoUrl,
                SocialMedia1 = instructor.SocialMedia1,
                SocialMedia2 = instructor.SocialMedia2,
                SocialMedia3 = instructor.SocialMedia3,
                Expertise = instructor.Expertise
            };

            return View(instructorVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, InstructorVM instructorVM)
        {
            if (id != instructorVM.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var instructor = await _instructorService.GetByIdAsync(id);
                if (instructor == null)
                {
                    return NotFound();
                }

                instructor.Name = instructorVM.Name;
                instructor.SocialMedia1 = instructorVM.SocialMedia1;
                instructor.SocialMedia2 = instructorVM.SocialMedia2;
                instructor.SocialMedia3 = instructorVM.SocialMedia3;
                instructor.Expertise = instructorVM.Expertise;

                if (instructorVM.PhotoFile != null)
                {
                    var fileName = Path.GetFileName(instructorVM.PhotoFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/img", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await instructorVM.PhotoFile.CopyToAsync(stream);
                    }
                    instructor.PhotoUrl = $"/assets/img/{fileName}";
                }
                await _instructorService.UpdateAsync(instructor);
                return RedirectToAction(nameof(Index));
            }
            return View(instructorVM);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _instructorService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}