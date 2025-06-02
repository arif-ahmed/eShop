using eShop.Models;
using System.Linq.Expressions;

namespace eShop.Data.Contracts;

public interface IRepository<TEntity> where TEntity : EntityBase
{
    void Add(TEntity entity);
    void Update(TEntity entity);
    Task Delete(Guid id);
    Task SaveChangesAsync();
    Task<TEntity> GetByIdAsync(Guid id);
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? expression);
}
