using FluentMigrator;

namespace Smarty.Notes.Infrastructure.Migrations
{
    [Migration(20240220_211700)]
    public class EmptyDB_Migration : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Create.Table("User")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("ExternalId").AsGuid().Unique();

            Create.Table("Note")
                .WithColumn("Id").AsGuid().PrimaryKey().Unique()
                .WithColumn("Content").AsString()
                .WithColumn("Created").AsDate()
                .WithColumn("CreatedBy").AsGuid().ForeignKey("User", "Id");

            Create.Table("Tag")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Name").AsString();
            
            Create.Table("NoteTagLink")
                .WithColumn("NoteId").AsGuid().ForeignKey("Note", "Id").PrimaryKey()
                .WithColumn("TagId").AsGuid().ForeignKey("Tag", "Id").PrimaryKey();
        }
    }
}