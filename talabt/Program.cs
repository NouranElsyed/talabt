
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using StackExchange.Redis;
using talabat.Core.Entities;
using talabat.Core.Entities.Identity;
using talabat.Core.RepositoriesContext;
using talabat.Repository;
using talabat.Repository.Data;
using talabat.Repository.Identity;
using talabt.Error;
using talabt.Extensions;
using talabt.Helper;
using talabtAPIs.Extensions;

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
            builder.Services.AddDbContext<AppIdentityDbcontext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(option => {
                var connection = builder.Configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(connection);
            });
     



            builder.Services.AddApplicationServices();

            builder.Services.AddIdentityServices(builder.Configuration);



            var app = builder.Build();


           using var scope = app.Services.CreateScope();//Group of services lifeTime scoped
                var services = scope.ServiceProvider;//services its Self
                var _dbcontext = services.GetRequiredService<StoreContext>();
                var _Identitydbcontext = services.GetRequiredService<AppIdentityDbcontext>();
            var UserManager = services.GetRequiredService <UserManager<AppUser>> ();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();


            try
            {
                await _dbcontext.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(_dbcontext);
                await _Identitydbcontext.Database.MigrateAsync();
                await AppIdentityDbcontextSeed.SeedUserAsync(UserManager);

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
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
