using Microsoft.AspNetCore.Mvc;
using talabat.Core;
using talabat.Core.RepositoriesContext;
using talabat.Repository;
using talabt.Error;
using talabt.Helper;

namespace talabt.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services) 
        {
            Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddAutoMapper(typeof(MappingProfiles));
            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                    .SelectMany(P => P.Value.Errors)
                    .Select(E => E.ErrorMessage).ToArray();
                    var Response = new ApiVaidationErrorResponse() { Errors = errors };
                    return new BadRequestObjectResult(Response);
                };
            });
            Services.AddScoped<IUnitOfWork,UnitOfWork>();
            return Services;
        }
    }
}
