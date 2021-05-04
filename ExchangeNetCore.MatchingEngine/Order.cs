using static ExchangeNetCore.MatchingEngine.Enums;

namespace ExchangeNetCore.MatchingEngine
{
    public class Order
    {
        public Order(OrderType type, double price, double amount)
        {
            Id = System.Guid.NewGuid().ToString();
            Price = price;
            Amount = amount;
            Type = type;
        }

        public string Id { get; set; }
        public double Amount { get; set; }
        public double Price { get; set; }
        public OrderType Type { get; set; }
        public double Total
        {
            get
            {
                return Amount * Price;
            }
        }
    }
}
