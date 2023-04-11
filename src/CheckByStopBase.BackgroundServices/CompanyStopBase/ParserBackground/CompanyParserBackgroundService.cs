using CheckByStopBase.BackgroundServices.CompanyStopBase.ParserBackground.Configurations;
using CheckByStopBase.RegistryParsers.CompanyParsers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CheckByStopBase.BackgroundServices.CompanyStopBase.ParserBackground;

public sealed class CompanyParserBackgroundService : BackgroundService
{
    private readonly IServiceProvider _provider;
    private readonly CompanyParserConfigurtionModel _configuration;
    private readonly ICompanyParser _parser;
    private readonly ILogger _logger;

    public CompanyParserBackgroundService(IServiceProvider provider, CompanyParserConfigurtionModel configuration)
    {
        _provider = provider;
        _configuration = configuration;

        using var scope = _provider.CreateScope();
        _parser = scope.ServiceProvider.GetRequiredService<ICompanyParser>();
        _logger = scope.ServiceProvider.GetRequiredService<ILogger<CompanyParserBackgroundService>>();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var periodicTimer = new PeriodicTimer(TimeSpan.FromMinutes(_configuration.RetryMinute));

        while (await periodicTimer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                await _parser.Parse();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Loading the registry for \"Red companies\" failed with an error!");
            }
        }
    }
}