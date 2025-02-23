using System.Transactions;
using Smarty.Notes.Dto;
using Smarty.Notes.Entities;
using Smarty.Notes.Interfaces;

namespace Core.Services;

public sealed class NotesService
{
    readonly INotesRepository _notesRepository;
    readonly ICurrentContext _currentContext;
    readonly ITagsRepository _tagsRepository;
    readonly INoteTagLinksRepository _noteTagLinksRepository;

    public NotesService(INotesRepository notesRepository, ITagsRepository tagsRepository,
        INoteTagLinksRepository noteTagLinksRepository, ICurrentContext currentContext)
    {
        _noteTagLinksRepository = noteTagLinksRepository;
        _tagsRepository = tagsRepository;
        _notesRepository = notesRepository;
        _currentContext = currentContext;
    }

    /// <summary>
    /// Метод добавляет новую заметку 
    /// </summary>
    public async Task<NoteDto> AddAsync(NoteDto noteDto)
    {
        if (noteDto is null)
        {
            throw new ArgumentNullException(nameof(noteDto));
        }

        using var transactionScope = new TransactionScope();

        var newNote = await InsertNoteAsync(noteDto);
        await InsertOrUpdateTagsAsync(newNote, noteDto.Tags);

        transactionScope.Complete();

        return Get(newNote.Id);
    }

    /// <summary>
    /// Метод добавляет заметку в сущности 
    /// </summary>
    private async Task<Note> InsertNoteAsync(NoteDto dto)
    {
        var newNote = new Note();

        newNote.Created = _currentContext.GetNow();
        newNote.CreatedBy = _currentContext.GetCurrentUser().Id;
        newNote.Content = dto.Content;

        return await _notesRepository.InsertAsync(newNote);
    }

    /// <summary>
    /// Метод добавляет теги к заметки 
    /// </summary>
    private async Task InsertOrUpdateTagsAsync(Note parent, TagDto[] tags)
    {
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

    public NoteDto Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Update(NoteDto note)
    {
        
    }
}
