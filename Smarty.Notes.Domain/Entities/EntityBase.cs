namespace Smarty.Notes.Domain.Interfaces;

/// <summary>
/// Base class of entity
/// </summary>
public abstract class EntityBase
{
    /// <summary>
    /// Uniq Id of entity
    /// </summary>
    /// <value></value>
    public Guid Id { get; set; }
}
