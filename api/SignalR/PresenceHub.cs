
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
namespace api.SignalR
{
    [Authorize]
    public class PresenceHub: Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.Others.SendAsync("user online", Context.User);
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Clients.Others.SendAsync("user Offline", Context.User);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
