using InteractiveDashboard.Application.InfrastructureServices;
using Microsoft.AspNetCore.SignalR;

namespace InteractiveDashboard.Application.Services
{
    public class TickerHub : Hub<IPriceTickerClient>
    {
        public async Task SubscribeForTicker(string tickerName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, tickerName);
        }
        public async Task UnSubscribeForTicker(string tickerName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, tickerName);
        }
    }
}
