using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;
using talabat.Core.Entities.Identity;
using talabat.Core.ServicesContext;
using talabat.Repository.Identity;
using talabat.Services.Services.Token;

namespace talabtAPIs.Extensions
{
    public static class IdentityServices
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection Services)
        {
            Services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppIdentityDbcontext>();
            Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
            Services.AddScoped<ITokenService,TokenService>();
            return  Services;
        }
    }
}
