using CourseManagement.Models;
using CourseManagement.Services.Interfaces;
using CourseManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CourseManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceItemController : Controller
    {
        private readonly IServiceItemService _serviceItemService;

        public ServiceItemController(IServiceItemService serviceItemService)
        {
            _serviceItemService = serviceItemService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _serviceItemService.GetAllAsync();
            return View(items);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceItemVM viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.IconFile != null)
                {
                    string fileName = Path.GetFileName(viewModel.IconFile.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/icons", fileName);

                    try
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await viewModel.IconFile.CopyToAsync(stream);
                        }

                        viewModel.IconUrl = "/assets/icons/" + fileName;
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty, "An error occurred while saving the file.");
                        return View(viewModel);
                    }
                }

                var serviceItem = new ServiceItem
                {
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    IconUrl = viewModel.IconUrl
                };

                await _serviceItemService.AddAsync(serviceItem);
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var serviceItem = await _serviceItemService.GetByIdAsync(id);
            if (serviceItem == null)
            {
                return NotFound();
            }

            var viewModel = new ServiceItemVM
            {
                Id = serviceItem.Id,
                Title = serviceItem.Title,
                Description = serviceItem.Description,
                IconUrl = serviceItem.IconUrl
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ServiceItemVM viewModel)
        {
            if (id != viewModel.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                if (viewModel.IconFile != null)
                {
                    string fileName = Path.GetFileName(viewModel.IconFile.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/icons", fileName);

                    try
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await viewModel.IconFile.CopyToAsync(stream);
                        }

                        viewModel.IconUrl = "/assets/icons/" + fileName;
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty, "An error occurred while saving the file.");
                        return View(viewModel);
                    }
                }

                var serviceItem = new ServiceItem
                {
                    Id = viewModel.Id,
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    IconUrl = viewModel.IconUrl
                };

                await _serviceItemService.UpdateAsync(serviceItem);
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _serviceItemService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
