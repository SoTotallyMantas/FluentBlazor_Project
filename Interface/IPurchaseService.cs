using FluentBlazor_Project.Data.Models;

namespace FluentBlazor_Project.Interface
{
    public interface IPurchaseService
    {
        Task<List<Purchase>> RetrieveAllPurchases();

        Task<List<Purchase>> RetrieveUsersPurchases(string UserId);

        Task DeletePurchaseAsync(Guid purchaseId);

        Task DeletePurchaseItemAsync(Guid purchaseId, Guid productId);

        Task ChangePurchaseItemQuantityAsync(Guid purchaseId, Guid productId, int quantity);

        Task CreateNewPurchase(string userId, Dictionary<Guid, int> ProductQuantityPair);
    }
}
