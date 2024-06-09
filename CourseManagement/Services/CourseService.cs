using CourseManagement.Data;
using CourseManagement.Models;
using CourseManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseManagement.Services
{
    public class CourseService : ICourseService
    {
        private readonly AppDbContext _context;

        public CourseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses.Include(c => c.Category).ToListAsync();
        }

        public async Task AddCourseAsync(Course course)
        {
            _context.Courses.Add(course);
            var category = await _context.Categories.Include(c => c.Courses).FirstOrDefaultAsync(c => c.Id == course.CategoryId);
            if (category != null)
            {
                category.Courses.Add(course);
            }
            await _context.SaveChangesAsync();
        }


        public async Task<Course> GetCourseByIdAsync(int id)
        {
            return await _context.Courses.Include(c => c.Category).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateCourseAsync(Course course)
        {
            _context.Entry(course).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCourseAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                var category = await _context.Categories.Include(c => c.Courses).FirstOrDefaultAsync(c => c.Id == course.CategoryId);
                if (category != null)
                {
                    category.Courses.Remove(course);
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
