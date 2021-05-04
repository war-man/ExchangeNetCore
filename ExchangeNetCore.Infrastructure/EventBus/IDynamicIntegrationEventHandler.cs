using System.Threading.Tasks;

namespace ExchangeNetCore.Infrastructure.EventBus
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
