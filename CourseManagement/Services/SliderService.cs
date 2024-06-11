using CourseManagement.Data;
using CourseManagement.Models;
using CourseManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

public class SliderService : ISliderService
{
    private readonly AppDbContext _context;

    public SliderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Slider>> GetAllAsync()
    {
        return await _context.Sliders.ToListAsync();
    }

    public async Task<Slider> GetByIdAsync(int id)
    {
        return await _context.Sliders.FindAsync(id);
    }

    public async Task AddAsync(Slider slider)
    {
        _context.Sliders.Add(slider);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Slider slider)
    {
        _context.Sliders.Update(slider);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var slider = await _context.Sliders.FindAsync(id);
        if (slider != null)
        {
            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
        }
    }
}