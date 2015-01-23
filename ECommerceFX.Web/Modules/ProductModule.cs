using System;
using ECommerceFX.Web.Services;
using ECommerceFX.Web.ViewModels.Products;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses;

namespace ECommerceFX.Web.Modules
{
    public class ProductModule : NancyModule
    {
        private readonly IProductService _productService;

        public ProductModule(IProductService productService)
            :base("/products")
        {
            _productService = productService;
            Get["/"] = Index;
            Get["/all"] = GetAllProducts;
            Get["/id/{id:guid}"] = ById;
            Get["/name/{name}"] = ByName;
            Get["/description/{description}"] = ByDescription;
            Get["/create"] = Get_Create;
            Post["/create"] = Post_Create;
            Post["/delete/{id:guid}"] = DeleteProduct;
            Get["/update/{id:guid}"] = Update;
        }

        public dynamic Index(dynamic parameters)
        {
           return new ProductViewModels(_productService.All());
        }

        public dynamic GetAllProducts(dynamic parameters)
        {
            return Response.AsJson(_productService.All());
        }

        public dynamic ById(dynamic parameters)
        {
            var product = _productService.ById((Guid) parameters.Id);
            return new ProductViewModel(product);
        }

        public dynamic ByName(dynamic parameters)
        {
            return new ProductViewModels(_productService.ByName(parameters.Name));
        }

        public dynamic ByDescription(dynamic parameters)
        {
            return new ProductViewModels(_productService.ByName(parameters.Description));
        }

        public dynamic Get_Create(dynamic parameter)
        {
            return View["Views/Products/Create.cshtml"];
        }

        public dynamic Post_Create(dynamic parameters)
        {
            var createModel = this.Bind<ProductViewModel>();
            createModel.Id = Guid.NewGuid();
            _productService.Create(createModel.ToEntity());
            return HttpStatusCode.OK;
        }

        public dynamic DeleteProduct(dynamic parameters)
        {
            var model = this.Bind<ProductViewModel>();
            _productService.Delete(model.Id);
            return Response.AsRedirect("~/products");
        }

        public dynamic Update(dynamic parameters)
        {
            return Response.AsRedirect("~/products", RedirectResponse.RedirectType.Temporary);
        }
    }
}