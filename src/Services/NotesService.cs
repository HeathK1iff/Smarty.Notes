using System.Transactions;
using AutoMapper;
using Smarty.Notes.Dto;
using Smarty.Notes.Entities;
using Smarty.Notes.Interfaces;

namespace Smarty.Notes.Services;

/// <summary>
/// Service class for handle add/edit/remove notice
/// </summary>
public sealed class NotesService
{
    readonly IMapper _mapper;
    readonly INotesRepository _notesRepository;
    readonly ICurrentContext _currentContext;
    readonly ITagsRepository _tagsRepository;
    readonly INoteTagLinksRepository _noteTagLinksRepository;

    public NotesService(INotesRepository notesRepository, ITagsRepository tagsRepository,
        INoteTagLinksRepository noteTagLinksRepository, IMapper mapper, 
        ICurrentContext currentContext)
    {
        _mapper = mapper;
        _noteTagLinksRepository = noteTagLinksRepository;
        _tagsRepository = tagsRepository;
        _notesRepository = notesRepository;
        _currentContext = currentContext;
    }

    /// <summary>
    /// Method is added a new notice 
    /// </summary>
    public async Task<NoteDto> AddAsync(NoteDto noteDto)
    {
        if (noteDto is null)
            throw new ArgumentNullException(nameof(noteDto));

        using var transactionScope = new TransactionScope();

        var newNote = await InsertNoteAsync(noteDto);
        await InsertOrUpdateTagsAsync(newNote, noteDto.Tags);

        transactionScope.Complete();

        return await GetAsync(newNote.Id);
    }

    /// <summary>
    /// Method is added notice 
    /// </summary>
    private async Task<Note> InsertNoteAsync(NoteDto dto)
    {
        if (dto is  null)
            throw new ArgumentNullException(nameof(dto));

        var newNote = _mapper.Map<Note>(dto);

        newNote.Created = _currentContext.GetNow();
        newNote.CreatedBy = _currentContext.GetCurrentUser().Id;
     
        return await _notesRepository.InsertAsync(newNote);
    }

    /// <summary>
    /// Method is add tags for notice 
    /// </summary>
    private async Task InsertOrUpdateTagsAsync(Note parent, TagDto[] tags)
    {
        if (parent is  null)
            throw new ArgumentNullException(nameof(parent));

        if (tags is not {Length: > 0})
            throw new ArgumentException(nameof(tags));

        var nameOfTags = tags.Select(i => i.Name).ToArray();
        var existingTags = await _tagsRepository.FindAllByTagsAsync(nameOfTags);

        var newTags = tags.ExceptBy(existingTags.Select(f => f.Name), f => f.Name);
        var noteTagLinks = new List<NoteTagLink>();

        foreach (var newTag in newTags)
        {
            var createdTag = await _tagsRepository.InsertAsync(new Tag()
            {
                Name = newTag.Name
            });

            await _noteTagLinksRepository.InsertAsync(new NoteTagLink(){
                NoteId = parent.Id,
                TagId = createdTag.Id
            }); 
        }

        foreach (var existingTag in existingTags)
        {
            await _noteTagLinksRepository.InsertAsync(new NoteTagLink(){
                NoteId = parent.Id,
                TagId = existingTag.Id
            });
        } 
    }

    /// <summary>
    /// Method is return constructed NoteDto by id 
    /// </summary>
    public async Task<NoteDto> GetAsync(Guid id)
    {
        var note = await _notesRepository.GetAsync(id);

        if (note is null)
          throw new InvalidDataException("Item not found");

        var noteTags = await _tagsRepository.FindAllByNoteId(id);

        return new NoteDto(){
            Id = id,
            Content = note.Content,
            Tags =  noteTags.Count() == 0 ? Array.Empty<TagDto>() : _mapper.Map<TagDto[]>(noteTags?.ToArray()) 
        };
    }

    public async Task UpdateAsync(NoteDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));
        
        var note = await _notesRepository.GetAsync(dto.Id);

        if (note is null)
            throw new InvalidDataException("Item not found");

        note.Content = dto.Content;

    }
}
