using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Text;
using talabat.Core.Entities.Identity;
using talabat.Core.ServicesContext;
using talabat.Repository.Identity;
using talabat.Services.Services.Token;

namespace talabtAPIs.Extensions
{
    public static class IdentityServices
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection Services,IConfiguration configuration)
        {
            Services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppIdentityDbcontext>();
            Services.AddAuthentication(options => 
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(Options => 
            {
                Options.TokenValidationParameters = new TokenValidationParameters()
                {
              
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))

                };
            });
            Services.AddScoped<ITokenService,TokenService>();
            return  Services;
        }
    }
}
