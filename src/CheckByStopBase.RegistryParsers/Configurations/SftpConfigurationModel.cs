namespace CheckByStopBase.RegistryParsers.Configurations;

public sealed class SftpConfigurationModel
{
    public string Host { get; set; }

    public int Port { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public string RemoteDirectory { get; set; }
}