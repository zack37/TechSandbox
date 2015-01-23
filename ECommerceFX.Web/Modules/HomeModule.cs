using Nancy;

namespace ECommerceFX.Web.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = Index;
            Get["/test"] = SendMessage;
        }

        public dynamic Index(dynamic parameter)
        {
            return View["Views/Home/Index.cshtml"];
        }

        public dynamic SendMessage(dynamic parameters)
        {
            return Response.AsRedirect("/");
        }
    }
}