using Smarty.Notes.Entities;

namespace Smarty.Notes.Interfaces;

public interface INoteTagLinksRepository
{
    public Task InsertAsync(NoteTagLink link);
}
