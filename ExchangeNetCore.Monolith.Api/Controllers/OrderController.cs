using ExchangeNetCore.MatchingEngine;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Linq;

namespace ExchangeNetCore.Monolith.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private readonly string _orderBookCacheKey = "OrderBook";
        private readonly IHubContext<OrderBookHub> _hubContext;

        public OrderController(IMemoryCache cache, IHubContext<OrderBookHub> hubContext)
        {
            _cache = cache;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult<Order>> PostAsync(Order order)
        {
            var orderBook = _cache.Get<OrderBook>(_orderBookCacheKey);
            if (orderBook == null)
                orderBook = new OrderBook();

            var processor = new Processor();
            processor.Process(order, orderBook);

            _cache.Set(_orderBookCacheKey, orderBook);

            var limitCount = 10;
            orderBook.AskOrders = orderBook.AskOrders.Take(limitCount).ToList();
            orderBook.BidOrders = orderBook.BidOrders.Take(limitCount).ToList();
            var orderBookJsonStr = JsonConvert.SerializeObject(orderBook);
            await _hubContext.Clients.All.SendAsync("ReceiveOrderBook", orderBookJsonStr);

            return Ok(order);
        }
    }
}