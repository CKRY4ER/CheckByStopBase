using CheckByStopBase.CompanyStopBase.DAL.Repositories;
using CheckByStopBase.CompanyStopBase.Domain.Entities;
using CheckByStopBase.CompanyStopBase.Domain.Enums;
using Microsoft.Win32;

namespace CheckByStopBase.ServiceLayer.CompanyServices.Services;

public sealed class GetCompanyService : IGetCompanyService
{
    private readonly ICompanyRegistryRepository _registryRepository;
    private readonly ICompanyReportRepository _reportRepository;
    private readonly ICompanyGetOrCreateReportService _companyGetOrCreateReportService;

    public GetCompanyService(ICompanyRegistryRepository registryRepository,
        ICompanyReportRepository reportRepository,
        ICompanyGetOrCreateReportService companyGetOrCreateReportService)
    {
        _registryRepository = registryRepository;
        _reportRepository = reportRepository;
        _companyGetOrCreateReportService = companyGetOrCreateReportService;
    }

    public async Task<IEnumerable<CompanyRegistry>> GetByTaxNumbers(IEnumerable<string> taxNumbers, string partner)
    {
        Enum.TryParse(partner, out PartnerEnum enumPartner);
        var companies = _registryRepository.GetByTaxNumber(taxNumbers);

        if (FindeCompanies(companies))
            await AddToReport(companies, enumPartner);

        return companies;
    }

    private bool FindeCompanies(IEnumerable<CompanyRegistry> companies)
        => companies.Count() > 0;

    private async Task AddToReport(IEnumerable<CompanyRegistry> redCompanies, PartnerEnum partner)
    {
        var report = await _companyGetOrCreateReportService.GetOrCreate(partner);

        var reportCompanies = redCompanies.Select(x => new CompanyReportPosition
        {
            TaxNumber = x.TaxNumber,
            CompanyType = x.CompanyType,
            Report = report,
            ReportId = report.Id
        }).ToList();

        await _reportRepository.FillReport(report, reportCompanies);
    }
}

public interface IGetCompanyService
{
    public Task<IEnumerable<CompanyRegistry>> GetByTaxNumbers(IEnumerable<string> taxNumbers, string partner);
}