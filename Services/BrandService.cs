using DarMirFurniture.Data;
using DarMirFurniture.Models;
using Microsoft.EntityFrameworkCore;

namespace DarMirFurniture.Services;

public class BrandService : IBrandService
{
    private readonly ApplicationDbContext _context;

    public BrandService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Brand>> GetAllAsync()
    {
        return await _context.Brands.OrderBy(b => b.Name).ToListAsync();
    }

    public async Task<List<Brand>> GetActiveAsync()
    {
        return await _context.Brands.Where(b => b.IsActive).OrderBy(b => b.Name).ToListAsync();
    }

    public async Task<Brand?> GetByIdAsync(int id)
    {
        return await _context.Brands.FindAsync(id);
    }

    public async Task CreateAsync(Brand brand)
    {
        brand.CreatedAt = DateTime.UtcNow;
        _context.Brands.Add(brand);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Brand brand)
    {
        _context.Brands.Update(brand);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var brand = await _context.Brands.FindAsync(id);
        if (brand != null)
        {
            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
        }
    }
}