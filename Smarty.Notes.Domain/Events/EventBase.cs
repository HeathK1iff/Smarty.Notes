namespace Smarty.Notes.Domain.Events;

public abstract class EventBase
{
    public DateTime Created { get; init; }
    public string? Sender { get; init; }
    public Guid UserId { get; init; }
}
