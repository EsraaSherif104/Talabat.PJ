using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.identity;
using Talabat.Core.Repositories.Identity;

namespace Talabt.APIS.Extention
{
    public static class IdentityServicesExten
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>()
                           .AddEntityFrameworkStores<AppIdentityDbcontext>();


            services.AddAuthentication(); //username +manafer+role

            return services;

        }
    }
}
