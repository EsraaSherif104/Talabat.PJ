using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Repositories;
using Talabat.Repository;
using Talabt.APIS.Errors;
using Talabt.APIS.helpers;

namespace Talabt.APIS.Extention
{
    public static class ApplicationServiceExtention
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection Services)
        {
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //builder.Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
            // builder.Services.AddAutoMapper(M=>M.AddProfile(new MappingProfiles()));
            Services.AddAutoMapper(typeof(MappingProfiles));
            #region handle error
            Services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    //modelstat =>dictionary[keyvaluepair
                    //key=>name of param
                    //value=>error

                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                            .SelectMany(p => p.Value.Errors)
                                            .Select(e => e.ErrorMessage).ToArray();

                    var validationErrorREsponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(validationErrorREsponse);
                };
            });
            #endregion
            return Services;
        }
    }
}
