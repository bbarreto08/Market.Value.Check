using Amazon.Lambda.Core;
using Market.Value.Check.Models.Responses;
using Market.Value.Domain.Interfaces.Repositories;
using Market.Value.Infra.Context;
using Market.Value.Infra.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Market.Value.Check
{
    public class Function
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ServiceCollection _service;

        public Function()
        {
            _service = new ServiceCollection();

            ConfigureDependencies();

            _serviceProvider = _service.BuildServiceProvider();
        }

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input">The event for the Lambda function handler to process.</param>
        /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
        /// <returns></returns>
        public async Task FunctionHandler(ILambdaContext context)
        {
            await _serviceProvider.GetService<App>().Run();
        }

        private void ConfigureDependencies()
        {
            // Carrega as configurações do arquivo appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            // Adiciona a configuração ao serviço de injeção de dependência
            _service.AddSingleton<IConfiguration>(configuration);

            _service.AddSingleton<App>();
            _service.AddSingleton<MarketContext>();

            _service.AddScoped<IMarketValueRepository, MarketValueRepository>();
            _service.AddScoped<IMarketValueHistoryRepository, MarketValueHistoryRepository>();
        }
       
    }
}
