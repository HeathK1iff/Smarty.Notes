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
}
