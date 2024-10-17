using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using talabat.Core.Entities.Identity;

namespace talabtAPIs.Extensions
{
    public static class UserMangerExtension
    {
        public static async Task<AppUser?> FindUserByAddressAsync(this UserManager<AppUser> userManager,ClaimsPrincipal User) 
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
    }
}
