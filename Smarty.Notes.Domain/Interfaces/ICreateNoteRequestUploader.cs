using Smarty.Notes.Entities;

namespace Smarty.Notes.Domain.Interfaces;

public interface ICreateNoteRequestUploader
{
    Task<CreateNoteRequest> GetNoteAsync(string url, CancellationToken cancellationToken);  
}
