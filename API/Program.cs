
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped(typeof(IGenericRepository<>),(typeof(GenericRepository<>)));
            builder.Services.AddDbContext<ECommerceDbContext>(option => option.UseSqlite(
                builder.Configuration.GetConnectionString("DefaultConnection")
                ));
            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            var app = builder.Build();            
            
            using (var host = app.Services.CreateScope())
            {
                var services = host.ServiceProvider;
                var loggerFactory= services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = services.GetRequiredService<ECommerceDbContext>();
                    await context.Database.MigrateAsync();
                    await ECommerceContextSeed.SeedAsync(context,loggerFactory);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An error occured during migration");
                }
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();

            
            app.MapControllers();

            app.Run();
        }
    }
}
