using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

namespace ExchangeNetCore.Services.Signalr.Hubs
{
    public class OrderBookHub : Hub
    {
        private readonly IMemoryCache _cache;
        private readonly string _orderBookCacheKey = "OrderBook";

        public OrderBookHub(IMemoryCache cache)
        {
            _cache = cache;
        }
    }
}
