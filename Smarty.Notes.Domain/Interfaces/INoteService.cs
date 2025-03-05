using Smarty.Notes.Domain.Entities.Aggregates;

namespace Smarty.Notes.Domain.Interfaces;

public interface INoteService
{
    Task<IEnumerable<NoteAggregate>> GetNotesForUserAsync(Guid userId);

    Task<Guid> AddAsync(NoteAggregate noteAggregate);
}
