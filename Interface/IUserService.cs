using FluentBlazor_Project.Data;
using FluentBlazor_Project.Data.DTO;
using Microsoft.AspNetCore.Identity;

namespace FluentBlazor_Project.Interface
{
    public interface IUserService
    {
        Task<List<UserDisplay>> GetUsers();
        Task SeedRoles(string UserId);
        Task DeleteRole(IdentityRole role);
        Task CreateRole(string Role);
        List<IdentityRole> GetRoles();
        Task UpdateUserDetails(UserDisplay userDisplay);
        Task<UserDisplay> GetUserAsync(string id);
        Task UpdateRole(IdentityRole role);
        Task SetUserRoles(string UserId, List<string> Roles);
        Task<IList<string>> GetUserRole(string Id);
        Task DeleteUser(string userId);
        
    }
}
