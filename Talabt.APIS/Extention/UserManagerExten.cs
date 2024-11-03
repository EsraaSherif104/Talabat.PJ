using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entities.identity;

namespace Talabt.APIS.Extention
{
    public static class UserManagerExten
    {
        public static async Task<AppUser?> finduserwithAddressAsync(this UserManager<AppUser> userManager,ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);
            var User =await userManager.Users.Include(u =>u.addres).FirstOrDefaultAsync(u=>u.Email == email);
           return User;
        }
    }
}
