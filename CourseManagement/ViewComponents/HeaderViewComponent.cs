using CourseManagement.Models;
using CourseManagement.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.ViewComponents
{
    [ViewComponent(Name = "HeaderViewComponent")]
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;
        private readonly IHttpContextAccessor _accessor;
        private readonly UserManager<AppUser> _userManager;

        public HeaderViewComponent(ISettingService settingService,
                                   IHttpContextAccessor accessor,
                                   UserManager<AppUser> userManager)
        {
            _settingService = settingService;
            _accessor = accessor;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            AppUser user = new();

            if (User.Identity.Name != null)
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
            }

            Dictionary<string, string> settings = await _settingService.GetAllAsync();

            HeaderVM response = new()
            {
                Settings = settings,
                UserFullName = user.FullName
            };

            return View(response);
        }
    }

    public class HeaderVM
    {
        public Dictionary<string, string> Settings { get; set; }
        public string UserFullName { get; set; }
    }




}
