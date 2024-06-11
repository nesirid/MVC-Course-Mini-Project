using CourseManagement.Models;

namespace CourseManagement.Services.Interfaces
{
    public interface ITestimonialService
    {
        Task<IEnumerable<Testimonial>> GetAllAsync();
        Task<Testimonial> GetByIdAsync(int id);
        Task AddAsync(Testimonial testimonial);
        Task UpdateAsync(Testimonial testimonial);
        Task DeleteAsync(int id);
    }
}
