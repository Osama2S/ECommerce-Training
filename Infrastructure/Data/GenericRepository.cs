using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Data
{
#nullable disable
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ECommerceDbContext _context;
        public GenericRepository(ECommerceDbContext context)
        {
            _context=context;
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T>GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);            
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> Spec)
        {
            return await ApplySpecification(Spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> Spec)
        {
            return await ApplySpecification(Spec).ToListAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> Spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), Spec);
        }
    }
}
