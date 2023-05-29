using CurrencyExchange.Repository.Abstract;
using Customer.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchange.Repository.Concrete
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly CurrencyExchangeEFContext _dbContext;
        public Repository(CurrencyExchangeEFContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task DeleteAsync(int entityId)
        {
            var entity = await _dbContext.Set<T>().FindAsync(entityId);
            if (entity != null)
            {
                _dbContext.Set<T>().Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public IQueryable<T> GetAllQueryable()
        {
            return _dbContext.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> InsertAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
