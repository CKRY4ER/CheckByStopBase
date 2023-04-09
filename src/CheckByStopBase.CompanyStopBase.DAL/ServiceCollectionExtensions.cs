using CheckByStopBase.CompanyStopBase.DAL.DataContext;
using CheckByStopBase.CompanyStopBase.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CheckByStopBase.CompanyStopBase.DAL;

public static class ServiceCollectionExtensions
{
    public static void AddCompanyRepositories(this IServiceCollection collection, string connectionString)
    {
        collection.AddCompanyPostgresSqlRepository<ICompanyRegistryRepository, CompanyRegistryRepository, CompanyDbContext>(connectionString);
        collection.AddCompanyPostgresSqlRepository<ICompanyReportRepository, CompanyReportRepository, CompanyDbContext>(connectionString);
    }

    private static void AddCompanyPostgresSqlRepository<TRepository, TRepositoryImplementation, TRepositoryDbContext>(
        this IServiceCollection collection, string connectionString)
        where TRepository : class
        where TRepositoryImplementation : class, TRepository
        where TRepositoryDbContext : DbContext
    {
        collection.AddDbContextPool<TRepositoryDbContext>(builder =>
            builder.UseNpgsql(connectionString)
        );

        collection.AddScoped<TRepository, TRepositoryImplementation>();
    }
}