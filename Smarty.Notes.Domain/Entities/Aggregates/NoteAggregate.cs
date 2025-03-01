using Smarty.Notes.Domain.Interfaces;

namespace Smarty.Notes.Domain.Entities.Aggregates;

/// <summary>
/// Complex aggregate that contains note 
/// </summary>
public sealed class NoteAggregate : EntityBase, IAggregateRoot
{
    /// <summary>
    /// Content of note
    /// </summary>
    public string Content { get; set; } = string.Empty;

    public ITags Tags { get; } = new Tags();

    /// <summary>
    /// Date of create of record 
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Id of user who created it 
    /// </summary>
    public Guid CreatedBy { get; set; }
}
