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
using Talabat.Core.Entities.identity;
using Talabat.Core.Services;

namespace Talabat.Services
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration _configuration;

        public TokenServices(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(AppUser User,UserManager<AppUser>userManager)
        {

            //payload
            //1-private claim [user defined]
            var AuthClaim = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName,User.DisplayName),
                new Claim(ClaimTypes.Email,User.Email)
            };


            var userRole = await userManager.GetRolesAsync(User);
            foreach(var role in userRole)
            {
                AuthClaim.Add(new Claim(ClaimTypes.Role, role));
            }

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var Token = new JwtSecurityToken(
                issuer: _configuration["JWT:VaildIssuer"],
                audience: _configuration["JWT:VaildAudience"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:DurationInDays"])),
                claims: AuthClaim,
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)

                );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
