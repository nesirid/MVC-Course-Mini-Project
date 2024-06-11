using CourseManagement.Models;

namespace CourseManagement.Services.Interfaces
{
    public interface IServiceItemService
    {
        Task<IEnumerable<ServiceItem>> GetAllAsync();
        Task<ServiceItem> GetByIdAsync(int id);
        Task AddAsync(ServiceItem serviceItem);
        Task UpdateAsync(ServiceItem serviceItem);
        Task DeleteAsync(int id);
    }
}
