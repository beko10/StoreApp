using Microsoft.AspNetCore.Http;
using StoreApp.Business.Abstract;

namespace StoreApp.Business.Concerete;

public class FileManager : IFileService
{
    // Dosyaların yükleneceği temel dizin yolu.
    // Bu yol, projenin "wwwroot/uploads" klasörüne işaret eder.
    private readonly string _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

    // Constructor: FileManager sınıfı örneği oluşturulduğunda çalışır.
    public FileManager()
    {
        // Eğer upload dizini yoksa, bu dizini oluşturur.
        if (!Directory.Exists(_uploadPath))
        {
            Directory.CreateDirectory(_uploadPath);
        }
    }

    // Ürün resmi yükleme işlemini gerçekleştiren metot.
    public async Task<string> UploadProductImageFileAsync(IFormFile file)
    {
        // Dosya adını benzersiz hale getirmek için GUID kullanılır.
        // Dosya adına orijinal dosya adı da eklenir.
        var fileName = $"{Guid.NewGuid().ToString()}_{file.FileName}";

        // Dosyanın tam yolu oluşturulur.
        var filePath = Path.Combine(_uploadPath, fileName);

        // Dosya, belirtilen yola asenkron olarak kaydedilir.
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Yüklenen dosyanın adı geri döndürülür.
        return $"/images/{fileName}";
    }

    // Ürün resmi silme işlemini gerçekleştiren metot.
    public bool DeleteProductImageFile(string fileName)
    {
        // Dosyanın tam yolu oluşturulur.
        var filePath = Path.Combine(_uploadPath, fileName);

        // Eğer dosya varsa, silinir ve true döner.
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            return true;
        }

        // Dosya yoksa, false döner.
        return false;
    }

    // Ürün resmi güncelleme işlemini gerçekleştiren metot.
    public async Task<string> UpdateProductImageFile(IFormFile file, string oldFileName)
    {
        // Eski dosya adı boş değilse, eski dosyayı sil.
        if (!string.IsNullOrEmpty(oldFileName))
        {
            DeleteProductImageFile(oldFileName);
        }

        // Yeni dosyayı yükle ve yeni dosya adını döndür.
        return await UploadProductImageFileAsync(file);
    }
}