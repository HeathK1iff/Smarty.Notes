using LinqToDB;
using Smarty.Notes.Infrastructure.Repositories.Entities;

namespace Smarty.Notes.Infrastructure.DbContext;

public class DbContext : LinqToDB.Data.DataConnection, IDbContext
{
    public ITable<NoteDb> Notes => this.GetTable<NoteDb>();
    public ITable<NoteTagLinkDb> NoteTagLinks => this.GetTable<NoteTagLinkDb>();
    public ITable<TagDb> Tags => this.GetTable<TagDb>();
}
