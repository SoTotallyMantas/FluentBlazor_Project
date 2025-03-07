using FluentBlazor_Project.Data;
using FluentBlazor_Project.Data.Models;
using FluentBlazor_Project.Interface;
using Microsoft.EntityFrameworkCore;

namespace FluentBlazor_Project.Services
{
    public class FavoriteService : IFavoriteService
    {
        public readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public FavoriteService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        private ApplicationDbContext CreateContext() => _contextFactory.CreateDbContext();

        public async Task<List<Favorites>> GetUserFavoritesAsync(string UserId)
        {
            var _dbContext = CreateContext();

            return await _dbContext.Favorites
                .Where(p => p.UserId == UserId)
                .Include(p => p.Product)
                .ToListAsync();
        }

        public async Task AddToFavorites(Favorites favorite)
        {
            var _dbContext = CreateContext();
            await _dbContext.Favorites.AddAsync(favorite);
        }

        public async Task RemoveFromFavorite(Guid favoriteId , string UserId)
        {
            var _dbContext = CreateContext();
            var Favorite = await _dbContext.Favorites.FirstAsync(p => p.Id == favoriteId && p.UserId == UserId);
            if (Favorite != null)
            {

               _dbContext.Favorites.Remove(Favorite);
                await _dbContext.SaveChangesAsync();
            }
        }




    }
}
