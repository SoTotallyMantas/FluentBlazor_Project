using FluentBlazor_Project.Data;
using FluentBlazor_Project.Data.Models;
using FluentBlazor_Project.Interface;
using Microsoft.EntityFrameworkCore;

namespace FluentBlazor_Project.Services
{
    public class CartService : ICartService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        public CartService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        private ApplicationDbContext CreateContext() => _contextFactory.CreateDbContext();

        public async Task<Cart> GetOrCreateUserCartAsync(string UserId)
        {
            using var _dbContext = CreateContext();
            var cart = await _dbContext.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == UserId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = UserId,
                    CartItems = new List<CartItem>()

                };
                _dbContext.Carts.Add(cart);
                await _dbContext.SaveChangesAsync();
            }
            return cart;
        }

        public async Task AddCartItem(string UserId, Guid productId,int quantity)
        {
            try
            {
                using var _dbContext = CreateContext();
                quantity = Math.Max(1, quantity);

                var cart = await _dbContext.Carts
                    .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                    .FirstOrDefaultAsync(c => c.UserId == UserId);
                if(cart is null)
                {
                    cart = new Cart
                    {
                        UserId = UserId,
                        CartItems = new List<CartItem>()

                    };
                    _dbContext.Carts.Add(cart);
                    await _dbContext.SaveChangesAsync();
                }
                var existingItem = cart.CartItems
                    .FirstOrDefault(ci => ci.ProductId == productId);

                if (existingItem != null)
                {
                    existingItem.Quantity += quantity;
                }
                else
                {
                    var product = await _dbContext.Products
                       .FirstOrDefaultAsync(p => p.Id == productId && p.IsDeleted == false);
                    if (product == null)
                    {
                        throw new InvalidOperationException("Product not found");
                    }

                    cart.CartItems.Add(new CartItem
                    {
                        Cart = cart,
                        CartId = cart.Id,
                        ProductId = productId,
                        Quantity = quantity
                    });

                }
                await _dbContext.SaveChangesAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error Adding to car:", e.Message);
            }
        }

        public async Task RemoveFromCartAsync(string UserId, Guid productId)
        {
            using var _dbContext = CreateContext();

            var cart = await _dbContext.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == UserId);

            if (cart == null)
            {
                throw new InvalidOperationException("Cart not found for the user.");
            }

            if (cart.CartItems == null || !cart.CartItems.Any())
            {
                throw new InvalidOperationException("No items in the cart to remove.");
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (cartItem == null)
            {
                throw new InvalidOperationException("Cart item not found.");
            }

            if(cartItem.Quantity > 1)
            {
                cartItem.Quantity = cartItem.Quantity - 1;
            } 
            else
            {
                _dbContext.CartItems.Remove(cartItem);
            }
 
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateQuantityAsync(string UserId, Guid productId, int newQuantity)
        {
            using var _dbContext = CreateContext();

            var cartItem = await _dbContext.CartItems.FirstOrDefaultAsync(ci=> ci.Cart.UserId == UserId && ci.ProductId == productId);
            if (cartItem != null)
            {
                if (newQuantity  <= 0)
                {
                    _dbContext.CartItems.Remove(cartItem);
                }
                else
                {
                    
                    cartItem.Quantity = newQuantity;
                }
                await _dbContext.SaveChangesAsync();
            }
            else 
            {
                throw new InvalidOperationException("CartItem does not exist");
            }
        }

        public async Task ClearCartAsync(string userId)
        {
            using var _dbContext = CreateContext();

            var cart = await _dbContext.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                throw new InvalidOperationException("Cart not found for the user.");
            }

            _dbContext.CartItems.RemoveRange(cart.CartItems);
            await _dbContext.SaveChangesAsync();
        }
    }
}
