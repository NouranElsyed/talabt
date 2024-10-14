using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entities.Identity;

namespace talabat.Repository.Identity
{
    public class AppIdentityDbcontextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager) 
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "",
                    Email = "",
                    UserName = "",
                    PhoneNumber = ""
                };
                await userManager.CreateAsync(user, "P@$Wa0rd");
            }
        }
          
    }
}
