namespace StoreApp.Business.Validation.Helper;

public static class FileExtensionsHelper
{
    public static bool IsValidImageExtension(string? fileName)
    {
        if (string.IsNullOrEmpty(fileName)) return false;

        // Supported image extensions
        string[] validExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

        // Check the extension
        return validExtensions.Any(ext => fileName.ToLower().EndsWith(ext));
    }
}