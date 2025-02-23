
using System.Reflection;
using FluentMigrator.Runner;

namespace Smarty.Notes.Utils.Extensions;

public static class FluentMigratorExtensions
{
    public static void AddFluentMigrator(this IServiceCollection services, IConfiguration configuration)
    {

        string connectionString = configuration.GetConnectionString("main");

        services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
    }

    public static void UseDatabase(this WebApplication webApplication)
    {
        using var scope = webApplication.Services.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }
}
