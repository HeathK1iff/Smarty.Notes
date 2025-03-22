using RabbitMQ.Client;
using Smarty.Notes.Infrastructure.EventBus.Interfaces;

public class EventBusChannelFactory : IEventBusChannelFactory
{
    private IConnection? _connection;

    public async Task<IChannel> CreateAndDeclareExchangeAsync(CancellationToken cancellationToken)
    {
        if (_connection is null)
        {
            var factory = new ConnectionFactory
            {
                UserName = "sss",
                Password = "pass",
                VirtualHost = "/",
                HostName = "207.15.1.2",
                ClientProvidedName = "smarty.notes"
            };
            _connection = await factory.CreateConnectionAsync(cancellationToken);
        }

        var channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);

        await channel.ExchangeDeclareAsync("Smarty.Notes", type: ExchangeType.Direct);

        return channel;
    }
}
