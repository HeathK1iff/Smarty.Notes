using Smarty.Notes.Domain.Interfaces;

namespace Smarty.Notes.Domain.Services;

public class NoteService
{
    readonly INotesRepository _notesRepository;
    
    public NoteService(INotesRepository notesRepository)
    {
        _notesRepository = notesRepository ?? throw new ArgumentNullException();
    }
}
