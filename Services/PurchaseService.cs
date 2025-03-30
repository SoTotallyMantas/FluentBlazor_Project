using FluentBlazor_Project.Data;
using FluentBlazor_Project.Data.Models;
using FluentBlazor_Project.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace FluentBlazor_Project.Services
{
    public class PurchaseService : IPurchaseService
    {
        public readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public PurchaseService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        private ApplicationDbContext CreateContext() => _contextFactory.CreateDbContext();

        public async Task<List<Purchase>> RetrieveAllPurchases()
        {
            using var _dbContext = CreateContext();
            return await _dbContext.Purchases.ToListAsync();
        }

        public async Task<List<Purchase>> RetrieveUsersPurchases(string UserId)
        {
            using var _dbContext = CreateContext();
            // UserId String due to Identity Guid being string
            // Ignores SoftDeleted Products
            // Includes Purchase items with the products
            // is readonly cannot be modified
            return await _dbContext.Purchases
                .Where(p => p.UserId == UserId)
                .Include(p => p.PurchaseItems)
                    .ThenInclude(pi => pi.Product)
                    .IgnoreQueryFilters()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task DeletePurchaseAsync(Guid purchaseId)
        {
            using var _dbContext = CreateContext();
            var purchase = await _dbContext.Purchases
                 .FirstAsync(p => p.Id == purchaseId);

            if(purchase != null)
            {
                _dbContext.Purchases.Remove(purchase);
                await _dbContext.SaveChangesAsync();

            }
        }

        public async Task DeletePurchaseItemAsync(Guid purchaseId, Guid productId)
        {
            using var _dbContext = CreateContext();
            var purchaseItem = await _dbContext.PurchaseItems.FirstAsync(p => p.PurchaseId == purchaseId && p.ProductId == productId);

            if (purchaseItem != null)
            {
                _dbContext.PurchaseItems.Remove(purchaseItem);
                await _dbContext.SaveChangesAsync();
            }
        }


        public async Task ChangePurchaseItemQuantityAsync(Guid purchaseId, Guid productId, int quantity)
        {
            using var _dbContext = CreateContext();
            var purchaseItem = await _dbContext.PurchaseItems.FirstAsync(p => p.PurchaseId == purchaseId && p.ProductId == productId);

            if (purchaseItem != null)
            {
                purchaseItem.Quantity = quantity;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task CreateNewPurchase(string userId,Dictionary<Guid,int> ProductQuantityPair)
        {
            using var _dbContext = CreateContext();
                var purchase = new Purchase
                {
                    UserId = userId,
                    PurchaseDate = DateTime.Now,

                };
            _dbContext.Purchases.Add(purchase);

            var purchaseItems = new List<PurchaseItem>();
                foreach (KeyValuePair<Guid, int> entry in ProductQuantityPair)
                {
                    PurchaseItem purchaseItemInsertion = new PurchaseItem
                    {
                        ProductId = entry.Key,
                        Quantity = entry.Value,
                        Purchase = purchase

                    };
                    purchaseItems.Add(purchaseItemInsertion);

                }
             _dbContext.PurchaseItems.AddRange(purchaseItems);
                
             await _dbContext.SaveChangesAsync();
        }
    }
}
