using ExchangeNetCore.MatchingEngine;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;

namespace ExchangeNetCore.Monolith.Api
{
    public class OrderBookHub : Hub
    {
        private readonly IMemoryCache _cache;
        private readonly string _orderBookCacheKey = "OrderBook";

        public OrderBookHub(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task LoadOrderBook()
        {
            var orderBook = _cache.Get<OrderBook>(_orderBookCacheKey);
            var camelCaseFormatter = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var orderBookJsonStr = JsonConvert.SerializeObject(orderBook, camelCaseFormatter);
            await Clients.All.SendAsync("ReceiveOrderBook", orderBookJsonStr);
        }
    }
}
