using Moq;
using Smarty.Notes.Domain.Interfaces;
using Smarty.Notes.Entities;

namespace Smarty.Notes.Domain.Tests.Helpers;

public static partial class RepositoryHelper
{
    public static INotesRepository CreateNotesRepositoryMock(List<Note> list)
    {
        var notesRepository = new Mock<INotesRepository>();

        notesRepository
            .Setup(f => f.GetAllForUserAsync(It.IsAny<Guid>()))
            .Returns((Guid id) => Task.FromResult(list.Where(n => n.CreatedBy == id)));

        notesRepository
                .Setup(f => f.AddAsync(It.IsAny<Note>()))
                .Returns((Note note) =>
                {
                    list.Add(note);
                    return Task.FromResult(note);
                });

        notesRepository
                .Setup(f => f.IsExistAsync(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(list.Any(f => f.Id == id)));

        notesRepository
                .Setup(f => f.DeleteAsync(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(() =>
                {
                    var noteForDelete = list.FirstOrDefault(f => f.Id == id);

                    if (noteForDelete != null)
                    {
                        list.Remove(noteForDelete);
                    }
                    ;
                }));

        notesRepository
                .Setup(f => f.UpdateAsync(It.IsAny<Note>()))
                .Returns((Note note) => Task.FromResult(() =>
                {
                    var noteForDelete = list.FirstOrDefault(f => f.Id == note.Id);

                    if (noteForDelete != null)
                    {
                        list.Remove(noteForDelete);
                    }
                    ;

                    list.Add(note);
                }));

        return notesRepository.Object;
    }
}
