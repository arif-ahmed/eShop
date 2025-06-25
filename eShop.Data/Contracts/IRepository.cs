using eShop.Models;
using System.Linq.Expressions;

namespace eShop.Data.Contracts;

public interface IRepository<TEntity> where TEntity : EntityBase
{
    Task Add(TEntity entity);
    Task Update(TEntity entity);
    Task Delete(Guid id);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<TEntity> GetByIdAsync(Guid id);
    Task<IEnumerable<TEntity>> GetAllAsync();    
    Task<FilterDataResponse> Filter(Expression<Func<TEntity, bool>> expression, string sortBY, string sortOrder, int offset = 0, int limit = 100);
}
