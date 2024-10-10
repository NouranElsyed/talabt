
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using talabat.Core.Entities;
using talabat.Core.RepositoriesContext;
using talabat.Repository;
using talabat.Repository.Data;
using talabt.Error;
using talabt.Helper;

namespace talabt
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreContext>(option => 
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            //builder.Services.AddScoped<IGenericRepository<Products>, GenericRepository<Products>>();
            //builder.Services.AddScoped<IGenericRepository<Brand>, GenericRepository<Brand>>();
            //builder.Services.AddScoped<IGenericRepository<Category>, GenericRepository<Category>>();
            builder.Services.AddScoped( typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddAutoMapper(typeof(MappingProfiles));
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(P=>P.Value.Errors.Count()>0)
                    .SelectMany(P => P.Value.Errors)
                    .Select(E=>E.ErrorMessage).ToArray();
                    var Response = new ApiVaidationErrorResponse() { Errors=errors};
                    return new BadRequestObjectResult(Response);
                };
            });
            var app = builder.Build();
           using var scope = app.Services.CreateScope();
                var services = scope.ServiceProvider;
                var _dbcontext = services.GetRequiredService<StoreContext>();
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await _dbcontext.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(_dbcontext);
            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex,"an error occur during migration");
            }
            app.UseStatusCodePagesWithReExecute("/error/{0}");
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
