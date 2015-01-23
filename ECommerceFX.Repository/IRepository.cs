using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ECommerceFX.Data;

namespace ECommerceFX.Repository
{
    public interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        IEnumerable<TEntity> All();
        IEnumerable<TEntity> AllByKey(Func<TEntity, bool> predicate);
        TEntity ByKey(Func<TEntity, bool> predicate);
        TEntity ById(Guid id);
        TEntity Create(TEntity entity);
        void Delete(Guid id);
        TEntity Update(TEntity entity);
    }
}