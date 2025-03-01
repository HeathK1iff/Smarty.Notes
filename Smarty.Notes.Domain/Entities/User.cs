using Smarty.Notes.Domain.Interfaces;

namespace Smarty.Notes.Entities;

/// <summary>
/// Class of user entity
/// </summary>
public sealed class User : EntityBase
{
    /// <summary>
    /// Id of User in external system
    /// </summary>
    /// <value></value>
    public Guid ExternalId { get; set; }
}