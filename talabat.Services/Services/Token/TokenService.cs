using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entities.Identity;
using talabat.Core.Entities.Order_Aggregate;
using talabat.Core.ServicesContext;

namespace talabat.Services.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
           
        }
        public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager)
        {
            var AuthClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName, user.DisplayName),
                new Claim(ClaimTypes.Email,user.Email)
            };
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                AuthClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));

            var Token = new JwtSecurityToken(
                issuer: configuration["JWT:issuer"],
                audience: configuration["JWT:audience"],
                expires: DateTime.Now.AddDays(double.Parse(configuration["JWT:DurationInDays"])),
                claims: AuthClaims,
                signingCredentials:new SigningCredentials(AuthKey,SecurityAlgorithms.HmacSha256Signature)

                );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
