using RabbitMQ.Client;

namespace Smarty.Notes.Infrastructure.EventBus.Interfaces;

public interface IEventBusChannelFactory
{
    Task<IChannel> CreateAndDeclareExchangeAsync(CancellationToken cancellationToken);
}
