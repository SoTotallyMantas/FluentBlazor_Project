using FluentBlazor_Project.Data.Models;

namespace FluentBlazor_Project.Interface
{
    public interface ICategoryService
    {

        Task<List<Category>> GetCategoriesAsync();
        Task DeleteCategoryAsync(string categoryName);
        Task CreateCategoryAsync(string categoryName, string title, string description);
        Task UpdateCategoryAsync(string categoryName, string title, string description);


}
}
