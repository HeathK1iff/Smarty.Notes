
using System.Data.Common;
using System.Text.Json;
using System.Threading.Tasks;
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
    const string TestContent = "test";
    readonly (Guid, string)[] _tagsForTest = [
        (Guid.NewGuid(), "a"),
        (Guid.NewGuid(), "b"),
        (Guid.NewGuid(), "c")
        ];
    readonly Note _noteForTest = new Note()
    {
        Id = Guid.NewGuid(),
        Content = TestContent,
        Created = DateTime.Today,
        CreatedBy = Guid.NewGuid()
    };
    readonly MapperConfiguration _mapperConfiration = new(cfg =>
        {
            cfg.AddProfile<EntityMapperProfile>();
        });

    public async Task GetNotesForUserAsync_TryToGetNotExisting_ThrowNotFoundException()
    {
        throw new NotImplementedException();
    }

    [TestCase]
    public async Task GetNotesForUserAsync_CheckCorrectReturn_ReturnCorrectNoteAggregate()
    {
        var notesRepository = new Mock<INotesRepository>();
        notesRepository.Setup(f => f.GetAllForUserAsync(It.IsAny<Guid>()))
            .Returns((Guid id) => Task.FromResult((IEnumerable<Note>) [_noteForTest]));
        var noteTagLinkRepository = new Mock<INoteTagLinkRepository>();
        noteTagLinkRepository.Setup(m => m.GetAllForNoteAsync(It.IsAny<Guid>()))
            .Returns((Guid id) => Task.FromResult(id == _noteForTest.Id ? _tagsForTest.Select(e => e.Item1) : null));
        var tagsRepository = new Mock<ITagsRepository>();
        tagsRepository
            .Setup(m => m.GetAsync(It.IsAny<Guid>()))
            .Returns((Guid id) => Task.FromResult(_tagsForTest.Select(m => new Tag() { Id = m.Item1, Name = m.Item2 })));
        var target = new NoteService(notesRepository.Object, noteTagLinkRepository.Object,
            tagsRepository.Object, _mapperConfiration.CreateMapper());
        var expected = JsonSerializer.Serialize(new NoteAggregate[]
        {
            new NoteAggregate
            {
                Id = _noteForTest.Id,
                Content = _noteForTest.Content,
                Created = _noteForTest.Created,
                CreatedBy = _noteForTest.CreatedBy,
                Tags = new Tags() { _tagsForTest.Select(m=>m.Item2).ToArray() }
            }
        });


        var actual = JsonSerializer.Serialize(await target.GetNotesForUserAsync(_noteForTest.Id));

        Assert.That(actual, Is.EqualTo(expected));

    }
}
