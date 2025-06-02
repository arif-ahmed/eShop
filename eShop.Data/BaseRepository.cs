using eShop.Data.Contracts;
using eShop.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace eShop.Data;

public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
{
    protected readonly EShopDbContext _context;
    public BaseRepository(EShopDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Add(TEntity entity)
    {
        _context.Add(entity);
    }

    public void Delete(Guid id)
    {
        var entity = _context.Set<TEntity>().Find(id);
        if (entity != null)
        {
            _context.Remove(entity);
        }
        else
        {
            throw new KeyNotFoundException($"Entity with ID {id} not found.");
        }
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? expression)
    {
        if (expression == null)
        {
            throw new ArgumentNullException(nameof(expression));
        }
        var entities = await _context.Set<TEntity>().Where(expression).ToListAsync();
        return entities;
    }

    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);

        if (entity == null)
        {
            throw new KeyNotFoundException($"Entity with ID {id} was not found.");
        }

        return entity;
    }


    public void Update(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        _context.Update(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();        
    }
}
