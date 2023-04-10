using CheckByStopBase.CompanyStopBase.Domain.Entities;
using CheckByStopBase.RegistryParsers.CompanyParsers.CsvConverter.Converters;
using CsvHelper.Configuration;

namespace CheckByStopBase.RegistryParsers.CompanyParsers.CsvConverter.Mappers;

public sealed class CompanyRegistryMap : ClassMap<CompanyRegistry>
{
    public CompanyRegistryMap()
    {
        var companyConverter = new CompanyRegistryConverter();

        Map(m => m.CreateDate).Convert(row => companyConverter.GetDateTime(row.Row.GetField<string>(0)));
        Map(m => m.AddDate).Convert(row => companyConverter.GetDateTime(row.Row.GetField<string>(1)));
        Map(m => m.TaxNumber).Name("inn");
        Map(m => m.CompanyType).Convert(row => companyConverter.GetCompanyType(row.Row.GetField<string>(3)));
    }
}