using System.Transactions;
using AutoMapper;
using LinqToDB;
using Smarty.Notes.Domain.Collections;
using Smarty.Notes.Domain.Entities;
using Smarty.Notes.Domain.Interfaces;
using Smarty.Notes.Infrastructure.DbContext;
using Smarty.Notes.Infrastructure.Repositories.Entities;

namespace Smarty.Notes.Domain.Services;

public class NoteRepository : INoteRepository
{
    readonly IDbContext _dbContext;
    readonly IMapper _mapper;
    readonly ICurrentContext _currentContext;

    public NoteRepository(IDbContext dbContext, IMapper mapper, ICurrentContext currentContext)
    {
        _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _currentContext = currentContext ?? throw new ArgumentNullException(nameof(currentContext));
    }

    public async Task<Guid> AddAsync(Note note, CancellationToken cancellationToken)
    {
        if (note is null)
        {
            throw new ArgumentNullException(nameof(note));
        }

        using (var transactionScope = new TransactionScope())
        {
            var noteDb = _mapper.Map<NoteDb>(note);
            noteDb.Id = Guid.NewGuid();

            await _dbContext.Notes.InsertAsync(() => noteDb, cancellationToken);

            await AddOrUpdateTagsAsync(noteDb.Id, note.Tags, cancellationToken);

            transactionScope.Complete();

            return noteDb.Id;
        }
    }

    private async Task AddOrUpdateTagsAsync(Guid noteId, Tags tags, CancellationToken cancellationToken)
    {
        foreach (string tagName in tags)
        {
            TagDb? tag = await _dbContext.Tags.FirstOrDefaultAsync(f => f.Name == tagName);

            if (tag is null)
            {
                tag = new TagDb()
                {
                    Id = Guid.NewGuid(),
                    Created = _currentContext.GetNow(),
                    Name = tagName
                };

                await _dbContext.Tags.InsertAsync(() => tag, cancellationToken);
            }

            await _dbContext.NoteTagLinks
                    .Value(f => f.NoteId, noteId)
                    .Value(f => f.TagId, tag.Id)
                    .InsertAsync();
        }
    }

    public async Task<IEnumerable<Note>> GetNotesForUserAsync(Guid userId)
    {
        var notes = _dbContext.Notes.Where(f => f.CreatedBy == userId).ToArray() ?? Array.Empty<NoteDb>();

        var result = _mapper.Map<Note[]>(notes);
        var tasks = new List<Task>();

        Array.ForEach(result, item =>
        {
            tasks.Add(Task.Run(() =>
            {
                var tags = from e in _dbContext.Tags
                           join c in _dbContext.NoteTagLinks on e.Id
                            equals c.TagId
                           where c.NoteId == item.CreatedBy
                           select e.Name;

                item.Tags.Add(tags.ToArray());
            }));
        });

        await Task.WhenAll(tasks);

        return result;
    }
}
