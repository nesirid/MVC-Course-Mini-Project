using CourseManagement.Data;
using CourseManagement.Models;
using CourseManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseManagement.Services
{
    public class TestimonialService : ITestimonialService
    {
        private readonly AppDbContext _context;

        public TestimonialService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Testimonial>> GetAllAsync()
        {
            return await _context.Testimonials.ToListAsync();
        }

        public async Task<Testimonial> GetByIdAsync(int id)
        {
            return await _context.Testimonials.FindAsync(id);
        }

        public async Task AddAsync(Testimonial testimonial)
        {
            _context.Testimonials.Add(testimonial);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Testimonial testimonial)
        {
            _context.Testimonials.Update(testimonial);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var testimonial = await _context.Testimonials.FindAsync(id);
            if (testimonial != null)
            {
                _context.Testimonials.Remove(testimonial);
                await _context.SaveChangesAsync();
            }
        }
    }
}
