namespace Smarty.Notes.Domain.Entities;

/// <summary>
/// Class of tag entity
/// </summary>
public sealed class Tag : EntityBase
{
    /// <summary>
    /// Name of tag  
    /// </summary>
    public required string Name { get; set; }
}
