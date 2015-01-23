using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ECommerceFX.Data;

namespace ECommerceFX.Web.Services
{
    public interface IService<TEntity>
        where TEntity : class, IEntity
    {
        IEnumerable<TEntity> All();
        TEntity ById(Guid id);
        TEntity Create(TEntity entity);
        Task<TEntity> CreateAsync(TEntity entity, CancellationToken token);
        void Delete(Guid id);
        Task DeleteAsync(Guid id, CancellationToken token);
        TEntity Update(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken token);
    }
}