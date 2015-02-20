using Microsoft.AspNet.SignalR.Hubs;

namespace ECommerceFX.Web.Tools
{
    public interface IHubProvider
    {
        IHub GetHub<THub>()
            where THub : IHub;
    }
}