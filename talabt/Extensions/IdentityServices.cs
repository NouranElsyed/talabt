using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;
using talabat.Core.Entities.Identity;
using talabat.Repository.Identity;

namespace talabtAPIs.Extensions
{
    public static class IdentityServices
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection Services)
        {
            Services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppIdentityDbcontext>();
            Services.AddAuthentication();

            return  Services;
        }
    }
}
