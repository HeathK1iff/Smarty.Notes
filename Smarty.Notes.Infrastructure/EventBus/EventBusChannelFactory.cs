using FluentValidation;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Smarty.Notes.Infrastructure.EventBus.Interfaces;
using Smarty.Notes.Infrastructure.Options;
using Smarty.Notes.Infrastructure.Validation;

public class EventBusChannelFactory : IEventBusChannelFactory
{
    private const string ClientProvidedName =  "smarty.notes";

    IConnection? _connection;
    readonly EventBusOptions _options;

    public EventBusChannelFactory(IOptions<EventBusOptions> options)
    {
        _options = options.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task<IChannel> CreateAndDeclareExchangeAsync(CancellationToken cancellationToken)
    {
        if (_connection is null)
        {
            EventBusOptionsValidator validator = new EventBusOptionsValidator();
            validator.ValidateAndThrow(_options);
           
            var factory = new ConnectionFactory
            {
                UserName = _options.UserName!,
                Password = _options.Password!,
                VirtualHost = "/",
                HostName = _options.HostName!,
                ClientProvidedName = ClientProvidedName
            };
            _connection = await factory.CreateConnectionAsync(cancellationToken);
        }

        var channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);

        await channel.ExchangeDeclareAsync("Smarty.Notes", type: ExchangeType.Direct);

        return channel;
    }
}
