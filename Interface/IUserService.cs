﻿using FluentBlazor_Project.Data;
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
        Task<List<IdentityRole>> GetRoles();
        Task UpdateRole(IdentityRole role);
        Task SetUserRoles(string UserId, List<string> Roles);
        Task<IList<string>> GetUserRole(string Id);
        Task DeleteUser(string userId);
        
    }
}
