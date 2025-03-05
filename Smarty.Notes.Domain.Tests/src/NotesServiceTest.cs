using System.Data.Common;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework.Internal;
using Smarty.Notes.Domain.Entities.Aggregates;
using Smarty.Notes.Domain.Exceptions;
using Smarty.Notes.Domain.Interfaces;
using Smarty.Notes.Domain.Mappings;
using Smarty.Notes.Domain.Services;
using Smarty.Notes.Domain.Tests.Helpers;
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

    INotesRepository _notesRepository;
    INoteTagLinkRepository _noteTagLinkRepository;
    ITagsRepository _tagsRepository;


    readonly MapperConfiguration _mapperConfiration = new(cfg =>
        {
            cfg.AddProfile<EntityMapperProfile>();
        });
    
    [SetUp]
    public void Setup()
    {
        var notes = new List<Note> { _noteForTest };
        _notesRepository = RepositoryHelper.CreateNotesRepositoryMock(notes);
        
        var links = new List<RepositoryHelper.NoteLink>
            (
                _tagsForTest.Select(f => new RepositoryHelper.NoteLink()
                {
                    NoteId = _noteForTest.Id,
                    TagId = f.Item1
                }).ToArray()
            );
        _noteTagLinkRepository = RepositoryHelper.CreateNoteTagLinkRepositoryMock(links);        
        
        var tags = _tagsForTest.Select(m => new Tag() { Id = m.Item1, Name = m.Item2 }).ToList();
        _tagsRepository =  RepositoryHelper.CreateTagsRepositoryMock(tags);
    }

    [TestCase]
    public async Task GetNotesForUserAsync_CheckCorrectReturn_ReturnCorrectNoteAggregate()
    {
         var target = new NoteService(_notesRepository, _noteTagLinkRepository,
            _tagsRepository, _mapperConfiration.CreateMapper());
        
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

        var actual = JsonSerializer.Serialize(await target.GetNotesForUserAsync(_noteForTest.CreatedBy));

        Assert.That(actual, Is.EqualTo(expected));
    }
    
    [TestCase]
    public async Task AddAsync_PutNullValue_ArgumentNullException()
    {

    }
}
