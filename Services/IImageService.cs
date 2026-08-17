namespace DarMirFurniture.Services;

public interface IImageService
{
    Task<string> UploadImageAsync(IFormFile file, string folder = "products");
    Task DeleteImageAsync(string imageUrl);
    bool IsValidImage(IFormFile file);
}