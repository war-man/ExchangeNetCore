using ExchangeNetCore.Infrastructure.EventBus;
using ExchangeNetCore.MatchingEngine;
using ExchangeNetCore.Services.Ordering.Api.IntegrationEvents.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ExchangeNetCore.Services.Ordering.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private readonly string _orderBookCacheKey = "OrderBook";
        private readonly IEventBus _eventBus;

        public OrderController(IMemoryCache cache, IEventBus eventBus)
        {
            _cache = cache;
            _eventBus = eventBus;
        }

        // POST api/order
        [HttpPost]
        public ActionResult<Order> Post(Order order)
        {
            var orderBook = _cache.Get<OrderBook>(_orderBookCacheKey);
            if (orderBook == null)
                orderBook = new OrderBook();

            var processor = new Processor();
            processor.Process(order, orderBook);

            _cache.Set(_orderBookCacheKey, orderBook);

            var @event = new OrderBookChangedIntegrationEvent(orderBook.BidOrders, orderBook.AskOrders);
            _eventBus.Publish(@event);

            return Ok(order);
        }

        // GET api/order/OrderBook
        [HttpGet]
        [Route("OrderBook")]
        public ActionResult<OrderBook> GetOrderBook()
        {
            var orderBook = _cache.Get<OrderBook>(_orderBookCacheKey);
            if (orderBook == null)
                orderBook = new OrderBook();

            return Ok(orderBook);
        }
    }
}