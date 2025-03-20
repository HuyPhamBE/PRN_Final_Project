using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Entities.IUOW
{
    public interface IGenericRepository<T> where T : class
    {
        // Queryable set of entities
        IQueryable<T> Entities { get; }


        // Async methods
        Task<IList<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);


        Task<T?> GetByIdAsync(object id);
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(object id);
        Task SaveAsync();
        Task<T?> FirstorDefaultAsync(Expression<Func<T, bool>> predicate);
    }
}
