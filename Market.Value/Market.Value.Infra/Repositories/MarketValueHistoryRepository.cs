using Dapper;
using Market.Value.Domain.Interfaces.Repositories;
using Market.Value.Domain.Models.Entities;
using Market.Value.Infra.Context;

namespace Market.Value.Infra.Repositories
{
    public class MarketValueHistoryRepository : IMarketValueHistoryRepository
    {
        private readonly MarketContext _context;

        public MarketValueHistoryRepository(MarketContext context)
        {
            _context = context;
        }

        public async Task Add(MarketValueHistoryEntity marketValueEntity)
        {
            var query = @"  
                            INSERT INTO MarketValueHistory
                            VALUES(@CoinId, @Value, @Date, @FullDate);";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, marketValueEntity);
            }
        }

        public async Task<MarketValueHistoryEntity> GetInitialValue(int coinId)
        {
            var query = @"
                             SELECT TOP 1 *
                             FROM MarketValueHistory m
                             WHERE m.CoinId = 2
                             AND m.Date = CONVERT(DATE, GETDATE())
                             ORDER BY m.FullDate
                            ";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<MarketValueHistoryEntity>(query, new { coinId });
            }
        }
    }
}
