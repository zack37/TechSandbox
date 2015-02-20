using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ECommerceFX.Web.Tools;
using Nancy;
using Nancy.ErrorHandling;
using Nancy.ViewEngines;

namespace ECommerceFX.Web
{
    public class ErrorHandler : DefaultViewRenderer, IStatusCodeHandler
    {
        private readonly ISet<HttpStatusCode> _supportedStatusCodes;

        public ErrorHandler(IViewFactory factory)
            : base(factory)
        {
            _supportedStatusCodes = Directory
                .GetFiles(Path.Combine(HttpRuntime.AppDomainAppPath, "Views", "Errors"))
                .Select(x =>
                {
                    var fileName = Path.GetFileNameWithoutExtension(x) ?? "404";
                    return (HttpStatusCode) int.Parse(fileName);
                })
                .ToHashSet();
        }

        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        {
            return _supportedStatusCodes.Contains(statusCode);
        }

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            var response = RenderView(context, "Views/Errors/" + (int) statusCode + ".cshtml");
            response.StatusCode = statusCode;
            context.Response = response;
        }
    }
}