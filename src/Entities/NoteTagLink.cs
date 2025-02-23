using System.ComponentModel.DataAnnotations.Schema;

namespace Smarty.Notes.Entities;

[Table("NoteTagLink")]
public sealed class NoteTagLink
{
    [Column("NoteId")]
    public Guid NoteId { get; set; }

    [Column("TagId")]
    public Guid TagId { get; set; }
}