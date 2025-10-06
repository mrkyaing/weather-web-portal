using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WeatherPortal.Data.Interfaces;
using WeatherPortal.Data.Data;

namespace WeatherPortal.Data.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        protected BaseRepository(ApplicationDbContext dbContext) 
        {
              _dbContext = dbContext;
              _dbSet = _dbContext.Set<T>();
        }
        public async Task Create(T entity)
        {
            await _dbContext.AddAsync<T>(entity);
        }
        public void Delete(T entity)
        {
            _dbContext.Remove<T>(entity);
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<T>> GetBy(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AsNoTracking().Where(expression).ToListAsync();
        }
        public void Update(T entity)
        {
            _dbContext.Update<T>(entity);
        }
    }
}
