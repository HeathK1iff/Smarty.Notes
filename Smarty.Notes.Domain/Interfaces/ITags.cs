namespace Smarty.Notes.Domain.Interfaces;

/// <summary>
/// Interface for list of tags of note
/// </summary>
public interface ITags : IEnumerable<string>
{
    /// <summary>
    /// Create a new tag into collection. If note is exist then throw exception
    /// </summary>
    /// <param name="name"></param>
    void Add(string name);

    /// <summary>
    /// Remove tag from collection, if not is not exist then throw exception
    /// </summary>
    /// <param name="name"></param>
    void Remove(string name);
    
    /// <summary>
    /// Return list of tags as array
    /// </summary>
    /// <returns></returns>
    public string[] ToArray();
}
