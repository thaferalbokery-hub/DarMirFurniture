using DarMirFurniture.Models;

namespace DarMirFurniture.Services;

public interface IBrandService
{
    Task<List<Brand>> GetAllAsync();
    Task<List<Brand>> GetActiveAsync();
    Task<Brand?> GetByIdAsync(int id);
    Task CreateAsync(Brand brand);
    Task UpdateAsync(Brand brand);
    Task DeleteAsync(int id);
}