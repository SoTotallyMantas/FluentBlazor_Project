using FluentBlazor_Project.Data.Models;

namespace FluentBlazor_Project.Interface
{
    public interface ICartService
    {
        Task<Cart> GetOrCreateUserCartAsync(string UserId);
        Task AddCartItem(string UserId, Guid productId, int quantity);
        Task RemoveFromCartAsync(string UserId, Guid productId);
        Task UpdateQuantityAsync(string UserId, Guid productId, int newQuantity);
        Task ClearCartAsync(string userId);
       
    }
}
