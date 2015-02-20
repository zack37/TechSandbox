using System;
using System.Collections.Generic;
using System.Linq;
using ECommerceFX.Data;
using ECommerceFX.Data.Messages.Queries;
using NServiceBus;

namespace ECommerceFX.Web.Services
{
    public abstract class DataService<T> : IDataService<T> where T : class, IEntity
    {
        protected IBus Bus { get; private set; }

        protected DataService(IBus bus)
        {
            Bus = bus;
        }

        public IEnumerable<T> All<TRequest, TResponse>(Func<TResponse, IEnumerable<T>> selector)
            where TRequest : class, IMessage
            where TResponse : class, IMessage
        {
            return selector(SendRequest<TRequest, TResponse>());
        }

        public T ById<TRequest, TResponse>(Guid id, Func<TResponse, T> responseSelector)
            where TRequest : class, IHaveIdMessage
            where TResponse : class, IMessage
        {
            return ByKey<TRequest, TResponse>(request => request.Id = id, responseSelector);
        }

        public T ByKey<TRequest, TResponse>(Action<TRequest> requestSetup, Func<TResponse, T> responseSelector)
            where TRequest : class, IMessage
            where TResponse : class, IMessage
        {
            return responseSelector(SendRequest<TRequest, TResponse>(requestSetup));
        }

        public T Create<TCommand>(T entity, Action<TCommand> commandSetup)
            where TCommand : class, ICommand
        {
            Bus.Send(commandSetup);
            return entity;
        }

        public void Delete<TCommand>(Guid id, Action<TCommand> commandSetup)
            where TCommand : class, ICommand
        {
            Bus.Send(commandSetup);
        }

        public T Update<TCommand>(T entity, Action<TCommand> commandSetup)
            where TCommand : class, ICommand
        {
            Bus.Send(commandSetup);
            return entity;
        }

        protected TResponse SendRequest<TRequest, TResponse>(Action<TRequest> setup)
            where TRequest : class, IMessage
            where TResponse : class, IMessage
        {
            return Bus.Send(setup).Register(t => (t.Messages.Single() as TResponse)).Result;
        }

        protected TResponse SendRequest<TRequest, TResponse>()
            where TRequest : class, IMessage
            where TResponse : class, IMessage
        {
            return Bus.Send<TRequest>(x => { }).Register(t => (t.Messages.Single() as TResponse)).Result;
        }
    }
}