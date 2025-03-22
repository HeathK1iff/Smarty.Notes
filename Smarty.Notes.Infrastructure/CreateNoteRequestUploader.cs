using Smarty.Notes.Domain.Interfaces;
using Smarty.Notes.Entities;

namespace Smarty.Notes.Infrastructure
{
    public class CreateNoteRequestUploader : ICreateNoteRequestUploader
    {
        public Task<CreateNoteRequest> GetNoteAsync(string url, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}