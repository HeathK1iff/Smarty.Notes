namespace Smarty.Notes.Domain.Interfaces;

public interface INoteTagLinkRepository
{
    Task AddAsync(Guid tagId, Guid notesId);
    Task DeleteAsync(Guid tagId, Guid notesId);
    Task<IEnumerable<Guid>> GetAllByNoteId(Guid id);
}