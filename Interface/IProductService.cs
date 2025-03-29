using FluentBlazor_Project.Data.Models;

namespace FluentBlazor_Project.Interface
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync();
        Task AddProductAsync(Product product);
        Task<Product> RetrieveProductByIndexAsync(Guid guid);
        Task<List<Product>> GetProductByCategoryAsync(string category);
        Task<List<Product>> GetProductByFilterAsync(ProductFilterOptions options);
        Task EditProductAsync(Product updateProduct);
        Task ToggleSoftDeletionProductAsync(Guid productId);
    }
}
