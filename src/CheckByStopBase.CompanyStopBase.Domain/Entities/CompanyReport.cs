using CheckByStopBase.CompanyStopBase.Domain.Enums;
using CheckByStopBase.Domain.Entities;

namespace CheckByStopBase.CompanyStopBase.Domain.Entities;

public sealed class CompanyReport : Entity
{
    public DateTime CreateDate { get; set; }

    public PartnerEnum Partner { get; set; }

    public List<CompanyReportPosition> Companies { get; set; }
}