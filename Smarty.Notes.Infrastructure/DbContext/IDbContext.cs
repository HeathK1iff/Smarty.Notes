using LinqToDB;
using LinqToDB.Data;
using Smarty.Notes.Infrastructure.Repositories.Entities;

namespace Smarty.Notes.Infrastructure.DbContext;

public interface IDbContext
{
    ITable<NoteDb> Notes { get; }
    ITable<NoteTagLinkDb> NoteTagLinks { get; }
    ITable<TagDb> Tags { get; }
}
