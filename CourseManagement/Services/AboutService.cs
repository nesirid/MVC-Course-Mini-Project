using CourseManagement.Data;
using CourseManagement.Models;
using CourseManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Services
{
    public class AboutService : IAboutService
    {
        private readonly AppDbContext _context;

        public AboutService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<About>> GetAllAsync()
        {
            return await _context.Abouts.ToListAsync();
        }

        public async Task<About> GetByIdAsync(int id)
        {
            return await _context.Abouts.FindAsync(id);
        }

        public async Task AddAsync(About about)
        {
            _context.Abouts.Add(about);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(About about)
        {
            _context.Abouts.Update(about);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var about = await _context.Abouts.FindAsync(id);
            if (about != null)
            {
                _context.Abouts.Remove(about);
                await _context.SaveChangesAsync();
            }
        }
    }
}
