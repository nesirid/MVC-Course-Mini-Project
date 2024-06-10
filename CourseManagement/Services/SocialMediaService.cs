using CourseManagement.Data;
using CourseManagement.Models;
using CourseManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Services
{
        public class SocialMediaService : ISocialMediaService
        {
            private readonly AppDbContext _context;

            public SocialMediaService(AppDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<SocialMedia>> GetAllAsync()
            {
                return await _context.SocialMedias.ToListAsync();
            }

            public async Task<SocialMedia> GetByIdAsync(int id)
            {
                return await _context.SocialMedias.FindAsync(id);
            }

            public async Task AddAsync(SocialMedia socialMedia)
            {
                _context.SocialMedias.Add(socialMedia);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(SocialMedia socialMedia)
            {
                _context.SocialMedias.Update(socialMedia);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(int id)
            {
                var socialMedia = await _context.SocialMedias.FindAsync(id);
                if (socialMedia != null)
                {
                    _context.SocialMedias.Remove(socialMedia);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
