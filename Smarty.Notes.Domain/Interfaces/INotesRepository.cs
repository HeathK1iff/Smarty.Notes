
using Smarty.Notes.Entities;

namespace Smarty.Notes.Domain.Interfaces;

public interface INotesRepository
{
    Task<Note> AddAsync(Note note);
    Task DeleteAsync(Guid id);
    Task UpdateAsync(Note note);
    Task<IEnumerable<Note>> GetAllForUserAsync(Guid userId);
}
