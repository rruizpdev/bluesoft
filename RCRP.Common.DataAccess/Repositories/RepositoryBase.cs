using Microsoft.EntityFrameworkCore;
using RCRP.Common.DataAccess.Repositories.Contracts;
using System.Linq.Expressions;

namespace RCRP.Common.DataAccess.Repositories;

public abstract class RepositoryBase<T>: IRepository<T>
    where T : class
{
    protected readonly DbContext _context;
    protected readonly DbSet<T> _entitySet;

    public RepositoryBase(
        DbContext context,
        DbSet<T> entitySet)
    {
        this._context = context;
        this._entitySet = entitySet;
    }

    public DbContext Context => _context;
    protected virtual IQueryable<T> BuildQuery() => _entitySet;

    public virtual async Task<IEnumerable<T>> GetAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
    {
        var query = BuildQuery();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return (orderBy != null)
            ? await orderBy(query).ToListAsync()
            : await query.ToListAsync();
    }

    public virtual async Task<T?> GetByKeyAsync(params object[] keyValues)
    {
        var query = BuildQuery();
        return await query.FirstOrDefaultAsync(FilterByKeyPredicate(keyValues));
    }

    public virtual async Task InsertAsync(T entity)
        => await _entitySet.AddAsync(entity);

    public virtual async Task DeleteAsync(params object[] keyValues)
    {
        var entityToDelete = await _entitySet.FindAsync(keyValues);

        if (entityToDelete != null)
        {
            Delete(entityToDelete);
        }
    }

    public virtual void Delete(T entityToDelete)
    {
        if (_context.Entry(entityToDelete).State == EntityState.Detached)
        {
            _entitySet.Attach(entityToDelete);
        }
        _entitySet.Remove(entityToDelete);
    }

    public virtual void Update(T entityToUpdate)
    {
        _entitySet.Attach(entityToUpdate);
        _context.Entry(entityToUpdate).State = EntityState.Modified;
    }

    protected abstract Expression<Func<T, bool>> FilterByKeyPredicate(
        params object[] keyValues);
}
