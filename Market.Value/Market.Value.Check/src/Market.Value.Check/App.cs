using Market.Value.Check.Models.Responses;
using Market.Value.Domain.Interfaces.Repositories;
using Market.Value.Domain.Models.Entities;
using Market.Value.Infra.Repositories;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Market.Value.Check
{
    public class App
    {
        public readonly IMarketValueRepository _marketValueRepository;
        public readonly IMarketValueHistoryRepository _marketValueHistoryRepository;
        public readonly IConfiguration _configuration;

        public App(IMarketValueRepository marketValueRepository, IMarketValueHistoryRepository marketValueHistoryRepository, IConfiguration configuration)
        {
            _marketValueRepository = marketValueRepository;
            _marketValueHistoryRepository = marketValueHistoryRepository;
            _configuration = configuration;
        }

        public async Task Run()
        {
            SentMessage("string title", "string message");

            var currentBtc = await GetBtc();
            var initialValue = await _marketValueHistoryRepository.GetInitialValue(2);

            if (currentBtc != null)
            {
                var insertValue = _marketValueRepository.Add(new MarketValueEntity
                {
                    CoinId = 2,
                    Value = currentBtc.market_data.current_price.brl,
                    LastUpdate = DateTime.Now
                });

                var insertHistory = _marketValueHistoryRepository.Add(new MarketValueHistoryEntity
                {
                    CoinId = 2,
                    Value = currentBtc.market_data.current_price.brl,
                    Date = DateTime.Now.Date,
                    FullDate = DateTime.Now
                });

                await Task.WhenAll(insertValue, insertHistory);
            }

            var diff = currentBtc.market_data.current_price.brl - initialValue.Value;

            var percentage = Math.Abs(diff / initialValue.Value);

            if(percentage > 0.02m)
            {
                SentMessage("Cotação BTC", $"Valor: {currentBtc?.market_data.current_price.GetBrl()} \n Variação: {(percentage*100).ToString("N4")}% ");
            }
        }

        private void SentMessage(string title, string message)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://api.pushover.net/1/messages.json?token={_configuration["pushover-token"]}&user={_configuration["pushover-user"]}&device={_configuration["pushover-device"]}&title={title}&message={message}");
            var response = client.Send(request);
            response.EnsureSuccessStatusCode();
        }

        private async Task<CoinResponse> GetBtc()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.coingecko.com/api/v3/coins/bitcoin");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<CoinResponse>(content);
            }
            else
            {
                return null;
            }
        }
    }
}
