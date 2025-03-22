using System.Collections;
using Smarty.Notes.Domain.Exceptions;

namespace Smarty.Notes.Domain.Collections;

/// <inheritdoc/>
public class Tags: IEnumerable<string>
{
    HashSet<string> _tags = new();
    IEqualityComparer<string> defaultComparer = EqualityComparer<string>.Create((a, b) =>
        string.Equals(a, b, StringComparison.CurrentCultureIgnoreCase));

    public int Count => throw new NotImplementedException();

    public bool IsReadOnly => throw new NotImplementedException();

    public Tags()
    {
    }

    public Tags(IEnumerable<string> strings) : this()
    {
        foreach (var @string in strings)
        {
            _tags.Add(@string);
        }
    }

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
        if (names is not { Length: > 0 })
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
    public string[] ToArray()
    {
        return _tags.ToArray();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
