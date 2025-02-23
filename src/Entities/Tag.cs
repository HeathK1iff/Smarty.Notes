using System.ComponentModel.DataAnnotations.Schema;

namespace Smarty.Notes.Entities;

[Table("Tag")]
public sealed class Tag
{
    [Column("Id")]
    public Guid Id { get; set; }

    [Column("Name")]
    public string Name { get; set; }
}
