using Smarty.Notes.Domain.Interfaces;

namespace Smarty.Notes.Entities;

public sealed class User : EntityBase
{
    public Guid ExternalId { get; set; }
}