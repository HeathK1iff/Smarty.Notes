using LinqToDB.Mapping;

namespace Smarty.Notes.Infrastructure.Repositories.Entities;

/// <summary>
/// Class of note entity 
/// </summary>
[Table("Notes")]
public sealed class NoteDb
{ 
    /// <summary>
    /// Id of entity
    /// </summary>
    [PrimaryKey, Identity]
    public Guid Id {get;set;}
    /// <summary>
    /// Content of note
    /// </summary>
    public string? Content { get; set; }
 
    /// <summary>
    ///  Date of create of record 
    /// </summary>
    public DateTime Created { get; set; }    
    
    /// <summary>
    /// Id of user who created it 
    /// </summary>
    public Guid CreatedBy { get; set; }
}

