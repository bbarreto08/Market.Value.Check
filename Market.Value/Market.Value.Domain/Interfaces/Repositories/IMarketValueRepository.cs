using Market.Value.Domain.Models.Entities;

namespace Market.Value.Domain.Interfaces.Repositories
{
    public interface IMarketValueRepository
    {
        Task<MarketValueEntity> Get(int coinId);
        Task Add(MarketValueEntity marketValue);
    }
}
