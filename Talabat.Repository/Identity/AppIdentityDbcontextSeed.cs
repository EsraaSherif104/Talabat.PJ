using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.identity;

namespace Talabat.Repository.Identity
{
    public static class AppIdentityDbcontextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var User = new AppUser()
                {
                    DisplayName = "Esraa Sherif",
                    Email = "esraasherif9992@gmail.com",
                    UserName = "esraasherif",
                    PhoneNumber = "01063277063",
                };
                await userManager.CreateAsync(User, "esraa321.com");

            }


        }

    }
}
