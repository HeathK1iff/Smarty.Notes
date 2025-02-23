using System.ComponentModel.DataAnnotations.Schema;

namespace Smarty.Notes.Entities;

[Table("Note")]
public sealed class Note
{
    [Column("Id")]
    public Guid Id { get; set; }
    
    [Column("Content")]
    public string Content { get; set; }
    
    [Column("Created")]
    public DateTime Created { get; set; }    
    
    [Column("CreatedBy")]
    public Guid CreatedBy { get; set; }
}

