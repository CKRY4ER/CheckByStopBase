using CheckByStopBase.CompanyStopBase.DAL.DataContext.Configurations;
using CheckByStopBase.CompanyStopBase.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CheckByStopBase.CompanyStopBase.DAL.DataContext;

public sealed class CompanyDbContext : DbContext
{
    public CompanyDbContext(DbContextOptions<CompanyDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CompanyRegistryConfiguration());
        modelBuilder.ApplyConfiguration(new CompanyReportConfiguration());
        modelBuilder.ApplyConfiguration(new CompanyReportPositionConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<CompanyRegistry> Registry { get; set; }
    public DbSet<CompanyReport> Report { get; set; }
    public DbSet<CompanyReportPosition> ReportPosition { get; set; }
}