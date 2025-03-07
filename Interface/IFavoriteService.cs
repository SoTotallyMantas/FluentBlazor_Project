using FluentBlazor_Project.Data.Models;

namespace FluentBlazor_Project.Interface
{
    public interface IFavoriteService
    {

        Task<List<Favorites>> GetUserFavoritesAsync(string UserId);

        Task AddToFavorites(Favorites favorite);

        Task RemoveFromFavorite(Guid favoriteId, string UserId);
    }
}
