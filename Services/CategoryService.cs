using FluentBlazor_Project.Data;
using FluentBlazor_Project.Data.Models;
using FluentBlazor_Project.Data.Models.ImageTables;
using FluentBlazor_Project.HelperFunctions;
using FluentBlazor_Project.Interface;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
                .Include(c=> c.Images)
                .OrderBy(c => c.CategoryName)
                .ToListAsync();
        }

        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            try
            {


                if (name is not null)
                {
                    using var _dbContext = CreateContext();

                    var result = await _dbContext.Category.Where(c => c.CategoryName == name)
                        .FirstOrDefaultAsync(c => c.CategoryName == name);

                    
                    return result ;
                }
                else
                {
                    return null;
                }
               
            }
            catch(Exception e )
            {
                Console.WriteLine($"Error Returning Category By Name: {name} " + $"{e.Message}");
                throw;
            }
        }
        public async Task<Category> GetByIdAsync(Guid Id)
        {
            using var _dbContext = CreateContext();
            return await _dbContext.Category
                .Include(c => c.Images)
                .FirstAsync(c => c.Id == Id);
        }

        public async Task DeleteCategoryAsync(Category category)
        {
           ValidateCategory(category);
            using var _dbContext = CreateContext();
           var trackedCategory =  await _dbContext.Category
                .Include(c=>c.Images)
                .FirstOrDefaultAsync(c => c.Id == category.Id);

            if (trackedCategory != null)
            {
                

                var imagePath = trackedCategory.Images?.ImagePath;
                _dbContext.Category.Remove(trackedCategory);
                await _dbContext.SaveChangesAsync();
                if (!string.IsNullOrEmpty(imagePath))
                {
                    FileHelper.TryDeleteImage(imagePath);
                }
            }
            else
            {
                throw new InvalidOperationException($"Category with ID '{category.Id}' does not exist");
            }

    


        }

        public async Task CreateCategoryAsync(Category category)
        {
            ValidateCategory(category);
            using var _dbContext = CreateContext();




            category.CategoryName = category.CategoryName.Trim().ToLowerInvariant();
            category.Title = string.IsNullOrWhiteSpace(category.Title) ? "Untitled" : category.Title.Trim();
            category.Description = string.IsNullOrWhiteSpace(category.Description) ? "No Description" : category.Description.Trim();

            if (category.Id == Guid.Empty)
                category.Id = Guid.NewGuid();

            if (category.Images != null)
            {
                 if(category.Images.Id == Guid.Empty)
                  category.Images.Id = Guid.NewGuid();

                category.Images.CategoryId = category.Id;
                category.Images.Category = category;
            }
            else
            {
                throw new ArgumentNullException("Image Cannot be null");
            }

            await _dbContext.Category.AddAsync(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            ValidateCategory(category);
            using var _dbContext = CreateContext();
            var trackedCategory = _dbContext.Category
                .Include(c=> c.Images)
                .FirstOrDefault(c => c.Id == category.Id);
            ValidateCategory(trackedCategory);

            trackedCategory.CategoryName = string.IsNullOrWhiteSpace(category.CategoryName) ? trackedCategory.CategoryName : category.CategoryName.Trim();
            trackedCategory.Title = string.IsNullOrWhiteSpace(category.Title) ? "Untitled" : category.Title.Trim();
            trackedCategory.Description = string.IsNullOrWhiteSpace(category.Description) ? "No Description" : category.Description.Trim();
            // Deletion of old path
            string? oldPath = null;
            if(category.Images != null)
            {
                if(trackedCategory.Images == null)
                {
                    trackedCategory.Images = new CategoryImages
                    {
                        Id = Guid.NewGuid(),
                        ImagePath = category.Images.ImagePath,
                        CategoryId = category.Id,
                        Category = category
                    };
                }
                else
                {
                    oldPath = trackedCategory.Images.ImagePath;
                    trackedCategory.Images.ImagePath = category.Images.ImagePath;
                }
            }


                await _dbContext.SaveChangesAsync();

                if(!string.Equals(oldPath,category.Images.ImagePath, StringComparison.OrdinalIgnoreCase))
                {
                    FileHelper.TryDeleteImage(oldPath);
                }
            
        }


        private void ValidateCategory(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            if (string.IsNullOrWhiteSpace(category.CategoryName))
            {
                throw new ArgumentException("Category name is required.", nameof(category.CategoryName));
            }
        }
        }
}
