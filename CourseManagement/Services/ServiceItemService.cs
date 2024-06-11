using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseManagement.Data;
using CourseManagement.Models;
using CourseManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Services
{
    public class ServiceItemService : IServiceItemService
    {
        private readonly AppDbContext _context;

        public ServiceItemService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServiceItem>> GetAllAsync()
        {
            return await _context.ServiceItems.ToListAsync();
        }

        public async Task<ServiceItem> GetByIdAsync(int id)
        {
            return await _context.ServiceItems.FindAsync(id);
        }

        public async Task AddAsync(ServiceItem serviceItem)
        {
            _context.ServiceItems.Add(serviceItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ServiceItem serviceItem)
        {
            _context.ServiceItems.Update(serviceItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var serviceItem = await _context.ServiceItems.FindAsync(id);
            if (serviceItem != null)
            {
                _context.ServiceItems.Remove(serviceItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
