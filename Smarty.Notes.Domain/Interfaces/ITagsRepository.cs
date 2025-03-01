using Smarty.Notes.Entities;

namespace Smarty.Notes.Domain.Interfaces;

/// <summary>
/// Interface of repository that provide access to tags
/// </summary>
public interface ITagsRepository
{
    /// <summary>
    /// Return list of tag for note  
    /// </summary>
    IEnumerable<Note> GetAllForNote(Guid noteId);

}
