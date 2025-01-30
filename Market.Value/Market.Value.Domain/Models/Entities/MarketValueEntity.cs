namespace Market.Value.Domain.Models.Entities
{
    public class MarketValueEntity
    {
        public int MarketValueId { get; set; }
        public int CoinId { get; set; }
        public DateTime LastUpdate { get; set; }
        public decimal Value { get; set; }
    }
}
