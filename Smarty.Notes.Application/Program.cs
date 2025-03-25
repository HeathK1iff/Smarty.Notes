using Smarty.Notes.Domain.Events;
using Smarty.Notes.Domain.Interfaces;
using Smarty.Notes.Domain.Services;
using Smarty.Notes.Infrastructure;
using Smarty.Notes.Infrastructure.DbContext;
using Smarty.Notes.Infrastructure.EventBus.Interfaces;
using Smarty.Notes.Infrastructure.Mappings;
using Smarty.Notes.Infrastructure.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile(Path.Combine(AppContext.BaseDirectory, "appsettings.json"));
builder.Services.AddOptions<EventBusOptions>()
    .Bind(builder.Configuration.GetSection(EventBusOptions.SectionName));

builder.Services.AddSingleton<IEventBusChannelFactory, EventBusChannelFactory>();
builder.Services.AddScoped<IEventPublisher, EventPublisher>();
builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<IDbContext, DbContext>();
builder.Services.AddSingleton<ICurrentContext, CurrentContext>();
builder.Services.AddScoped<IEventHandler<CreateNoteEvent>, AddNoteEventService>();
builder.Services.AddScoped<ICreateNoteRequestUploader, CreateNoteRequestUploader>();
builder.Services.AddHostedService<EventBusService>();
builder.Services.AddAutoMapper(a => a.AddProfile<EntityMapperProfile>());

var app = builder.Build();

app.Run();