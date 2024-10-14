
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using StackExchange.Redis;
using talabat.Core.Entities;
using talabat.Core.RepositoriesContext;
using talabat.Repository;
using talabat.Repository.Data;
using talabt.Error;
using talabt.Extensions;
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
            builder.Services.AddSingleton<IConnectionMultiplexer>(option => {
                var connection = builder.Configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(connection);
            });
            //builder.Services.AddScoped<IGenericRepository<Products>, GenericRepository<Products>>();
            //builder.Services.AddScoped<IGenericRepository<Brand>, GenericRepository<Brand>>();
            //builder.Services.AddScoped<IGenericRepository<Category>, GenericRepository<Category>>();
            builder.Services.AddApplicationServices();
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
