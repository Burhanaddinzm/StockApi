using TestApi.Helpers.File;
using IO = System.IO;

namespace TestApi.Extensions.File;

/// <summary>
/// Extension methods for IFormFile.
/// </summary>
public static class FileExtension
{
    /// <summary>
    /// Saves the uploaded file to the specified location.
    /// </summary>
    /// <param name="file">The file to save.</param>
    /// <param name="root">The root directory to save the file to.</param>
    /// <param name="folders">Additional folders to save the file to.</param>
    /// <returns>The unique file name.</returns>
    public static async Task<string> SaveFileAsync(this IFormFile file, string root, params string[] folders)
    {
        string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
        string[] pathParts = folders.Prepend(root).Append(uniqueFileName).ToArray();
        string path = Path.Combine(pathParts);

        using FileStream fs = new FileStream(path, FileMode.Create);

        await file.CopyToAsync(fs);

        return uniqueFileName;
    }

    /// <summary>
    /// Checks if the file type is valid.
    /// </summary>
    /// <param name="file">The file to check.</param>
    /// <param name="fileType">The valid file type.</param>
    /// <returns>True if the file type is valid, false otherwise.</returns>
    public static bool CheckFileType(this IFormFile file, string fileType)
    {
        if (file.ContentType.Contains(fileType))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Checks if the file size is within the specified limit.
    /// </summary>
    /// <param name="file">The file to check.</param>
    /// <param name="megabytes">The maximum file size in megabytes.</param>
    /// <returns>True if the file size is within the limit, false otherwise.</returns>
    public static bool CheckFileSize(this IFormFile file, int megabytes)
    {
        if (file.Length / 1024 / 1024 >= megabytes)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Deletes the file from the specified location.
    /// </summary>
    /// <param name="file">The file to delete.</param>
    /// <param name="root">The root directory of the file.</param>
    /// <param name="fileLocation">The additional directories of the file.</param>
    public static void DeleteFile(this IFormFile file, string root, params string[] fileLocation)
    {
        string[] pathParts = fileLocation.Prepend(root).ToArray();
        string path = Path.Combine(pathParts);

        if (IO.File.Exists(path))
        {
            IO.File.Delete(path);
        }
    }

    /// <summary>
    /// Validates the file.
    /// </summary>
    /// <param name="file">The file to validate.</param>
    /// <returns>A FileValidationResult object indicating the validation result.</returns>
    public static FileValidationResult ValidateFile(this IFormFile file)
    {
        if (!file.CheckFileSize(2))
        {
            return new FileValidationResult(false, "File size can't exceed 2 MB!");
        }

        if (!file.CheckFileType("image"))
        {
            return new FileValidationResult(false, "File type is invalid!");
        }

        return new FileValidationResult(true);
    }
}
