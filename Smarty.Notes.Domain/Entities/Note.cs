using Smarty.Notes.Domain.Interfaces;

namespace Smarty.Notes.Entities;

public sealed class Note : EntityBase
{ 
    public string Content { get; set; } = null!;
 
    public DateTime Created { get; set; }    
    
    public Guid CreatedBy { get; set; }
}

