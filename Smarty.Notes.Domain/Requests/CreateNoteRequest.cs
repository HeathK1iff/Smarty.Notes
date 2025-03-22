namespace Smarty.Notes.Entities;

public class CreateNoteRequest
{
    public string? Content { get; init; }
    public string[]? Tags { get; init; }
    public Guid CreatedBy { get; init; }
}
