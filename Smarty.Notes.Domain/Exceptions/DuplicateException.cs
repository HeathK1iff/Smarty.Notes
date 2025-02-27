using System.Runtime.Serialization;

namespace Smarty.Notes.Domain.Exceptions;

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
