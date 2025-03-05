using FluentBlazor_Project.Data.Models;
using FluentBlazor_Project.Interface;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace FluentBlazor_Project.Services
{
    public class ProductService : IProductService
    {
        DbContext _dbContext;
        public ProductService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await  _dbContext.Set<Product>().ToListAsync();
        }

        public async Task AddProuctAsync(Product product)
        {
            if (product != null)
            {
                await _dbContext.Set<Product>().AddAsync(product);    
            }
        }

        public async Task<Product> RetrieveProductByIndexAsync(Guid guid)
        {
            return await _dbContext.Set<Product>().FirstAsync(x => x.Id == guid);
        }

        public async Task<List<Product>> GetProductByCategoryAsync(string category)
        {
            if (category != null)
            {
                return await _dbContext.Set<Product>().Where(p => p.Category == category).ToListAsync();
            }
            else
            {
                return [];
            }
        }

       public async Task<List<Product>> GetProductByFilterAsync(ProductFilterOptions options)
        {
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
