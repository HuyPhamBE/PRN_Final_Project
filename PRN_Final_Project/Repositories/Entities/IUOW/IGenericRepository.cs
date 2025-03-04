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
        //query
        IQueryable<T> Entities { get; }
        //async methods

        Task<IList<T>> GetAllAsync();
        Task<T?> GetByIdAsync(object id);
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(object id);
        Task SaveAsync();
        Task<T?> FirstorDefaultAsync(Expression<Func<T, bool>> predicate);
    }
}
