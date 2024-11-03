using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.identity;
using Talabat.Core.Repositories.Identity;
using Talabat.Core.Services;
using Talabat.Services;

namespace Talabt.APIS.Extention
{
    public static class IdentityServicesExten
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenServices,TokenServices>();
            services.AddIdentity<AppUser, IdentityRole>()
                           .AddEntityFrameworkStores<AppIdentityDbcontext>();


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(); //username +manafer+role

            return services;

        }
    }
}
