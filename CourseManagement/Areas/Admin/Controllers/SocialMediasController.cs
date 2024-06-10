using Microsoft.AspNetCore.Mvc;
using CourseManagement.Models;
using CourseManagement.Services.Interfaces;
using CourseManagement.ViewModels;

namespace CourseManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SocialMediasController : Controller
    {
        private readonly ISocialMediaService _socialMediaService;

        public SocialMediasController(ISocialMediaService socialMediaService)
        {
            _socialMediaService = socialMediaService;
        }

        public async Task<IActionResult> Index()
        {
            var socialMedias = await _socialMediaService.GetAllAsync();
            var socialMediaVMs = socialMedias.Select(sm => new SocialMediaVM
            {
                Id = sm.Id,
                Name = sm.Name,
                Icon = sm.Icon
            }).ToList();
            return View(socialMediaVMs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SocialMediaVM socialMediaVM)
        {
            if (ModelState.IsValid)
            {
                string iconUrl = null;

                if (socialMediaVM.IconFile != null)
                {
                    var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/icons");
                    if (!Directory.Exists(uploadsDir))
                    {
                        Directory.CreateDirectory(uploadsDir);
                    }

                    var fileName = Path.GetFileName(socialMediaVM.IconFile.FileName);
                    var filePath = Path.Combine(uploadsDir, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await socialMediaVM.IconFile.CopyToAsync(stream);
                    }

                    iconUrl = $"/assets/icons/{fileName}";
                }

                var socialMedia = new SocialMedia
                {
                    Name = socialMediaVM.Name,
                    Icon = iconUrl
                };

                await _socialMediaService.AddAsync(socialMedia);
                return RedirectToAction(nameof(Index));
            }
            return View(socialMediaVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var socialMedia = await _socialMediaService.GetByIdAsync(id);
            if (socialMedia == null)
            {
                return NotFound();
            }

            var socialMediaVM = new SocialMediaVM
            {
                Id = socialMedia.Id,
                Name = socialMedia.Name,
                Icon = socialMedia.Icon
            };

            return View(socialMediaVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SocialMediaVM socialMediaVM)
        {
            if (id != socialMediaVM.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var socialMedia = await _socialMediaService.GetByIdAsync(id);
                if (socialMedia == null)
                {
                    return NotFound();
                }

                string iconUrl = socialMedia.Icon;

                if (socialMediaVM.IconFile != null)
                {
                    var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/icons");
                    if (!Directory.Exists(uploadsDir))
                    {
                        Directory.CreateDirectory(uploadsDir);
                    }

                    var fileName = Path.GetFileName(socialMediaVM.IconFile.FileName);
                    var filePath = Path.Combine(uploadsDir, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await socialMediaVM.IconFile.CopyToAsync(stream);
                    }

                    iconUrl = $"/assets/icons/{fileName}";
                }

                socialMedia.Name = socialMediaVM.Name;
                socialMedia.Icon = iconUrl;

                await _socialMediaService.UpdateAsync(socialMedia);
                return RedirectToAction(nameof(Index));
            }
            return View(socialMediaVM);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _socialMediaService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
