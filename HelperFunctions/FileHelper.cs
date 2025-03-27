using Azure.Core;
using Microsoft.FluentUI.AspNetCore.Components;
using System.IO;
using System.Net.Mime;

namespace FluentBlazor_Project.HelperFunctions
{
    public class FileHelper
    {
        public FileHelper()
        {
        }

        public static void TryDeleteImage(string relativePath)
        {
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath);
            if (File.Exists(fullPath))
            {
                try
                {
                    File.Delete(fullPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            }

        }


        public async Task<List<string>> SaveImageStreamToFile(IEnumerable<FluentInputFileEventArgs> Files)
        {


            // Save Paths for later creation of ProductImage in ProductService To avoid calling Product 
            List<string> productImagesRelativePaths = [];
            string UploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles");
            if (!Directory.Exists(UploadPath))
            {
                Directory.CreateDirectory(UploadPath);

            }
            long maxFileSize = 1 * 1024 * 1024;
            string[] allowedExtensions = [".jpg", ".jpeg", ".png", ".webp", ".svg"];
  
            foreach (var file in Files)
            {
                if (file.Stream != null)
                {
                    var extension = Path.GetExtension(file.Name);
                    if (!allowedExtensions.Contains(extension))
                    {
                        Console.WriteLine($"Rejected file '{file.Name}': Unsupported type '{file.ContentType}'");
                        continue;
                    }
                    if (file.Size > maxFileSize)
                    {
                        Console.WriteLine($"Rejected file '{file.Name}': Exceeds max file size");
                        continue;
                    }
                    // Get Random File Name For our file and create filePath to know where to write file 
                    // Relativepath is for database to save 
                    
                    
                    var filePath = $"{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}{extension}";
                    var fullFilePath = Path.Combine(UploadPath, filePath);
                    var relativePath = Path.Combine("UploadedFiles", filePath);

                    try
                    {
                        // Create file and save it to path

                        await using FileStream fileStream = new(fullFilePath, FileMode.Create);
                        await file.Stream.CopyToAsync(fileStream);
                        // save RelativePath to List of image paths
                        productImagesRelativePaths.Add(relativePath);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"Error saving file '{file.Name}': {ex.Message}"); 
                    }
                }
            }
            return productImagesRelativePaths;

        }
    }
}
