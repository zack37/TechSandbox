using System;
using System.Collections.Generic;
using ECommerceFX.Data;
using ECommerceFX.Data.Messages.Queries;
using NServiceBus;

namespace ECommerceFX.Web.Services
{
    public interface IDataService<TEntity>
        where TEntity : class, IEntity
    {
        IEnumerable<TEntity> All<TRequest, TResponse>(Func<TResponse, IEnumerable<TEntity>> selector)
            where TRequest : class, IMessage
            where TResponse : class, IMessage;

        TEntity ByKey<TRequest, TResponse>(Action<TRequest> requestSetup, Func<TResponse, TEntity> responseSelector)
            where TRequest : class, IMessage
            where TResponse : class, IMessage;

        TEntity Create<TCommand>(TEntity entity, Action<TCommand> commandSetup)
            where TCommand : class, ICommand;
        
        void Delete<TCommand>(Guid id, Action<TCommand> commandSetup)
            where TCommand : class, ICommand;

        TEntity Update<TCommand>(TEntity entity, Action<TCommand> commandSetup)
            where TCommand : class, ICommand;

        TEntity ById<TRequest, TResponse>(Guid id, Func<TResponse, TEntity> responseSelector)
            where TRequest : class, IHaveIdMessage
            where TResponse : class, IMessage;
    }
}