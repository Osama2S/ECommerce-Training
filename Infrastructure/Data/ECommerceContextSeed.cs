using Core.Entities;
using Ganss.Excel;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public  class ECommerceContextSeed
    {
        public static async Task SeedAsync(ECommerceDbContext context , ILoggerFactory logger)
        {
			
			try
			{
				if(!context.ProductBrands.Any())
				{
					var filename = @"C:\Users\osama\source\repos\Ecommerce Project\Infrastructure\Data\SeedingData\ProductBrand.xlsx";
					var productBrand=new ExcelMapper(filename).Fetch<ProductBrand>();
                    foreach (var item in productBrand)
                    {
						await context.ProductBrands.AddAsync(item);
                    }
                    await context.SaveChangesAsync();

                }
            }
			catch (Exception ex)
			{
				var _logger = logger.CreateLogger<ECommerceContextSeed>();
				_logger.LogError(ex.Message);
			}
			try
			{
				if(!context.ProductTypes.Any())
				{
					var filename = @"C:\Users\osama\source\repos\Ecommerce Project\Infrastructure\Data\SeedingData\ProductType.xlsx";
					var productType = new ExcelMapper(filename).Fetch<ProductType>();
                    foreach (var item in productType)
                    {
						await context.ProductTypes.AddAsync(item);
                    }
					await context.SaveChangesAsync();
                }
			}
			catch (Exception ex)
			{
				var _logger = logger.CreateLogger<ECommerceContextSeed>();
				_logger.LogError(ex.Message);
			}
            try
            {
                if (!context.products.Any())
                {
                    var filename = @"C:\Users\osama\source\repos\Ecommerce Project\Infrastructure\Data\SeedingData\ProductECommerce.xlsx";
                    var product = new ExcelMapper(filename).Fetch<Product>();
                    foreach (var item in product)
                    {
                        var productBrand = await context.ProductBrands.FindAsync(item.ProductBrandId);
                        var productType = await context.ProductTypes.FindAsync(item.ProductTypeId);

                        if (productBrand != null)
                        {
                            item.productBrand = productBrand; // Assign the productBrand based on the ID
                        }

                        if (productType != null)
                        {
                            item.productType = productType; // Assign the productType based on the ID
                        }

                        await context.products.AddAsync(item);
                    }
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                var _logger = logger.CreateLogger<ECommerceContextSeed>();
                _logger.LogError(ex.Message);
            }
        }
    }
}
