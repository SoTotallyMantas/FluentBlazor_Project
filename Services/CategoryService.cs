using FluentBlazor_Project.Data;
using FluentBlazor_Project.Data.Models;
using FluentBlazor_Project.Interface;
using Microsoft.EntityFrameworkCore;

namespace FluentBlazor_Project.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        public CategoryService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        private ApplicationDbContext CreateContext() => _contextFactory.CreateDbContext();

        public async Task<List<Category>> GetCategoriesAsync()
        {
            using var _dbContext = CreateContext();
            return await _dbContext.Category
                .OrderBy(c => c.CategoryName)
                .ToListAsync();
        }

        public async Task<Category> GetByIdAsync(Guid Id)
        {
            using var _dbContext = CreateContext();
            return await _dbContext.Category.FirstAsync(c => c.Id == Id);
        }

        public async Task DeleteCategoryAsync(string categoryName)
        {
            using var _dbContext = CreateContext();
            var category =  await _dbContext.Category.FirstOrDefaultAsync(c => c.CategoryName == categoryName.Trim().ToLowerInvariant());
            if (category != null)
            {
                _dbContext.Category.Remove(category);
               await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"No Category '{categoryName ?? "Unknown"}' Exists"); 
            }


        }

        public async Task CreateCategoryAsync(string categoryName,string title ,string description)
        {
            using var _dbContext = CreateContext();
            if (string.IsNullOrWhiteSpace(categoryName))
                throw new ArgumentNullException("Category name required.", nameof(categoryName));

           

            Category newCategory = new Category()
                {
                CategoryName = categoryName.Trim().ToLowerInvariant(),
                Title = string.IsNullOrWhiteSpace(title) ? "Untitled" : title.Trim(),
                Description = string.IsNullOrWhiteSpace(description) ? "No Description" : description.Trim()
                };

            await _dbContext.Category.AddAsync(newCategory);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(string categoryName,string title,string description)
        {
            using var _dbContext = CreateContext();
            var category = _dbContext.Category.FirstOrDefault(c => c.CategoryName == categoryName);
            if (category != null)
            {
                category.Title = string.IsNullOrWhiteSpace(title) ? "Untitled" : title.Trim();
                category.Description = string.IsNullOrWhiteSpace(title) ? "No Description" : description.Trim();
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
