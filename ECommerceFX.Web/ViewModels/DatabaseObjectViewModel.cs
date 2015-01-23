using System;
using System.ComponentModel.DataAnnotations;
using ECommerceFX.Data;

namespace ECommerceFX.Web.ViewModels
{
    public abstract class DatabaseObjectViewModel<TEntity>
        where TEntity : class, IEntity, new()
    {
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }

        public virtual TEntity ToEntity()
        {
            var entity = new TEntity();
            var properties = GetType().GetProperties();
            foreach (var property in properties)
            {
                var p = entity.GetType().GetProperty(property.Name);
                if (p != null)
                    p.SetValue(entity, property.GetValue(this));
            }

            return entity;
        }
    }
}