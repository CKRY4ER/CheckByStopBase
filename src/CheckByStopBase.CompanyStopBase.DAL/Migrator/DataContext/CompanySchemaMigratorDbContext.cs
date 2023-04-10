using Microsoft.EntityFrameworkCore;

namespace CheckByStopBase.CompanyStopBase.DAL.Migrator.DataContext;

public sealed class CompanySchemaMigratorDbContext : DbContext
{
    public CompanySchemaMigratorDbContext(DbContextOptions<CompanySchemaMigratorDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CompanySchemaMigratorDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}