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

    public NoteService(INotesRepository notesRepository, INoteTagLinkRepository noteTagLinkRepository,
        ITagsRepository tagsRepository, IMapper mapper)
    {
        _mapper = mapper ?? throw new ArgumentException(nameof(notesRepository));
        _notesRepository = notesRepository ?? throw new ArgumentNullException(nameof(mapper));
        _noteTagLinkRepository = noteTagLinkRepository ?? throw new ArgumentNullException(nameof(noteTagLinkRepository));
        _tagsRepository = tagsRepository ?? throw new ArgumentNullException(nameof(tagsRepository));
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
