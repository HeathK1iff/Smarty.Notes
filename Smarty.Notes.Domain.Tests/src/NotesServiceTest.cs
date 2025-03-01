
using AutoMapper;
using Moq;
using Smarty.Notes.Domain.Interfaces;
using Smarty.Notes.Domain.Mappings;
using Smarty.Notes.Domain.Services;

namespace Smarty.Notes.Domain.Tests;

[TestFixture]
public class NotesServiceTest
{
    readonly MapperConfiguration _mapperConfiration = new(cfg =>
        {
            cfg.AddProfile<EntityMapperProfile>();
        });

    [TestCase]
    public void GetNotesForUserAsync_TryToGetNotExisting_ThrowNotFoundException()
    {   
        var notesRepository = new Mock<INotesRepository>();
        var noteTagLinkRepository = new Mock<INoteTagLinkRepository>();
        var tagsRepository = new Mock<ITagsRepository>();

        var actual = new NoteService(notesRepository.Object, noteTagLinkRepository.Object,
            tagsRepository.Object, _mapperConfiration.CreateMapper());
    }

    [TestCase]
    public void GetNotesForUserAsync_CheckCorrectReturn_ReturnNoteAggregate()
    {


    }
}
