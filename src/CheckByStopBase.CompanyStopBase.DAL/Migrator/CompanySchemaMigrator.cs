using CheckByStopBase.CompanyStopBase.DAL.Migrator.DataContext;
using Microsoft.EntityFrameworkCore;

namespace CheckByStopBase.CompanyStopBase.DAL.Migrator;

public sealed class CompanySchemaMigrator : ICompanySchemaMigrator
{
    private readonly CompanySchemaMigratorDbContext _context;

    public CompanySchemaMigrator(CompanySchemaMigratorDbContext context)
    {
        _context = context;
    }

    public async Task CompanyMigrate()
        => await _context.Database.MigrateAsync();
}

public interface ICompanySchemaMigrator
{
    Task CompanyMigrate();
}