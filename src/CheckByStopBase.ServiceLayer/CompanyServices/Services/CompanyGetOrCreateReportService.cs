using CheckByStopBase.CompanyStopBase.DAL.Repositories;
using CheckByStopBase.CompanyStopBase.Domain.Entities;
using CheckByStopBase.CompanyStopBase.Domain.Enums;

namespace CheckByStopBase.ServiceLayer.CompanyServices.Services;

public sealed class CompanyGetOrCreateReportService : ICompanyGetOrCreateReportService
{
    private readonly ICompanyReportRepository _reportRepository;

    public CompanyGetOrCreateReportService(ICompanyReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<CompanyReport> GetOrCreate(PartnerEnum partner)
    {
        var report = _reportRepository.GetReportByPartner(partner);

        if (ReportAlreadyExists(report) is false)
            report = await CreateReport(partner);

        return report;
    }

    private bool ReportAlreadyExists(CompanyReport report) => report != null;

    private async Task<CompanyReport> CreateReport(PartnerEnum processing)
    {
        DateTimeOffset date = new DateTimeOffset(DateTime.UtcNow);

        date = date.ToOffset(new TimeSpan(3, 0, 0));

        var report = new CompanyReport()
        {
            CreateDate = date.Date,
            Partner = processing,
            Companies = new List<CompanyReportPosition>()
        };

        await _reportRepository.SaveReport(report);

        return report;
    }
}

public interface ICompanyGetOrCreateReportService
{
    Task<CompanyReport> GetOrCreate(PartnerEnum partner);
}