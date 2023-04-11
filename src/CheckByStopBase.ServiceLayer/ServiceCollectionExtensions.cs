using CheckByStopBase.ServiceLayer.CompanyServices.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CheckByStopBase.ServiceLayer;

public static class ServiceCollectionExtensions
{
    public static void AddCompanyService(this IServiceCollection collection)
    {
        collection.AddScoped<ICompanyGetOrCreateReportService, CompanyGetOrCreateReportService>();
        collection.AddScoped<IGetCompanyService, GetCompanyService>();
    }
}