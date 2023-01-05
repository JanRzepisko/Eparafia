using Eparafia.Application.DataAccess.Abstract;
using Eparafia.Application.DTOs;
using Eparafia.Application.Repository;
using Microsoft.EntityFrameworkCore;

namespace Eparafia.Infrastructure.Repository;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity, new()
{
    protected DbSet<TEntity> _entities;

    public BaseRepository(DbSet<TEntity>? entities)
    {
        _entities = entities!;
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _entities.AddAsync(entity, cancellationToken);
    }

    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _entities.AnyAsync(c => c.Id == id, cancellationToken);
    }

    public Task<List<TEntity>> GetPage(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        return _entities.OrderBy(c => c.Id).Skip((pageNumber) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }

    public Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _entities.ToListAsync(cancellationToken);
    }

    public ValueTask<TEntity?> GetByIdAsync(Guid? id, CancellationToken cancellationToken = default)
    {
        return _entities.FindAsync(new object[] {id}, cancellationToken);
    }

    public IQueryable<TEntity> GetQueryable()
    {
        return _entities.AsQueryable();
    }

    public void Remove(TEntity entity)
    {
        _entities.Remove(entity);
    }

    public void RemoveById(Guid id)
    {
        var attachedEntity = _entities.Attach(new TEntity {Id = id});
        attachedEntity.State = EntityState.Deleted;
    }

    public void Update(TEntity entity)
    {
        _entities.Update(entity);
    }
}