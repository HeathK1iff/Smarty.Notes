namespace Smarty.Notes.Domain.Exceptions;

/// <summary>
/// Base class of domain exception
/// </summary>
public abstract class DomainException : Exception
{
    protected DomainException()
    {
    }

    protected DomainException(string message) : base(message)
    {
    }

    protected DomainException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
