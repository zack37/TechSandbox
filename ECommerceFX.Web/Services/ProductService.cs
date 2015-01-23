using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using ECommerceFX.Data;
using ECommerceFX.Data.Messages.Commands;
using ECommerceFX.Data.Messages.Events;
using ECommerceFX.Data.Messages.Queries;
using ECommerceFX.Web.ViewModels.Products;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using NLog;
using NServiceBus;
using RestSharp;

namespace ECommerceFX.Web.Services
{
    public class ProductService : RestService<Product>, IProductService
    {
        public ProductService(IRestClient client)
            : base(client, "/products") { }

        public IEnumerable<Product> ByName(string name)
        {
            var request = new RestRequest(BaseUrl + "/name/" + name);
            return RequestAll(request);
        }

        public async Task<IEnumerable<Product>> ByNameAsync(string name)
        {
            var request = new RestRequest(BaseUrl + "/name/" + name);
            return await RequestAllAsync(request, new CancellationToken());
        }

        public IEnumerable<Product> ByDescription(string description)
        {
            var request = new RestRequest(BaseUrl + "/description/" + description);
            return RequestAll(request);
        }

        public async Task<IEnumerable<Product>> ByDescriptionAsync(string description)
        {
            var request = new RestRequest(BaseUrl + "/description/" + description);
            return await RequestAllAsync(request, new CancellationToken());
        }
    }

    public class NProductService : IProductService
    { 
        private readonly IBus _bus;
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public NProductService(IBus bus)
        {
            _bus = bus;
        }

        public IEnumerable<Product> All()
        {
            Log.Info("Retrieving all products using NServiceBus. I'm actually making some pretty decent progress");
            return SendMessage<GetAllProductsQuery, GetAllProductsResponse>(x => { }).Products;
        }

        private TResponse SendMessage<TRequest, TResponse>(Action<TRequest> setup)
            where TResponse : class
        {
            return _bus.Send(setup).Register(t => (t.Messages.Single() as TResponse)).Result;
        }

        public Product ById(Guid id)
        {
            return SendMessage<GetProductByIdQuery, GetProductByIdResponse>(x => x.Id = id).Product;
        }

        public Product Create(Product entity)
        {
            return SendMessage<CreateProduct, ProductCreated>(x => x.Product = entity).Product;
        }

        public Task<Product> CreateAsync(Product entity, CancellationToken token)
        {
            return null;
        }

        public void Delete(Guid id)
        {
            _bus.Send<DeleteProduct>(x => x.Id = id);
        }

        public Task DeleteAsync(Guid id, CancellationToken token)
        {
            return null;
        }

        public Product Update(Product entity)
        {
            return null;
        }

        public Task<Product> UpdateAsync(Product entity, CancellationToken token)
        {
            return null;
        }

        public IEnumerable<Product> ByName(string name)
        {
            yield break;
        }

        public Task<IEnumerable<Product>> ByNameAsync(string name)
        {
            return null;
        }

        public IEnumerable<Product> ByDescription(string description)
        {
            yield break;
        }

        public Task<IEnumerable<Product>> ByDescriptionAsync(string description)
        {
            return null;
        }
    }

    public interface IHubProvider
    {
        IHubContext GetHub<THub>()
            where THub : IHub;
    }

    public class HubProvider : IHubProvider
    {
        public IHubContext GetHub<THub>() where THub : IHub
        {
            return GlobalHost.ConnectionManager.GetHubContext<THub>();
        }
    }

    public class ProductConsumer : IHandleMessages<ProductCreated>,
                                   IHandleMessages<ProductDeleted>
    {
        private readonly IHubContext _productHubContext;

        public ProductConsumer(IHubProvider hubProvider)
        {
            _productHubContext = hubProvider.GetHub<ProductHub>();
        }

        public void Handle(ProductCreated message)
        {
            _productHubContext.Clients.All.broadcastProductCreated(new ProductViewModel(message.Product));
        }

        public void Handle(ProductDeleted message)
        {
            _productHubContext.Clients.All.broadcastProductDeleted(message.Id);
        }
    }
}