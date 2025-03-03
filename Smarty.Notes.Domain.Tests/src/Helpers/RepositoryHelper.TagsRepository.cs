using Moq;
using Smarty.Notes.Domain.Interfaces;
using Smarty.Notes.Entities;

namespace Smarty.Notes.Domain.Tests.Helpers;

public static partial class RepositoryHelper
{
    public static ITagsRepository CreateTagsRepositoryMock(List<Tag> tags)
    {
        var repository = new Mock<ITagsRepository>();
        
        repository
            .Setup(m => m.GetAsync(It.IsAny<Guid>()))
            .Returns((Guid id) => Task.FromResult<IEnumerable<Tag>>(tags));

        return repository.Object;
    }
}
