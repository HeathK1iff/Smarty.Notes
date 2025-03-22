using Smarty.Notes.Domain.Events;

namespace Smarty.Notes.Domain.Interfaces;

public interface IEventHandler<T> where T: EventBase
{
    Task ReceivedAsync(T @event, CancellationToken cancellationToken);
}
