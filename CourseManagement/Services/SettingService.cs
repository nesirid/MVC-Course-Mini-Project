using CourseManagement.Services.Interfaces;
using CourseManagement.ViewComponents;

namespace CourseManagement.Services
{
    public class SettingService : ISettingService
    {
        public Task<Dictionary<string, string>> GetAllAsync()
        {
            var settings = new Dictionary<string, string>
            {
                { "Setting1", "Value1" },
                { "Setting2", "Value2" }
            };
            return Task.FromResult(settings);
        }
    }
}
