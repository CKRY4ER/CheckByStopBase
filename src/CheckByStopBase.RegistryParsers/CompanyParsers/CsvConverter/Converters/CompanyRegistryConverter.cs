using CheckByStopBase.CompanyStopBase.Domain.Enums;
using System.Globalization;

namespace CheckByStopBase.RegistryParsers.CompanyParsers.CsvConverter.Converters;

public sealed class CompanyRegistryConverter
{
    private readonly IDictionary<string, CompanyTypeEnum> _companyType;

    public CompanyRegistryConverter()
    {
        _companyType = new Dictionary<string, CompanyTypeEnum>()
        {
            { "ЮЛ", CompanyTypeEnum.Person },
            { "ИП", CompanyTypeEnum.LegalEntity }
        };
    }

    public CompanyTypeEnum GetCompanyType(string companyType)
        => _companyType[companyType];

    public DateTime GetDateTime(string date)
        => DateTime.SpecifyKind(DateTime.ParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture), DateTimeKind.Utc);
}