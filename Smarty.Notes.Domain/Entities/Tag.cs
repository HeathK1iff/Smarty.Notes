using Smarty.Notes.Domain.Interfaces;

namespace Smarty.Notes.Entities;

/// <summary>
/// Class of tag entity
/// </summary>
public sealed class Tag : EntityBase
{
    /// <summary>
    /// Name of tag  
    /// </summary>
    public required string Name { get; set; }

    public DateTime Created { get; set; }
}
