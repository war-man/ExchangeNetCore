using ExchangeNetCore.Infrastructure.EventBus;
using ExchangeNetCore.MatchingEngine;
using System.Collections.Generic;

namespace ExchangeNetCore.Services.Signalr.Api.IntegrationEvents.Events
{
    public class OrderBookChangedIntegrationEvent : IntegrationEvent
    {
        public List<Order> BidOrders { get; set; }
        public List<Order> AskOrders { get; set; }

        public OrderBookChangedIntegrationEvent(List<Order> bidOrders, List<Order> askOrders)
        {
            BidOrders = bidOrders;
            AskOrders = askOrders;
        }
    }
}
