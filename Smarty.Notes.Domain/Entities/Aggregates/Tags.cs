using System.Collections;
using Smarty.Notes.Domain.Exceptions;
using Smarty.Notes.Domain.Interfaces;

namespace Smarty.Notes.Domain.Entities.Aggregates;

/// <inheritdoc/>
public class Tags: ITags
{
    HashSet<string> _tags = new();
    IEqualityComparer<string> defaultComparer = EqualityComparer<string>.Create((a, b) =>  
        string.Equals(a, b, StringComparison.CurrentCultureIgnoreCase)); 
    
    /// <inheritdoc/>
    public void Add(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException(nameof(name));

        if (_tags.Contains(name, defaultComparer))
            throw new DuplicateException();

        _tags.Add(name);
    }

    /// <inheritdoc/>
    public void Add(string[] names)
    {
        if (names is not {Length: > 0})
          throw new ArgumentNullException(nameof(names));

        Array.ForEach(names, name => Add(name));
    }

    /// <inheritdoc/>
    public void Remove(string name)
    {
        if (!_tags.Contains(name, defaultComparer))
          throw new NotFoundException();

        _tags.Remove(name);
    }

    /// <inheritdoc/>
    public IEnumerator<string> GetEnumerator()
    {
        return _tags.GetEnumerator();
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <inheritdoc/>
    public string[] ToArray()
    {
        return _tags.ToArray();
    }
}
