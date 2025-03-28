using FluentBlazor_Project.Data;
using FluentBlazor_Project.Data.Models;
using FluentBlazor_Project.Data.Models.ImageTables;
using FluentBlazor_Project.HelperFunctions;
using FluentBlazor_Project.Interface;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace FluentBlazor_Project.Services
{
    public class ProductService : IProductService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        public ProductService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        private ApplicationDbContext CreateContext() => _contextFactory.CreateDbContext();

        public async Task<List<Product>> GetProductsAsync()
        {
            using var _dbContext = CreateContext();
            return await  _dbContext.Products
                .Include(p => p.Images).
                ToListAsync();
        }

        public async Task AddProductAsync(Product product)
        {
            ProductValidationHelper.EnsureValidProduct(product);
            ProductValidationHelper.EnsureSingleThumbnail(product.Images);

                using var _dbContext = CreateContext();


                    if (product.Id != Guid.Empty)
                    {
                        product.Id = Guid.NewGuid();
                    }
                    foreach (var image in product.Images)
                    {
                        image.ProductId = product.Id;
                    }
            await _dbContext.Set<Product>().AddAsync(product);
            await _dbContext.SaveChangesAsync();

        }
        public async Task EditProductAsync(Product updateProduct)
        {
            ProductValidationHelper.EnsureValidProduct(updateProduct);
            ProductValidationHelper.EnsureSingleThumbnail(updateProduct.Images);
            ProductValidationHelper.EnsureValidImages(updateProduct.Images);
            using var _dbContext = CreateContext();
            
            var existingProduct = await _dbContext.Products
                .Include(p=>p.Images)
                .FirstOrDefaultAsync(p=> p.Id == updateProduct.Id);

            ProductValidationHelper.EnsureValidProduct(existingProduct);

            bool isModified = false;
            
            if(DifferenceCheckHelper.UpdateIfDifferent(existingProduct.Name, updateProduct.Name))
            {
                existingProduct.Name = updateProduct.Name;
                isModified = true;
            }

            if (DifferenceCheckHelper.UpdateIfDifferent(existingProduct.Type, updateProduct.Type))
            {
                existingProduct.Type = updateProduct.Type;
                isModified = true;
            }
            if (DifferenceCheckHelper.UpdateIfDifferent(existingProduct.Category, updateProduct.Category))
            {
                existingProduct.Category = updateProduct.Category;
                isModified = true;
            }
            if (DifferenceCheckHelper.UpdateIfDifferent(existingProduct.Description, updateProduct.Description))
            {
                existingProduct.Description = updateProduct.Description;
                isModified = true;
            }
            if (DifferenceCheckHelper.UpdateIfDifferent(existingProduct.Price, updateProduct.Price))
            {
                existingProduct.Price = updateProduct.Price;
                isModified = true;
            }

            
            List<string> imagePathsToDelete = new List<string>();

                // explicitly defining img => img to rember how it works
                var existingMap = existingProduct.Images.ToDictionary(img => img.Id, img => img);
            

                var imagesToRemove = existingProduct.Images
                    .Where(e => !updateProduct.Images.Any(u => u.Id == e.Id))
                    .ToList();


            foreach (var image in imagesToRemove)
            {
                imagePathsToDelete.Add(image.ImagePath);
                _dbContext.ProductImages.Remove(image);
                isModified = true;  
            }
            
           

            foreach (var incomingImage in updateProduct.Images)
            {
                if (existingMap.TryGetValue(incomingImage.Id, out var existing))
                {
                    if (existing.SelectedTag != incomingImage.SelectedTag)
                    {

                        existing.SelectedTag = incomingImage.SelectedTag;
                        isModified = true;
                    }
                }
                else

                {
                    incomingImage.Product = null;
                    incomingImage.ProductId = existingProduct.Id;
                    _dbContext.ProductImages.Add(incomingImage);
                    isModified = true;
                }
                
            }

            if (isModified)
            {
               await _dbContext.SaveChangesAsync();

                foreach (var path in imagePathsToDelete)
                {
                    FileHelper.TryDeleteImage(path);
                }
            }
        }

        public async Task ToggleSoftDeletionProductAsync(Guid productId)
        {
            using var _dbContext = CreateContext();

            var product = await _dbContext.Products
                          .IgnoreQueryFilters()
                          .FirstOrDefaultAsync(p => p.Id == productId);

            ProductValidationHelper.EnsureValidProduct(product);
            // Toggle Flag
            product.IsDeleted = !product.IsDeleted;

            if (product.IsDeleted)
            {
                var relatedCartItems = _dbContext.CartItems
                    .Where(ci => ci.ProductId == productId);
                _dbContext.CartItems.RemoveRange(relatedCartItems);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Product> RetrieveProductByIndexAsync(Guid guid)
        {
            using var _dbContext = CreateContext();
            return await _dbContext.Set<Product>()
                .Include(p => p.Images)
                .FirstAsync(x => x.Id == guid);
        }

        public async Task<List<Product>> GetProductByCategoryAsync(string category)
        {
            if (category != null)
            {
                using var _dbContext = CreateContext();
                return await _dbContext.Set<Product>().Where(p => p.Category == category)
                    .Include(p => p.Images)
                    .ToListAsync();
            }
            else
            {
                throw new ArgumentNullException("Category is null");
            }
        }
        // Not Used
       public async Task<List<Product>> GetProductByFilterAsync(ProductFilterOptions options)
        {
            using var _dbContext = CreateContext();
            var query = _dbContext.Set<Product>()
                .Include(p => p.Images)
                .AsQueryable();
            
            if (options == null)
            {
                // Return empty instead of null to avoid null checks
                return [];
            }
            // Filter by price range 
            if (options.PriceRange != null && options.PriceRange.Count == 2)
            {
                decimal minPrice = options.PriceRange.Min();
                decimal maxPrice = options.PriceRange.Max();

                query = query.Where(p => p.Price >= minPrice && p.Price <= maxPrice);
            }
            // Filter by categories
            if(options.categories  != null && options.categories.Any())
            {
                query = query.Where(p => options.categories.Contains(p.Category));
            }

            // Filter by name 
            if(options.ProductName != null && options.ProductName.Any())
            {
                query = query.Where(p => options.ProductName.Contains(p.Name));
            }

            // Returns List of products based on filter options
            return await query
                .Include(p => p.Images)
                .ToListAsync();

        }

    }
}
