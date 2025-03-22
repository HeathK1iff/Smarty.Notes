using Smarty.Notes.Domain.Collections;
using Smarty.Notes.Domain.Interfaces;

namespace Smarty.Notes.Domain.Entities;

/// <summary>
/// Complex aggregate that contains note 
/// </summary>
public sealed class Note : EntityBase, IAggregateRoot
{
    /// <summary>
    /// Content of note
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// List of tags
    /// </summary>
    /// <returns></returns>
    public Tags Tags { get; init; } = new();

    /// <summary>
    /// Date of create of record 
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Id of user who created it 
    /// </summary>
    public Guid CreatedBy { get; set; }
}
