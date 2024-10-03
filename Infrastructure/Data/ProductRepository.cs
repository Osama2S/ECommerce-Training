using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
#nullable disable
    public class ProductRepository : IProductRepository
    {
        private readonly ECommerceDbContext _context;
        public ProductRepository(ECommerceDbContext context)
        {
            _context = context;
        }

       public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.products.Include(x=>x.productBrand).Include(x=>x.productType).ToListAsync();
        }

       public async Task<Product> GetProductIdAsync(int id)
        {
            return await _context.products.Include(x=>x.productBrand).Include(x=>x.productType).FirstAsync();
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }
    }
}
