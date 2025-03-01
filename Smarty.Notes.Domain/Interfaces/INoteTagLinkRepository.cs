namespace Smarty.Notes.Domain.Interfaces;

/// <summary>
/// Interface provide access to links between note and tags
/// </summary>
public interface INoteTagLinkRepository
{
    /// <summary>
    /// Provide possibility to async add link to storage
    /// </summary>
    /// <param name="tagId"></param>
    /// <param name="notesId"></param>
    /// <returns></returns>
    Task AddAsync(Guid tagId, Guid notesId);
    
    /// <summary>
    /// Provide possibility to async delete link
    /// </summary>
    /// <param name="tagId"></param>
    /// <param name="notesId"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid tagId, Guid notesId);

    /// <summary>
    /// Return list of ids of Tags for note
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IEnumerable<Guid>> GetAllForNote(Guid noteId);
}