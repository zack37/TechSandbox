using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.ErrorHandling;
using Nancy.ViewEngines;

namespace ECommerceFX.Web
{
    public class ErrorHandler : DefaultViewRenderer, IStatusCodeHandler
    {
        private readonly IEnumerable<HttpStatusCode> _supportedStatusCodes;

        public ErrorHandler(IViewFactory factory)
            : base(factory)
        {
            _supportedStatusCodes = Directory
                .GetFiles(Path.Combine(HttpRuntime.AppDomainAppPath, "Views", "Errors"))
                .Select(x =>
                {
                    var fileName = Path.GetFileNameWithoutExtension(x);
                    return (HttpStatusCode) int.Parse(fileName);
                });
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