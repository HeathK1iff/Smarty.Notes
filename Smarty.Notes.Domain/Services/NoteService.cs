using System.Net.Http.Headers;
using System.Transactions;
using AutoMapper;
using Smarty.Notes.Domain.Entities.Aggregates;
using Smarty.Notes.Domain.Interfaces;
using Smarty.Notes.Entities;

namespace Smarty.Notes.Domain.Services;

public class NoteService : INoteService
{
    readonly INoteTagLinkRepository _noteTagLinkRepository;
    readonly ITagsRepository _tagsRepository;
    readonly INotesRepository _notesRepository;
    readonly IMapper _mapper;
    readonly ICurrentContext _currentContext;

    public NoteService(INotesRepository notesRepository, INoteTagLinkRepository noteTagLinkRepository,
        ITagsRepository tagsRepository, IMapper mapper, ICurrentContext currentContext)
    {
        _mapper = mapper ?? throw new ArgumentException(nameof(notesRepository));
        _notesRepository = notesRepository ?? throw new ArgumentNullException(nameof(mapper));
        _noteTagLinkRepository = noteTagLinkRepository ?? throw new ArgumentNullException(nameof(noteTagLinkRepository));
        _tagsRepository = tagsRepository ?? throw new ArgumentNullException(nameof(tagsRepository));
        _currentContext = currentContext ?? throw new ArgumentNullException(nameof(currentContext));
    }

    public async Task<Guid> AddAsync(NoteAggregate noteAggregate)
    {
        if (noteAggregate is null)
        {
            throw new ArgumentNullException(nameof(noteAggregate));
        }

        using var scope = new TransactionScope();

        var note = _mapper.Map<Note>(noteAggregate);
        note.CreatedBy = _currentContext.GetCurrentUser();
        note.Created = _currentContext.GetNow();
        Guid noteId = await _notesRepository.AddAsync(note);
        
        await AddOrUpdateTagsAsync(noteId, noteAggregate.Tags);

        scope.Complete();

        return noteId;
    }

    private async Task AddOrUpdateTagsAsync(Guid noteId, ITags tags)
    {
        using var scope = new TransactionScope();

        foreach (string tagName in tags)
        {
            Tag? tag = await _tagsRepository.FindTagByNameAsync(tagName);
            
            if (tag is null)
            {
                tag = new Tag()
                {
                    Name = tagName
                };

                Guid tagId = await _tagsRepository.AddAsync(tag);

                await _noteTagLinkRepository.AddAsync(tagId, noteId);
            }
            else
            {
                await _noteTagLinkRepository.AddAsync(tag.Id, noteId);
            }
        }

        scope.Complete();
    }

    public async Task<IEnumerable<NoteAggregate>> GetNotesForUserAsync(Guid userId)
    {
        IEnumerable<Note> notes = await _notesRepository.GetAllForUserAsync(userId);

        var result = _mapper.Map<NoteAggregate[]>(notes);
        var tasks = new List<Task>();

        Array.ForEach(result, item =>
        {
            var task = new Task(async () =>
            {
                var linkIds = await _noteTagLinkRepository.GetAllForNoteAsync(item.Id);
                foreach (var linkId in linkIds)
                {
                    Tag? tag = await _tagsRepository.GetAsync(linkId);
                    
                    if (tag is null)
                      continue;

                    item.Tags.Add(tag.Name);
                }
            });

            task.Start();
            tasks.Add(task);
        });

        await Task.WhenAll(tasks);

        return result;
    }
}
