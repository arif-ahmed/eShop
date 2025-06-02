using eShop.Models;
using System.Linq.Expressions;

namespace eShop.Data.Contracts;

public interface IRepository
{
    void Add(EntityBase entity);
    void Update(EntityBase entity);
    void Delete(Guid id);
    Task SaveChangesAsync();
    Task<EntityBase> GetByIdAsync(Guid id);
    Task<IEnumerable<EntityBase>> GetAllAsync(Expression<Func<EntityBase,bool>> expression);
}
