using CourseManagement.Models;

namespace CourseManagement.Services.Interfaces
{
    public interface IAboutService
    {
        Task<IEnumerable<About>> GetAllAsync();
        Task<About> GetByIdAsync(int id);
        Task AddAsync(About about);
        Task UpdateAsync(About about);
        Task DeleteAsync(int id);
    }
}
