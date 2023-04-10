using CheckByStopBase.CompanyStopBase.DAL;
using CheckByStopBase.CompanyStopBase.DAL.Migrator;

namespace CheckByStopBase.Migrator;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                var connectionString = hostContext.Configuration.GetConnectionString("main");
                services.AddPostgresSqlCompanySchemaMigrator(connectionString);
            })
           .Build();

        var scope = host.Services.CreateScope();
        var dataAccessSchemaMigrator = scope.ServiceProvider.GetRequiredService<ICompanySchemaMigrator>();

        try
        {
            await dataAccessSchemaMigrator.CompanyMigrate();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}