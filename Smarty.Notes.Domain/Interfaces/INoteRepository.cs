using Smarty.Notes.Domain.Entities;

namespace Smarty.Notes.Domain.Interfaces;

public interface INoteRepository
{
    Task<IEnumerable<Note>> GetNotesForUserAsync(Guid userId);

    Task<Guid> AddAsync(Note noteAggregate, CancellationToken cancellationToken);
}
