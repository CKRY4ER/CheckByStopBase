using CheckByStopBase.CompanyStopBase.Domain.Enums;
using CheckByStopBase.Domain.Entities;

namespace CheckByStopBase.CompanyStopBase.Domain.Entities;

public sealed class CompanyReportPosition : Entity
{
    public string TaxNumber { get; set; }

    public CompanyTypeEnum CompanyType { get; set; }

    public CompanyReport Report { get; set; }

    public long ReportId { get; set; }
}