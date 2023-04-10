using CheckByStopBase.CompanyStopBase.Domain.Entities;
using CheckByStopBase.RegistryParsers.CompanyParsers.CsvConverter.Mappers;
using CheckByStopBase.RegistryParsers.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text;

namespace CheckByStopBase.RegistryParsers.CompanyParsers.CsvConverter;

public sealed class CompanyCsvConverter : ICompanyCsvConverter
{
    public IEnumerable<CompanyRegistry> Convert(MemoryStream stream)
    {
        IEnumerable<CompanyRegistry> companyRegistry = null;

        var config = new CsvConfiguration(CultureInfo.CurrentCulture) { Delimiter = ";", Encoding = Encoding.UTF8 };

        using (var reader = new StreamReader(stream, Encoding.UTF8))
        using (var csv = new CsvReader(reader, config))
        {
            csv.Context.RegisterClassMap<CompanyRegistryMap>();
            companyRegistry = csv.GetRecords<CompanyRegistry>().ToList();
        }

        return companyRegistry;
    }
}

public interface ICompanyCsvConverter : ICsvConverter<CompanyRegistry>
{
}