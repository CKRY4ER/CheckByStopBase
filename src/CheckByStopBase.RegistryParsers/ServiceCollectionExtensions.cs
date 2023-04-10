using CheckByStopBase.RegistryParsers.CompanyParsers;
using CheckByStopBase.RegistryParsers.CompanyParsers.CsvConverter;
using CheckByStopBase.RegistryParsers.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace CheckByStopBase.RegistryParsers;

public static class ServiceCollectionExtensions
{
    public static void AddCompanyParser(this IServiceCollection collection, SftpConfigurationModel sftpConfiguration)
    {
        collection.AddSingleton(sftpConfiguration);
        collection.AddScoped<ICompanyCsvConverter, CompanyCsvConverter>();
        collection.AddScoped<ICompanyParser, CompanyParser>();
    }
}