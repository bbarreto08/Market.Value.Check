using Market.Value.Domain.Interfaces.Repositories;
using Market.Value.Infra.Context;
using Market.Value.Infra.Repositories;

var builder = Host.CreateApplicationBuilder(args);

// Carrega as configura��es do arquivo appsettings.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables()
    .Build();

// Adiciona a configura��o ao servi�o de inje��o de depend�ncia
builder.Services.AddSingleton<IConfiguration>(configuration);
builder.Services.AddSingleton<MarketContext>();
builder.Services.AddSingleton<IMarketValueRepository, MarketValueRepository>();
builder.Services.AddSingleton<IMarketValueHistoryRepository, MarketValueHistoryRepository>();
builder.Services.AddHostedService<Worker.Market.Value.Check.Worker>();

var host = builder.Build();
host.Run();


