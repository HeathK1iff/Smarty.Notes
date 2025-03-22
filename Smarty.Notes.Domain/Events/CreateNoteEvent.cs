namespace Smarty.Notes.Domain.Events;

public class CreateNoteEvent : EventBase
{
    public string? UrlForRequest { get; init; }
}
