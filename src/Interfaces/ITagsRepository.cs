using Smarty.Notes.Entities;

namespace Smarty.Notes.Interfaces;

public interface ITagsRepository
{
    Task<Tag> InsertAsync(Tag tag);
    Task<IEnumerable<Tag>> FindAllByTagsAsync(string[] tags);
}
