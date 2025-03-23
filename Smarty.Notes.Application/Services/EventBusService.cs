
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Smarty.Notes.Domain.Events;
using Smarty.Notes.Domain.Interfaces;
using Smarty.Notes.Infrastructure.EventBus.Interfaces;
using Smarty.Notes.Infrastructure.Exceptions;

public class EventBusService : BackgroundService
{
    readonly IServiceProvider _serviceProvider;
    readonly IEventBusChannelFactory _eventBusConnection;
    readonly ICurrentContext _currentContext;

    public EventBusService(IEventBusChannelFactory eventBusConnection, ICurrentContext currentContext, IServiceProvider serviceProvider)
    {
        _eventBusConnection = eventBusConnection ?? throw new ArgumentNullException(nameof(eventBusConnection));
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _currentContext = currentContext ?? throw new ArgumentNullException(nameof(currentContext));

        _currentContext.GetInstanceId();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var channel = await _eventBusConnection.CreateAndDeclareExchangeAsync(stoppingToken);

        await channel.QueueDeclareAsync(queue: "notes_add", durable: true);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                switch (ea.RoutingKey)
                {
                    case "notes_add":
                        var handler = scope.ServiceProvider.GetKeyedService<IEventHandler<CreateNoteEvent>>(ea.RoutingKey);

                        if (handler is null)
                            throw new NotFoundDependencyException();

                        var @object = JsonSerializer.Deserialize<CreateNoteEvent>(message);

                        if (@object is null)
                            throw new JsonException();

                        await handler.ReceivedAsync(@object, stoppingToken);

                        break;
                }
            }
        };
    }
}