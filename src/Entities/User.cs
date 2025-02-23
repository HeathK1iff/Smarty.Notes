using System.ComponentModel.DataAnnotations.Schema;

namespace Smarty.Notes.Entities;

[Table("User")]
public sealed class User
{
    [Column("Id")]
    public Guid Id { get; set; }
    
    [Column("ExternalId")]
    public Guid ExternalId { get; set; }
}