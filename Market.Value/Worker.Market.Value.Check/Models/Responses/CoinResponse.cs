using System.Globalization;

namespace Worker.Market.Value.Check.Models.Responses
{
    internal class CoinResponse
    {
        public MarketData market_data { get; set; }
    }

    internal class MarketData
    {
        public CurrentPrice current_price { get; set; }
    }

    internal class CurrentPrice
    {
        public decimal brl { get; set; }

        public string GetBrl()
        {
            CultureInfo culturaBrasileira = new CultureInfo("pt-BR");
            return brl.ToString("C", culturaBrasileira);
        }
    }
}
