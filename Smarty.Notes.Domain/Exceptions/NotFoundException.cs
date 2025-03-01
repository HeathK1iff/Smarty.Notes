namespace Smarty.Notes.Domain.Exceptions;

/// <summary>
/// Exception class for situation when item not found in collection
/// </summary>
public class NotFoundException : DomainException
{
    public NotFoundException()
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}