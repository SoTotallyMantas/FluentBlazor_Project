using FluentBlazor_Project.Data.Models;

namespace FluentBlazor_Project.Interface
{
    public interface ICategoryService
    {

        Task<List<Category>> GetCategoriesAsync();
        Task<Category> GetByIdAsync(Guid id);
        Task DeleteCategoryAsync(Category category);
        Task CreateCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);

        Task<Category> GetCategoryByNameAsync(string name);


}
}
