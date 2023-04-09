using CheckByStopBase.CompanyStopBase.DAL.DataContext;
using CheckByStopBase.CompanyStopBase.Domain.Entities;
using CheckByStopBase.CompanyStopBase.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace CheckByStopBase.CompanyStopBase.DAL.Repositories;

public sealed class CompanyReportRepository : ICompanyReportRepository
{
    private readonly CompanyDbContext _context;
    private readonly DateTimeOffset _moscowTime = new DateTimeOffset(DateTime.UtcNow).ToOffset(new TimeSpan(3, 0, 0));

    public CompanyReportRepository(CompanyDbContext context)
        => _context = context;

    public async Task FillReport(CompanyReport report, IEnumerable<CompanyReportPosition> companies)
    {
        foreach (var company in companies)
        {
            company.ReportId = report.Id;
            company.Report = report;
            report.Companies.Add(company);
        }

        await _context.ReportPosition.AddRangeAsync(companies);
        await _context.SaveChangesAsync();
    }

    public CompanyReport? GetReportByPartner(PartnerEnum partner)
        => _context.Report
                .Where(r => r.Partner == partner)
                .Where(r => r.CreateDate.DayOfYear == _moscowTime.DayOfYear)
                .Where(r => r.CreateDate.Year == _moscowTime.Year)
                .Include(r => r.Companies)
                .FirstOrDefault();

    public async Task SaveReport(CompanyReport report)
    {
        await _context.Report.AddAsync(report);
        await _context.SaveChangesAsync();
    }
}

public interface ICompanyReportRepository
{
    CompanyReport? GetReportByPartner(PartnerEnum partner);

    Task SaveReport(CompanyReport report);

    Task FillReport(CompanyReport report, IEnumerable<CompanyReportPosition> companies);
}