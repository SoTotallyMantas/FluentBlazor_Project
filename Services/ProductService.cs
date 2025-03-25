using FluentBlazor_Project.Data;
using FluentBlazor_Project.Data.Models;
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
            return await  _dbContext.Products.ToListAsync();
        }

        public async Task AddProuctAsync(Product product)
        {
            if (product != null)
            {
                using var _dbContext = CreateContext();
                await _dbContext.Set<Product>().AddAsync(product);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task ToggleSoftDeletionProductAsync(Guid productId)
        {
            using var _dbContext = CreateContext();

            var product = await _dbContext.Products
                          .IgnoreQueryFilters()
                          .FirstOrDefaultAsync(p => p.Id == productId);

            if (product is null)
            {

                throw new InvalidOperationException("Product Not found.");
            }
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
            return await _dbContext.Set<Product>().FirstAsync(x => x.Id == guid);
        }

        public async Task<List<Product>> GetProductByCategoryAsync(string category)
        {
            if (category != null)
            {
                using var _dbContext = CreateContext();
                return await _dbContext.Set<Product>().Where(p => p.Category == category).ToListAsync();
            }
            else
            {
                return [];
            }
        }

       public async Task<List<Product>> GetProductByFilterAsync(ProductFilterOptions options)
        {
            using var _dbContext = CreateContext();
            var query = _dbContext.Set<Product>().AsQueryable();
            
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
            return await query.ToListAsync();

        }

    }
}
