using CourseManagement.Models;

namespace CourseManagement.Services.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task AddCourseAsync(Course course);
        Task<Course> GetCourseByIdAsync(int id);
        Task UpdateCourseAsync(Course course);
        Task DeleteCourseAsync(int id);
    }
}
