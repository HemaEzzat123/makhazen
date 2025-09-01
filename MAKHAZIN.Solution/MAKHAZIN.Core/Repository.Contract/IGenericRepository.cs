using MAKHAZIN.Core.Entities;
using MAKHAZIN.Core.Sepecification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Core.Repository.Contract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        public Task<T?> FindFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        public Task<T?> GetByIdWithSpecAsync(ISpecifications<T> spec);
        public Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
