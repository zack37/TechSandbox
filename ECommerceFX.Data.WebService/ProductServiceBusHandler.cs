using ECommerceFX.Data.Messages.Commands;
using ECommerceFX.Data.Messages.Events;
using ECommerceFX.Data.Messages.Queries;
using ECommerceFX.Repository;
using NServiceBus;

namespace ECommerceFX.Data.WebService
{
    public class ProductServiceBusHandler : IHandleMessages<GetAllProductsQuery>,
                                            IHandleMessages<GetProductByIdQuery>,
                                            IHandleMessages<CreateProduct>,
                                            IHandleMessages<DeleteProduct>,
                                            IHandleMessages<UpdateProduct>
    {
        private readonly IBus _bus;
        private readonly IProductRepository _productsRepository;

        public ProductServiceBusHandler(IBus bus, IProductRepository productsRepository)
        {
            _bus = bus;
            _productsRepository = productsRepository;
        }

        public void Handle(GetAllProductsQuery message)
        {
            var response = new GetAllProductsResponse
            {
                Products = _productsRepository.All()
            };
            _bus.Reply(response);
        }

        public void Handle(GetProductByIdQuery message)
        {
            var response = new GetProductByIdResponse
            {
                Product = _productsRepository.ById(message.Id)
            };
            _bus.Reply(response);
        }

        public void Handle(CreateProduct message)
        {
            var newProduct = _productsRepository.Create(message.Product);
            _bus.Publish<ProductCreated>(p => p.Product = newProduct);
        }

        public void Handle(DeleteProduct message)
        {
            _productsRepository.Delete(message.Id);
            _bus.Publish<ProductDeleted>(x => x.Id = message.Id);
        }
 
        public void Handle(UpdateProduct message)
        {
            _productsRepository.Update(message.Product);
            _bus.Publish<ProductUpdated>(x => x.Product = message.Product);
        }
    }
}