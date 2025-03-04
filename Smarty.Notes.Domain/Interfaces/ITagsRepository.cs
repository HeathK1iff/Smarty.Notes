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
    Task<IEnumerable<Tag>> GetAsync(Guid tagId);

    /// <summary>
    /// Provide existing of object in storage by Guid
    /// </summary>
    /// <param name="id"></param>
    /// <returns>True if object is exist</returns>
    Task<bool> IsExistAsync(Guid id); 
}
