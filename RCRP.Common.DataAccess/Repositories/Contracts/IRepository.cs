using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace RCRP.Common.DataAccess.Repositories.Contracts;

public interface IRepository<T>
{
    DbContext Context { get; }

    Task DeleteAsync(params object[] keyValues);
    void Delete(T entityToDelete);

    Task<IEnumerable<T>> GetAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

    Task<T?> GetByKeyAsync(params object[] keyValues);

    Task InsertAsync(T entity);

    void Update(T entityToUpdate);
}
