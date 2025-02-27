using Smarty.Notes.Domain.Interfaces;

namespace Smarty.Notes.Entities;

public sealed class Tag : EntityBase
{
    public string Name { get; set; } = null!;
}
