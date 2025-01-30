namespace Market.Value.Domain.Models.Entities
{
    public class MarketValueHistoryEntity
    {
        public int MarketValueHistoryId { get; set; }
        public int CoinId { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public DateTime FullDate { get; set; }
        public CoinEntity Coin { get; set; }
    }
}
