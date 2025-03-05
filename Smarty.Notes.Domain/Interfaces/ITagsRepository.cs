using Smarty.Notes.Entities;

namespace Smarty.Notes.Domain.Interfaces;

/// <summary>
/// Interface of repository that provide access to tags
/// </summary>
public interface ITagsRepository
{
    /// <summary>
    /// Get tag by Id  
    /// </summary>
    /// <param name="tagId">Id of tag</param>
    /// <returns>Tag</returns>
    Task<Tag?> GetAsync(Guid tagId);

    /// <summary>
    /// Return list of tag for note 
    /// </summary>
    /// <param name="tagName">Text of tag</param>
    Task<Tag?> FindTagByNameAsync(string tagName);

    /// <summary>
    /// Add a new tag
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    Task<Guid> AddAsync(Tag tag);
}
