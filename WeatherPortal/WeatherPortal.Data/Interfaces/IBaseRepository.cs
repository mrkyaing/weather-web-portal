using System.Linq.Expressions;

namespace WeatherPortal.Data.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task Create(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetBy(Expression<Func<T, bool>> expression);
        void Delete(T entity); 
        void Update(T entity);
    }
}
