using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabt.APIS.helpers;

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
            //builder.Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            // builder.Services.AddAutoMapper(M=>M.AddProfile(new MappingProfiles()));
            builder.Services.AddAutoMapper(typeof(MappingProfiles));
          
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
                await  StoreContextSeed.SeedAsync(dbcontext);


            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An Error Occured During Appling the Migration ");


            }

            #endregion
           

            #region Configure- Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
