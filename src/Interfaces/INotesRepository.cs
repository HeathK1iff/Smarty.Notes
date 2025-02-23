using Smarty.Notes.Entities;

namespace Smarty.Notes.Interfaces;

public interface INotesRepository
{
    Task<IEnumerable<Note>> GetAllAsync();
    Task<Note> GetAsync(Guid guid);
    Task<Note> InsertAsync(Note notes);
    Task<Note> FindAsync(Guid id);
    Task RemoveAsync(Note notes);
    Task UpdateAsync(Note notes);
}