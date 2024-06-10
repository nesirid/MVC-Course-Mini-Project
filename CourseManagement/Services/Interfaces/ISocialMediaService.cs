using CourseManagement.Models;

namespace CourseManagement.Services.Interfaces
{
    public interface ISocialMediaService
    {
        Task<IEnumerable<SocialMedia>> GetAllAsync();
        Task<SocialMedia> GetByIdAsync(int id);
        Task AddAsync(SocialMedia socialMedia);
        Task UpdateAsync(SocialMedia socialMedia);
        Task DeleteAsync(int id);
    }
}
