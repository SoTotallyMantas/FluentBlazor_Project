using Microsoft.FluentUI.AspNetCore.Components;
using System.IO;

namespace FluentBlazor_Project.HelperFunctions
{
    public class FileHelper
    {
        public FileHelper()
        {
        }

        public static void TryDeleteImage(string relativePath)
        {
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadFiles");
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
            string UploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadFiles");
            if (!Directory.Exists(UploadPath))
            {
                Directory.CreateDirectory(UploadPath);

            }

            foreach (var file in Files)
            {
                if (file.Stream != null)
                {
                    // Get Random File Name For our file and create filePath to know where to write file 
                    // Relativepath is for database to save 
                    var randomFileName = Path.GetRandomFileName();
                    var filePath = Path.Combine(UploadPath, randomFileName);
                    var relativePath = Path.Combine("UploadFiles", randomFileName);

                    // Create file and save it to path
                    await using FileStream fileStream = new(filePath, FileMode.Create);
                    await file.Stream.CopyToAsync(fileStream);
                    // save RelativePath to List of image paths
                    productImagesRelativePaths.Add(relativePath);
                }
            }
            return productImagesRelativePaths;

        }
    }
}
