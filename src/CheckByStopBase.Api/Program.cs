using CheckByStopBase.BackgroundServices.CompanyStopBase.ParserBackground.Configurations;
using Serilog;
using CheckByStopBase.CompanyStopBase.DAL;
using CheckByStopBase.ServiceLayer;
using CheckByStopBase.BackgroundServices;
using FluentValidation;
using FluentValidation.AspNetCore;
using CheckByStopBase.RegistryParsers;
using CheckByStopBase.RegistryParsers.Configurations;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

var connectionString = builder.Configuration.GetConnectionString("main")!;
var companyParserConfiguration = builder.Configuration.GetSection("CompanyParserConfiguration").Get<CompanyParserConfigurtionModel>()!;
var companySftpConfiguration = builder.Configuration.GetSection("CompanySftpConfiguration").Get<SftpConfigurationModel>()!;

builder.Services.AddCompanyRepositories(connectionString);
builder.Services.AddCompanyService();
builder.Services.AddCompanyParser(companySftpConfiguration);
builder.Services.AddCompanyParserBackgroundService(companyParserConfiguration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();