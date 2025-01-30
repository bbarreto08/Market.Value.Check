using Market.Value.Domain.Models.Entities;

namespace Market.Value.Domain.Interfaces.Repositories
{
    public interface IMarketValueHistoryRepository
    {
        Task Add(MarketValueHistoryEntity marketValueEntity);
        Task<MarketValueHistoryEntity> GetInitialValue(int coinId);
    }
}
