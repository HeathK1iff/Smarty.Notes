using Smarty.Notes.Domain.Collections;
using Smarty.Notes.Domain.Entities;
using Smarty.Notes.Domain.Events;
using Smarty.Notes.Domain.Interfaces;

namespace Smarty.Notes.Domain.Services;

public class AddNoteEventService : IEventHandler<CreateNoteEvent>
{
    readonly INoteRepository _noteRepository;
    readonly ICreateNoteRequestUploader _uploader;
    public AddNoteEventService(INoteRepository noteRepository, 
        ICreateNoteRequestUploader uploader)
    {
        _noteRepository = noteRepository ?? throw new ArgumentNullException(nameof(noteRepository)); 
        _uploader = uploader ?? throw new ArgumentNullException(nameof(uploader));
    }

    public async Task ReceivedAsync(CreateNoteEvent @event, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(@event.UrlForRequest))
        {
            throw new ArgumentNullException();
        }
        
        var request = await _uploader.GetNoteAsync(@event.UrlForRequest, cancellationToken);
        
        var newNote = new Note(){
            Content = request.Content,
            CreatedBy = request.CreatedBy,
            Tags = new Tags(request?.Tags ?? Array.Empty<string>())
        };
        
        await _noteRepository.AddAsync(newNote, cancellationToken);
    }
}
