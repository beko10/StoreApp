using Microsoft.AspNetCore.Http;

namespace StoreApp.Business.Abstract;

public interface IFileService
{
    Task<string> UploadProductImageFileAsync(IFormFile file);
    Task<string> UpdateProductImageFile(IFormFile file, string oldFileName);
    bool DeleteProductImageFile(string fileName);
}