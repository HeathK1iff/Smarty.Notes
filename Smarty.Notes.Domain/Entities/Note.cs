using Smarty.Notes.Domain.Interfaces;

namespace Smarty.Notes.Entities;

/// <summary>
/// Class of note entity 
/// </summary>
public sealed class Note : EntityBase
{ 
    /// <summary>
    /// Content of note
    /// </summary>
    public string Content { get; set; } = null!;
 
    /// <summary>
    ///  Date of create of record 
    /// </summary>
    public DateTime Created { get; set; }    
    
    /// <summary>
    /// Id of user who created it 
    /// </summary>
    public Guid CreatedBy { get; set; }
}

