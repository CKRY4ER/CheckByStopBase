using CheckByStopBase.CompanyStopBase.Domain.Enums;
using CheckByStopBase.Domain.Entities;

namespace CheckByStopBase.CompanyStopBase.Domain.Entities;

public sealed class CompanyRegistry : Entity
{
    public DateTime CreateDate { get; set; }

    public DateTime AddDate { get; set; }

    public string TaxNumber { get; set; } = null!;

    public CompanyTypeEnum CompanyType { get; set; }
}