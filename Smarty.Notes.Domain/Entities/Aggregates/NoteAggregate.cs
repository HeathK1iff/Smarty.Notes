using Smarty.Notes.Domain.Interfaces;

namespace Smarty.Notes.Domain.Entities.Aggregates;

/// <summary>
/// Complex aggregate that contains note 
/// </summary>
public sealed class NoteAggregate : EntityBase, IAggregateRoot
{
    public string Content { get; set; } = null!;

    public ITags Tags { get; set; } = null!;

    public DateTime Created { get; set; }

    public Guid CreatedBy { get; set; }
}
