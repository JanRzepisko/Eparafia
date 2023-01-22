using Eparafia.Application.DataAccess.Abstract;
using Eparafia.Application.DTOs;

namespace Eparafia.Application.Repository;

public interface IBaseRepository<TEntity>
{
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    void Update(TEntity entity);
    ValueTask<TEntity?> GetByIdAsync(Guid? id, CancellationToken cancellationToken = default);
    IQueryable<TEntity> GetQueryable();
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    void RemoveById(Guid id);
    void Remove(TEntity entity);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<TEntity>> GetPage (int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}