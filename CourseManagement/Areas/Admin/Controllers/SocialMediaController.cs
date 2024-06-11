using CourseManagement.Models;
using CourseManagement.Services.Interfaces;
using CourseManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CourseManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SocialMediaController : Controller
    {
        private readonly ISocialMediaService _socialMediaService;

        public SocialMediaController(ISocialMediaService socialMediaService)
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
                IconUrl = sm.Icon,
                Url = sm.Url
            }).ToList();
            return View(socialMediaVMs);
        }

        public IActionResult Create()
        {
            ViewData["Action"] = "Create";
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
                    var fileName = Path.GetFileName(socialMediaVM.IconFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/icons", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await socialMediaVM.IconFile.CopyToAsync(stream);
                    }

                    iconUrl = $"/assets/icons/{fileName}";
                }

                var socialMedia = new SocialMedia
                {
                    Name = socialMediaVM.Name,
                    Icon = iconUrl,
                    Url = socialMediaVM.Url
                };

                await _socialMediaService.AddAsync(socialMedia);
                return RedirectToAction(nameof(Index));
            }

            ViewData["Action"] = "Create";
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
                IconUrl = socialMedia.Icon,
                Url = socialMedia.Url
            };

            ViewData["Action"] = "Edit";
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

                socialMedia.Name = socialMediaVM.Name;
                socialMedia.Url = socialMediaVM.Url;

                if (socialMediaVM.IconFile != null)
                {
                    var fileName = Path.GetFileName(socialMediaVM.IconFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/icons", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await socialMediaVM.IconFile.CopyToAsync(stream);
                    }

                    socialMedia.Icon = $"/assets/icons/{fileName}";
                }

                await _socialMediaService.UpdateAsync(socialMedia);
                return RedirectToAction(nameof(Index));
            }

            ViewData["Action"] = "Edit";
            return View(socialMediaVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var socialMedia = await _socialMediaService.GetByIdAsync(id);
            if (socialMedia == null)
            {
                return NotFound();
            }

            await _socialMediaService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
