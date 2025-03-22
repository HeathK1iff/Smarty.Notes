
using LinqToDB.Mapping;

namespace Smarty.Notes.Infrastructure.Repositories.Entities;

[Table("NoteTagLinks")]
public sealed class NoteTagLinkDb
{
    public Guid NoteId { get; set; }
    public Guid TagId { get; set; }
}
