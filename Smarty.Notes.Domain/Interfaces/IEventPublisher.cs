using Smarty.Notes.Domain.Events;

namespace Smarty.Notes.Domain.Interfaces;

public interface IEventPublisher
{
    public Task PublishAsync(EventBase @event, CancellationToken cancellationToken);
}
