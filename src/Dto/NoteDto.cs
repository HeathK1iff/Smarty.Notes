namespace Smarty.Notes.Dto;

public sealed class NoteDto
{
    public Guid Id { get; set; }
    
    public string Content { get; set; }

    public TagDto[] Tags { get; set;}
}

