using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using Nancy.Hosting.Wcf;

namespace ECommerceFX.Data.WebService
{
    public class Startup
    {
        public static void Main()
        {
            var host = new WebServiceHost(new NancyWcfGenericService(), new Uri("http://localhost:3227/api/data"));
            host.AddServiceEndpoint(typeof (NancyWcfGenericService), new WebHttpBinding(), "");
            host.Open();
            Console.ReadLine();
        }
    }
}