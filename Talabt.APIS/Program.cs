using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Talabat.Core.Entities;
using Talabat.Core.Entities.identity;
using Talabat.Core.Repositories;
using Talabat.Core.Repositories.Identity;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Talabt.APIS.Errors;
using Talabt.APIS.Extention;
using Talabt.APIS.helpers;
using Talabt.APIS.MiddelWire;

namespace Talabt.APIS
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region Configure Services-Add services to the container.


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
          
            builder.Services.AddDbContext<StoreContext>(options=>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddDbContext<AppIdentityDbcontext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            builder.Services.AddSingleton < IConnectionMultiplexer>(options=>
           {
               var connection = builder.Configuration.GetConnectionString("RedisConnection");
               return ConnectionMultiplexer.Connect(connection);
           
           } );
            
            
            builder.Services.AddApplicationService();
            builder.Services.AddIdentityServices(builder.Configuration);
           builder.Services.AddCors(options=>
           {
               options.AddPolicy("MyPolicy", options =>
               {
                   options.AllowAnyHeader();
                   options.AllowAnyMethod();
                  // options.WithOrigins(builder.Configuration["FrontBaseUrl"]);
               });
           });
            
            #endregion

            var app = builder.Build();
            #region Update-Database
            //StoreContext dbcontext = new StoreContext();//invaild
            //await dbcontext.Database.MigrateAsync();

            using var scope = app.Services.CreateScope();
            //group of services lifetime scoped
            var services = scope.ServiceProvider;
            //services its self
            var loggerFactory=services.GetRequiredService<ILoggerFactory>();    
            try
            {
                
                var dbcontext = services.GetRequiredService<StoreContext>();
                //ask clr for creating object from dbcontext explicity
                await dbcontext.Database.MigrateAsync();
                //  scope.Dispose();

                var IdentityDbcontext = services.GetRequiredService<AppIdentityDbcontext>();
                await IdentityDbcontext.Database.MigrateAsync();
                var usermanger = services.GetRequiredService<UserManager<AppUser>>();
                
                await AppIdentityDbcontextSeed.SeedUserAsync(usermanger);

                await  StoreContextSeed.SeedAsync(dbcontext);


            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An Error Occured During Appling the Migration ");


            }

            #endregion


            #region Configure- Configure the HTTP request pipeline.
            app.UseMiddleware<ExceptionMiddlewire>();

            if (app.Environment.IsDevelopment())
            {

                app.UseSwaggerMiddelWire();
            }
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseCors("MyPolicy");  
            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
