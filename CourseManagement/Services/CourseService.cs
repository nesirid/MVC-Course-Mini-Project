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
            try
            {
                _context.Courses.Add(course);
                await _context.SaveChangesAsync();
                Console.WriteLine($"Course {course.Title} saved to the database successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving course to the database: {ex.Message}");
            }
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
                await _context.SaveChangesAsync();

                Console.WriteLine($"Course with ID {id} was deleted from the database successfully.");
            }
            else
            {
                Console.WriteLine($"Course with ID {id} not found in the database.");
            }
        }
    }
}
