using ExchangeNetCore.Infrastructure.EventBus;
using ExchangeNetCore.MatchingEngine;
using ExchangeNetCore.Services.Signalr.Api.IntegrationEvents.Events;
using ExchangeNetCore.Services.Signalr.Hubs;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeNetCore.Services.Signalr.Api.IntegrationEvents.Handles
{
    public class OrderBookChangedIntegrationEventHandler : IIntegrationEventHandler<OrderBookChangedIntegrationEvent>
    {
        private readonly IHubContext<OrderBookHub> _hubContext;

        public OrderBookChangedIntegrationEventHandler(IHubContext<OrderBookHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task Handle(OrderBookChangedIntegrationEvent @event)
        {
            var limitCount = 10;
            var orderBook = new OrderBook
            {
                AskOrders = @event.AskOrders.Take(limitCount).ToList(),
                BidOrders = @event.BidOrders.Take(limitCount).ToList()
            };
            var camelCaseFormatter = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var orderBookJsonStr = JsonConvert.SerializeObject(orderBook, camelCaseFormatter);
            return _hubContext.Clients.All.SendAsync("ReceiveOrderBook", orderBookJsonStr);
        }
    }
}
