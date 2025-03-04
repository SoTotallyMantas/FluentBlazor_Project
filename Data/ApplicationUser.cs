using FluentBlazor_Project.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace FluentBlazor_Project.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {

        public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
        public virtual ICollection<Favorites> Favorites { get; set; } = new List<Favorites>();
    }

}
