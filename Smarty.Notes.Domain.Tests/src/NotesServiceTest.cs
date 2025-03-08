using System.Text.Json;
using AutoMapper;
using Moq;
using NUnit.Framework.Internal;
using Smarty.Notes.Domain.Entities.Aggregates;
using Smarty.Notes.Domain.Interfaces;
using Smarty.Notes.Domain.Mappings;
using Smarty.Notes.Domain.Services;
using Smarty.Notes.Entities;

namespace Smarty.Notes.Domain.Tests;

[TestFixture]
public class NotesServiceTest
{
    readonly MapperConfiguration _mapperConfiration = new(cfg =>
        {
            cfg.AddProfile<EntityMapperProfile>();
        });


    [TestCase]
    public async Task GetNotesForUserAsync_CheckConstructionObject_ReturnCorrectNoteAggregate()
    {
        Guid noteId = Guid.NewGuid();
        Guid userId = Guid.NewGuid();
        Guid tagId = Guid.NewGuid();
        Guid tag2Id = Guid.NewGuid();

        var notesRepository = new Mock<INotesRepository>();
        notesRepository.Setup(a => a.GetAllForUserAsync(userId))
            .Returns((Guid id) => Task.FromResult<IEnumerable<Note>>([
                new(){
                    Id = noteId,
                    CreatedBy = userId,
                    Content = "xyz123"
                }
            ]));
        var noteTagLinkRepository = new Mock<INoteTagLinkRepository>();
        noteTagLinkRepository.Setup(a => a.GetAllForNoteAsync(noteId))
            .Returns((Guid id) => Task.FromResult<IEnumerable<Guid>>(new Guid[] { tagId, tag2Id }));
        var tagsRepository = new Mock<ITagsRepository>();
        tagsRepository.Setup(a => a.GetAsync(tagId))
            .Returns(Task.FromResult(new Tag()
            {
                Id = tagId,
                Name = "test"
            }));
        tagsRepository.Setup(a => a.GetAsync(tag2Id))
            .Returns(Task.FromResult(new Tag()
            {
                Id = tag2Id,
                Name = "test1"
            }));
        var currentContext = new Mock<ICurrentContext>();

        var target = new NoteService(notesRepository.Object, noteTagLinkRepository.Object,
           tagsRepository.Object, _mapperConfiration.CreateMapper(),
           currentContext.Object);

        var expected = JsonSerializer.Serialize(new NoteAggregate[]
        {
            new NoteAggregate
            {
                Id = noteId,
                CreatedBy = userId,
                Content = "xyz123",
                Tags = new Tags() {
                    "test", "test1"
                }
            }
        });

        var actual = JsonSerializer.Serialize(await target.GetNotesForUserAsync(userId));

        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase]
    public void AddAsync_PutNullValue_ArgumentNullException()
    {
        var notesRepository = new Mock<INotesRepository>();
        var noteTagLinkRepository = new Mock<INoteTagLinkRepository>();
        var tagsRepository = new Mock<ITagsRepository>();
        var currentContext = new Mock<ICurrentContext>();
        currentContext.Setup(a => a.GetCurrentUser())
            .Returns(Guid.NewGuid());
        currentContext.Setup(a => a.GetNow())
            .Returns(DateTime.Now);


        var target = new NoteService(notesRepository.Object, noteTagLinkRepository.Object,
            tagsRepository.Object, _mapperConfiration.CreateMapper(), currentContext.Object);

        Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            await target.AddAsync(null);
        });
    }

    [TestCase]
    public void AddAsync_CreateNoteWithTwoTags_Success()
    {
        var noteId = Guid.NewGuid();
        var notesRepository = new Mock<INotesRepository>();
        notesRepository.Setup(a => a.AddAsync(It.IsAny<Note>()))
            .Returns(Task.FromResult(noteId))
            .Verifiable();
        var tagId = Guid.NewGuid();
        var noteTagLinkRepository = new Mock<INoteTagLinkRepository>();
        noteTagLinkRepository.Setup(a => a.AddAsync(tagId, noteId))
            .Verifiable(Times.Exactly(1));
        var tagsRepository = new Mock<ITagsRepository>();
        tagsRepository.Setup(a => a.AddAsync(It.IsAny<Tag>()))
            .Returns(Task.FromResult(tagId))
            .Verifiable();
        var currentContext = new Mock<ICurrentContext>();
        var target = new NoteService(notesRepository.Object, noteTagLinkRepository.Object,
            tagsRepository.Object, _mapperConfiration.CreateMapper(), currentContext.Object);

        Assert.DoesNotThrowAsync(async () =>
        {
            await target.AddAsync(new NoteAggregate()
            {
                Tags = new Tags(){
                    "aaa"
                }
            });
        });

        notesRepository.Verify();
        noteTagLinkRepository.Verify();
        tagsRepository.Verify();
    }
}
