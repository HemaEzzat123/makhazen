using MAKHAZIN.Core;
using MAKHAZIN.Core.Entities;
using MAKHAZIN.Core.Repository.Contract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MAKHAZINDbContext _dbContext;
        private Hashtable _repositories;

        public UnitOfWork(MAKHAZINDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Hashtable();
        }
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var key = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(key))
            {
                var respository = new GenericRepository<TEntity>(_dbContext);
                _repositories.Add(key, respository);
            }
            return _repositories[key] as IGenericRepository<TEntity>;
        }
        public Task<int> CompleteAsync()
            => _dbContext.SaveChangesAsync();

        public ValueTask DisposeAsync()
            => _dbContext.DisposeAsync();


    }
}
