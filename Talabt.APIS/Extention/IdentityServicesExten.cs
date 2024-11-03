using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Entities.identity;
using Talabat.Core.Repositories.Identity;
using Talabat.Core.Services;
using Talabat.Services;

namespace Talabt.APIS.Extention
{
    public static class IdentityServicesExten
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<ITokenServices,TokenServices>();
            services.AddIdentity<AppUser, IdentityRole>()
                           .AddEntityFrameworkStores<AppIdentityDbcontext>();


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =  JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["JWT:VaildIssuer"],
                        ValidateAudience = true,
                        ValidAudience = configuration["JWT:VaildAudience"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))

                };
                });
                
                
                
                 //username +manafer+role

            return services;

        }
    }
}
