using LinqToDB.Mapping;

namespace Smarty.Notes.Infrastructure.Repositories.Entities;

[Table("Tags")]
public sealed class TagDb
{
    public Guid Id { get; set; }
    /// <summary>
    /// Name of tag  
    /// </summary>
    public string? Name { get; set; }

    public DateTime Created { get; set; }
}
