using System;
using System.Collections.Generic;
using System.Linq;
using ECommerceFX.Data;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace ECommerceFX.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly Func<MongoCollection<TEntity>> _entities;

        protected Repository()
        {
            const string connection = "mongodb://localhost/?safe=true";
            var server = new MongoClient(connection).GetServer();
            var database = server.GetDatabase("ecfx");
            _entities = () => database.GetCollection<TEntity>(typeof (TEntity).Name);
        }

        public IEnumerable<TEntity> All()
        {
            return _entities().AsQueryable().AsEnumerable();
        }

        public IEnumerable<TEntity> AllByKey(Func<TEntity, bool> predicate)
        {
            return All().Where(predicate);
        }

        public TEntity ByKey(Func<TEntity, bool> predicate)
        {
            return All().FirstOrDefault(predicate);
        }

        public TEntity ById(Guid id)
        {
            return All().SingleOrDefault(x => x.Id == id);
        }

        public TEntity Create(TEntity entity)
        {
            _entities().Save(entity);
            return entity;
        }

        public void Delete(Guid id)
        {
            var query = Query<TEntity>.EQ(x => x.Id, id);
            _entities().Remove(query);
        }

        public TEntity Update(TEntity entity)
        {
            var query = Query<TEntity>.EQ(x => x.Id, entity.Id);
            var update = Update<TEntity>.Replace(entity);
            _entities().Update(query, update);
            return entity;
        }
    }
}
