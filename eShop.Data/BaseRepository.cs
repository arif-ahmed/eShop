using eShop.Data.Contracts;
using eShop.Models;
using eShop.Models.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace eShop.Data;

public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
{
    protected readonly EShopDbContext _context;
    public BaseRepository(EShopDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Add(TEntity entity)
    {
        await _context.AddAsync(entity);        
    }

    public async Task Delete(Guid id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        if (entity != null)
        {
            _context.Remove(entity);
        }
        else
        {
            throw new KeyNotFoundException($"Entity with ID {id} not found.");
        }
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>()
            .Where(e => !e.IsDeleted) 
            .ToListAsync();
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


    public Task Update(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        _context.Update(entity);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<FilterDataResponse> Filter(Expression<Func<TEntity, bool>> expression, string sortBY, string sortOrder = "asc", int offset = 0, int limit = 100)
    {
        var query = _context.Set<TEntity>().Where(expression).AsQueryable();
        if (!string.IsNullOrEmpty(sortBY))
        {
            if (sortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase))
            {
                query = query.OrderBy(e => EF.Property<object>(e, sortBY));
            }
            else if (sortOrder.Equals("desc", StringComparison.OrdinalIgnoreCase))
            {
                query = query.OrderByDescending(e => EF.Property<object>(e, sortBY));
            }
        }

        var count = await query.CountAsync();

        var response = await query.Skip(offset).Take(limit).ToListAsync();

        var result = new FilterDataResponse
        {
            TotalCount = count,
            Items = response.Select(e => e) // Convert to object to match the response type
        };

        return result;
    }
}

public class FilterDataResponse 
{
    public int TotalCount { get; set; }
    public IEnumerable<EntityBase> Items { get; set; } = Enumerable.Empty<EntityBase>();
}