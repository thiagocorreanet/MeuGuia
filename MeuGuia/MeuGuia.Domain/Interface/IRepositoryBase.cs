using MeuGuia.Domain.Entitie;
using System.Linq.Expressions;

namespace MeuGuia.Domain.Interface;

public interface IRepositoryBase<TEntity> : IDisposable where TEntity : Base
{
    Task<IEnumerable<TEntity>> GetAllAsync(bool trackChange = false);

    Task<IEnumerable<TEntity>> SearchByConditionAsync(Expression<Func<TEntity, bool>> expression, bool trackChange = false);
    Task<TEntity> GetByIdAsync(int id, bool trackChange = false);

    void Create(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task<bool> SaveChangesAsync();
    Task StartTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
