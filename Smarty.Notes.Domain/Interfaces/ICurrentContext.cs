namespace Smarty.Notes.Domain.Interfaces;

public interface ICurrentContext
{
    Guid GetInstanceId();

    DateTime GetNow();
}
