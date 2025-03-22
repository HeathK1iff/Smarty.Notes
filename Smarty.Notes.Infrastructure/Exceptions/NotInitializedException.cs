namespace Smarty.Notes.Infrastructure.Exceptions;

public class NotInitializedException : InfrastructureException
{
    public NotInitializedException()
    {
    }

    public NotInitializedException(string? message) : base(message)
    {
    }

    public NotInitializedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
