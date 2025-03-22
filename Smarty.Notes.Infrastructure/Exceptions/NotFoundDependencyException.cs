namespace Smarty.Notes.Infrastructure.Exceptions;

public class NotFoundDependencyException : InfrastructureException
{
    public NotFoundDependencyException()
    {
    }

    public NotFoundDependencyException(string? message) : base(message)
    {
    }

    public NotFoundDependencyException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
