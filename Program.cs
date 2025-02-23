using Smarty.Notes.Utils.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFluentMigrator(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseDatabase();
app.Run();