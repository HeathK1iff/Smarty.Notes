using Smarty.Notes.Domain.Interfaces;

namespace Smarty.Notes.Infrastructure
{
    public class CurrentContext : ICurrentContext
    {
        public Guid GetCurrentUser()
        {
            throw new NotImplementedException();
        }

        public DateTime GetNow()
        {
            return DateTime.Now;
        }
    }
}