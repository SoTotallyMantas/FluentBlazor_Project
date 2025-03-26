using FluentBlazor_Project.Data.Models;
using FluentBlazor_Project.Data.Models.ImageTables;

namespace FluentBlazor_Project.HelperFunctions
{
    public static class ProductValidationHelper
    {
       
        public static void EnsureValidProduct(Product? product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            if (product.Images == null || !product.Images.Any())
                throw new ArgumentNullException(nameof(product));
        }

        public static void EnsureValidImages(List<ProductImages> images)
        {
            if (images == null || !images.Any())
                throw new ArgumentNullException(nameof(images));
        }
        public static void EnsureSingleThumbnail(List<ProductImages> images)
        {

           
            var thumbnailCount = images.Count(img => img.SelectedTag == ProductImages.Tag.Thumbnail);

            if (thumbnailCount > 1)
                throw new ArgumentException("Only one image can be marked as Thumbnail.");
        }
    }
}
