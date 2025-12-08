using MAKHAZIN.Core.Entities;
using MAKHAZIN.Core.Repository.Contract;
using MAKHAZIN.Core.Sepecification;
using MAKHAZIN.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly MAKHAZINDbContext _context;

        public GenericRepository(MAKHAZINDbContext context)
        {
            _context = context;
        }
        public async Task<T?> GetByIdAsync(int id)
            => await _context.Set<T>().FindAsync(id);

        public async Task<IReadOnlyList<T>> GetAllAsync()
            => await _context.Set<T>().ToListAsync();


        public async Task AddAsync(T entity)
            => await _context.Set<T>().AddAsync(entity);


        public void Update(T entity)
            => _context.Set<T>().Update(entity);
        public void Delete(T entity)
            => _context.Set<T>().Remove(entity);
        public async Task<int> CountAsync(ISpecifications<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }
        public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }
        public async Task<T?> FindFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<T?> GetByIdWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecification(spec).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecification(spec).AsNoTracking().ToListAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecifications<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }
}
