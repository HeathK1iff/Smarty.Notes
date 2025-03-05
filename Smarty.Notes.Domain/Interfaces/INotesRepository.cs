
using Smarty.Notes.Entities;

namespace Smarty.Notes.Domain.Interfaces;

/// <summary>
/// Interface provide access to notes in storage
/// </summary>
public interface INotesRepository
{
    /// <summary>
    /// Provide possibility to async add note into storage
    /// </summary>
    /// <param name="note"></param>
    /// <returns>Note</returns>
    Task<Note> AddAsync(Note note);

    /// <summary>
    /// Provide existing of object in storage by Guid
    /// </summary>
    /// <param name="id"></param>
    /// <returns>True if object is exist</returns>
    Task<Note?> GetAsync(Guid id); 

    /// <summary>
    /// Provide possibility to async delete note into storage
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid id);
    
    /// <summary>
    /// Provide possibility to async update note
    /// </summary>
    /// <param name="note"></param>
    /// <returns></returns>
    Task UpdateAsync(Note note);

    /// <summary>
    /// Return list of note for user
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<IEnumerable<Note>> GetAllForUserAsync(Guid userId);
}
