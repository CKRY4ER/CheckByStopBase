using CheckByStopBase.CompanyStopBase.DAL.Repositories;
using CheckByStopBase.CompanyStopBase.Domain.Entities;
using CheckByStopBase.RegistryParsers.CompanyParsers.CsvConverter;
using CheckByStopBase.RegistryParsers.Configurations;
using Microsoft.Extensions.Logging;
using Renci.SshNet;
using Renci.SshNet.Async;
using Renci.SshNet.Sftp;

namespace CheckByStopBase.RegistryParsers.CompanyParsers;

public sealed class CompanyParser : ICompanyParser
{
    private readonly ICompanyRegistryRepository _companyRegistryRepository;
    private readonly SftpConfigurationModel _sftpConfiguration;
    private readonly ILogger _logger;
    private readonly ICompanyCsvConverter _csvConverter;

    public CompanyParser(ICompanyRegistryRepository companyRegistryRepository,
        SftpConfigurationModel sftpConfiguration,
        ILogger<CompanyParser> logger,
        ICompanyCsvConverter csvConverter)
    {
        _companyRegistryRepository = companyRegistryRepository;
        _sftpConfiguration = sftpConfiguration;
        _logger = logger;
        _csvConverter = csvConverter;
    }

    public async Task Parse()
    {
        IEnumerable<CompanyRegistry> registry;

        using (var memoryStream = new MemoryStream())
        using (var client = new SftpClient(_sftpConfiguration.Host, _sftpConfiguration.Port, _sftpConfiguration.UserName, _sftpConfiguration.Password))
        {
            client.Connect();

            if (!client.Exists($"{_sftpConfiguration.RemoteDirectory}/CompanyRegistry.csv"))
                return;

            var file = client.ListDirectory(_sftpConfiguration.RemoteDirectory)
                   .Where(f => ExcludeSystemFiles(f))
                   .First();

            _logger.LogInformation($"File found! Date file create: {file.LastWriteTime}");

            await client.DownloadAsync($"{_sftpConfiguration.RemoteDirectory}/{file.Name}", memoryStream);
            client.DeleteFile($"{_sftpConfiguration.RemoteDirectory}/{file.Name}");

            memoryStream.Position = 0;

            registry = _csvConverter.Convert(memoryStream);

            await _companyRegistryRepository.LoadNewRegistry(registry);
        }

        _logger.LogInformation("The loading of the registry for \"Companies\" has been successfully completed!");
    }

    private bool ExcludeSystemFiles(SftpFile file) =>
         !file.Name.StartsWith(".");
}

public interface ICompanyParser
{
    Task Parse();
}