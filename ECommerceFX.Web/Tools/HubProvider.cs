using System;
using Microsoft.AspNet.SignalR.Hubs;

namespace ECommerceFX.Web.Tools
{
    public class HubProvider : IHubProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public HubProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IHub GetHub<THub>() where THub : IHub
        {
            return (THub) _serviceProvider.GetService(typeof (THub));
        }
    }
}