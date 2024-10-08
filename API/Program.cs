
using API.Errors;
using API.Middleware;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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
            builder.Services.Configure<ApiBehaviorOptions>(option =>
            option.InvalidModelStateResponseFactory=actionContext =>
            {
                var errors = actionContext.ModelState.Where(x => x.Value!.Errors.Count>0)
                .SelectMany(x => x.Value!.Errors)
                .Select(x => x.ErrorMessage)
                .ToArray();
                var errorResponse = new APIValidationErrorResponse
                {
                    Errors = errors
                };
                return new BadRequestObjectResult(errorResponse);
            });
            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title="Ecommerce Project",
                Version="v1"
            }));
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
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseStatusCodePagesWithReExecute("/error/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce Project V1");
            });
            app.MapControllers();

            app.Run();
        }
    }
}
