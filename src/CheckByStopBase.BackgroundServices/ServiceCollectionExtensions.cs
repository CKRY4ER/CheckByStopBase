using CheckByStopBase.BackgroundServices.CompanyStopBase.ParserBackground;
using CheckByStopBase.BackgroundServices.CompanyStopBase.ParserBackground.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace CheckByStopBase.BackgroundServices;

public static class ServiceCollectionExtensions
{
    public static void AddCompanyParserBackgroundService(this IServiceCollection collection, CompanyParserConfigurtionModel model)
    {
        collection.AddHostedService(sp => new CompanyParserBackgroundService(sp, model));
    }
}