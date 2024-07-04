using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SuperRate.Persistence.Context;

namespace SuperRate.Infrastructure;

public class BaseRepository<T> where T : class
{
    protected readonly SuperRateContext _context;
    protected readonly DbSet<T> _dbSet;

    protected BaseRepository(SuperRateContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    public async Task<T?> GetAsync(CancellationToken cancellationToken, params object[] key)
    {
        return await _dbSet.FindAsync(key, cancellationToken);
    }

    public async Task CreateAsync(T entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        _dbSet.Update(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }

    protected async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _dbSet.AnyAsync(predicate, cancellationToken);
    }
}