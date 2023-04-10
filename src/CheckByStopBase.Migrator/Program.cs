using CheckByStopBase.CompanyStopBase.DAL;
using CheckByStopBase.CompanyStopBase.DAL.Migrator;
using Serilog;

namespace CheckByStopBase.Migrator;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration))
            .ConfigureServices((hostContext, services) =>
            {
                var connectionString = hostContext.Configuration.GetConnectionString("main");
                services.AddPostgresSqlCompanySchemaMigrator(connectionString);
            })
           .Build();

        var scope = host.Services.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<ICompanySchemaMigrator>>();
        var dataAccessSchemaMigrator = scope.ServiceProvider.GetRequiredService<ICompanySchemaMigrator>();

        logger.LogInformation("Migrator - started");

        try
        {
            await dataAccessSchemaMigrator.CompanyMigrate();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Migration failed");
            throw;
        }

        logger.LogInformation("Migrator - stopped");
    }
}