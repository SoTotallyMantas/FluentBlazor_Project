using FluentBlazor_Project.Data;
using FluentBlazor_Project.Data.DTO;
using FluentBlazor_Project.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FluentBlazor_Project.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedRoles(string UserId)
        {
            
            if(await _roleManager.RoleExistsAsync("administrator"))
            {
                var UserList = await _userManager.GetUsersInRoleAsync("administrator");
                if(UserList.Count > 0)
                {
                    return;
                }
                else
                {
                    await _userManager.AddToRoleAsync(await _userManager.FindByIdAsync(UserId), "administrator");
                    
                }
            }
            else
            {
               await _roleManager.CreateAsync(new IdentityRole { Name = "administrator" });
               
                await _userManager.AddToRoleAsync(await _userManager.FindByIdAsync(UserId), "administrator");
                
            }
            return;

        }
        public async Task DeleteRole(IdentityRole role)
        {
            try
            {
                if (role.Name != "administrator" && role.Name != "user")
                {
                    
                    await _roleManager.DeleteAsync(role);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error Deleting Role: ", e);
            }
        }

        public async Task CreateRole(string Role)
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = Role });
        }
        public async Task<List<IdentityRole>> GetRoles()
        {
            return _roleManager.Roles.ToList();
        }
        public async Task DeleteUser(string userId)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByIdAsync(userId);
                if (user is not null)
                {
                    await _userManager.DeleteAsync(user);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error While Deleting User: ", e.Message);
            }
        }
        public async Task<List<UserDisplay>> GetUsers()
        {
            
           
            List<ApplicationUser> UserList = _userManager.Users.ToList();
            List<UserDisplay> UserDisplayList = UserList.Select(u => new UserDisplay { Id = u.Id, Email = u.Email, Name = u.UserName })
                                                        .ToList();          
            return UserDisplayList;
        }

        public async Task<IList<string>> GetUserRole(string Id)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByIdAsync(Id);
                if (user != null)
                {
                    var UserRoles = await _userManager.GetRolesAsync(user);
                    return UserRoles;
                }
                else
                {
                    Console.WriteLine("User Null");
                }
                return [];
            }
            catch
            {
                return new List<string>();
            }
        }
        public async Task UpdateRole(IdentityRole role)
        {
            if(role != null && role.Name != "administrator" && role.Name != "user")
            {
                _roleManager.UpdateAsync(role);
            }
        }
        public async Task SetUserRoles(string UserId,List<string> Roles)
        {

            try
            { 
                ApplicationUser User = await _userManager.FindByIdAsync(UserId);
                if (User != null)
                {
                    await _userManager.AddToRolesAsync(User, Roles);
                }
                else
                {
                    Console.WriteLine("User null");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error  Setting Roles: ", e);
                
            }
        }

        
    }
}
