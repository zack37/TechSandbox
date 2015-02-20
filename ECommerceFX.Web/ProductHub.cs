using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace ECommerceFX.Web
{
    public interface IProductHub : IHub { }

    public class ProductHub : Hub, IProductHub { }
}