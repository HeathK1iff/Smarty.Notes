using Moq;
using Smarty.Notes.Domain.Interfaces;
using Smarty.Notes.Entities;

namespace Smarty.Notes.Domain.Tests.Helpers;

public static partial class RepositoryHelper
{
    public static INotesRepository CreateNotesRepositoryMock(List<Note> storage)
    {
        var notesRepository = new Mock<INotesRepository>();
        
        notesRepository
            .Setup(f => f.GetAllForUserAsync(It.IsAny<Guid>()))
            .Returns((Guid id) => Task.FromResult(storage.Where(n => n.CreatedBy == id)));

        notesRepository
                .Setup(f => f.AddAsync(It.IsAny<Note>()))
                .Returns((Note note) => 
                {
                    storage.Add(note);
                    return Task.FromResult(note);
                });

        notesRepository
                .Setup(f => f.DeleteAsync(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(() =>
                {
                    var noteForDelete = storage.FirstOrDefault(f => f.Id == id);

                    if (noteForDelete != null)
                    {
                        storage.Remove(noteForDelete);
                    }
                    ;
                }));

        notesRepository
                .Setup(f => f.UpdateAsync(It.IsAny<Note>()))
                .Returns((Note note) => Task.FromResult(() =>
                {
                    var noteForDelete = storage.FirstOrDefault(f => f.Id == note.Id);

                    if (noteForDelete != null)
                    {
                        storage.Remove(noteForDelete);
                    }
                    ;

                    storage.Add(note);
                }));

        return notesRepository.Object;
    }
}
