using Azure.Core;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using System.Net.Mime;
using System.Runtime.CompilerServices;

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
        // Get Content Type based on Extension
        private static string GetContentType(string path)
        {
            string extension = Path.GetExtension(path).ToLowerInvariant();
            // New way of writing switch
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".svg" => "image/svg+xml",
                ".webp" => "image/webp",
                _ => "Unknown"
            };
        }
        // Reads File from Predetermined location and returns base64 to display
        public async Task<string> ReadFileRetrieveBase64(string path)
        {
            if(string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                return string.Empty;
            }
            string UploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles");
            var relativePath = Path.Combine("UploadedFiles", path);
            try
            {
                byte[] fileBytes = await File.ReadAllBytesAsync(relativePath);
                string base64String = Convert.ToBase64String(fileBytes);
                string contentType = GetContentType(relativePath);
                return $"data:{contentType},base64,{base64String}";
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error reading file at '{relativePath}': {ex.Message}");
                return string.Empty;
            }


        }
        public async Task<List<string>> SaveFluentInputToFile(IEnumerable<FluentInputFileEventArgs> Files, List<Byte[]> ImageByteStreams)
        {


            // Save Paths for later creation of ProductImage in ProductService To avoid calling Product 
            List<string> productImagesRelativePaths = [];
            string UploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles");
            if (!Directory.Exists(UploadPath))
            {
                Directory.CreateDirectory(UploadPath);

            }
            var pairedValues = Files.Zip(ImageByteStreams, (file, data) => new { file, data });
            foreach (var pair in pairedValues)
            {
                if (pair.file != null || pair.data != null)
                {
                    {
                        var extension = Path.GetExtension(pair.file.Name);

                        // Get Random File Name For our file and create filePath to know where to write file 
                        // Relativepath is for database to save 

                        var filePath = $"{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}{extension}";
                        var fullFilePath = Path.Combine(UploadPath, filePath);
                        var relativePath = Path.Combine("UploadedFiles", filePath);

                        try
                        {
                            // Create file and save it to path
                            await File.WriteAllBytesAsync(fullFilePath, pair.data);
                            // save RelativePath to List of image paths
                            productImagesRelativePaths.Add(relativePath);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error saving file '{pair.file.Name}': {ex.Message}");
                        }
                    }
                }
            }
            return productImagesRelativePaths;
        }
    }
}
