using Moq;
using Smarty.Notes.Domain.Interfaces;

namespace Smarty.Notes.Domain.Tests.Helpers;

public static partial class RepositoryHelper
{
    public static INoteTagLinkRepository CreateNoteTagLinkRepositoryMock(List<NoteLink> list)
    {
        var repository = new Mock<INoteTagLinkRepository>();

        repository.Setup(m => m.GetAllForNoteAsync(It.IsAny<Guid>()))
            .Returns((Guid id) => Task.FromResult(list.Where(item => item.NoteId == id).Select(f => f.TagId)));

        repository.Setup(m => m.AddAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .Returns((Guid tagId, Guid noteId) =>
            {

                list.Add(new NoteLink()
                {
                    NoteId = noteId,
                    TagId = tagId
                });

                return Task.CompletedTask;
            });
        
        repository
                .Setup(f => f.IsExistAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns((Guid tagId, Guid notesId) => Task.FromResult(list.Any(f => f.TagId == tagId && f.NoteId == notesId)));

        repository.Setup(m => m.DeleteAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .Returns((Guid tagId, Guid noteId) =>
            {
                var itemForDelete = list.FirstOrDefault(f => f.NoteId == noteId && f.TagId == tagId) ?? throw new IndexOutOfRangeException();
                list.Remove(itemForDelete);

                return Task.CompletedTask;
            });

        return repository.Object;
    }

    public class NoteLink
    {
        public Guid NoteId { get; set; }
        public Guid TagId { get; set; }
    }
}
