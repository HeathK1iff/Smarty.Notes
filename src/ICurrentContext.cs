
using Smarty.Notes.Entities;

public interface ICurrentContext
{
    User GetCurrentUser();

    DateTime GetNow();
}
