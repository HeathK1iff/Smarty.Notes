namespace Smarty.Notes.Domain.Exceptions;

/// <summary>
/// Exception class for situation when detected duplicate in collection of items
/// </summary>
public class DuplicateException : DomainException
{
    public DuplicateException(): this ("Item already exist")
    {
    }

    public DuplicateException(string message) : base(message)
    {
    }

    public DuplicateException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
