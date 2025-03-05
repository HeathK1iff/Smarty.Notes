namespace Smarty.Notes.Domain.Interfaces;

public interface ICurrentContext
{
    Guid GetCurrentUser();

    DateTime GetNow();
}
