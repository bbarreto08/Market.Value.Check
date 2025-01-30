using Dapper;
using Market.Value.Domain.Interfaces.Repositories;
using Market.Value.Domain.Models.Entities;
using Market.Value.Infra.Context;

namespace Market.Value.Infra.Repositories
{
    public class MarketValueRepository : IMarketValueRepository
    {
        private readonly MarketContext _context;

        public MarketValueRepository(MarketContext context)
        {
            _context = context;
        }

        public async Task<MarketValueEntity> Get(int coinId)
        {
            var query = @"
                            SELECT *
                            FROM MarketValue m
                            WHERE m.CoinId = @coinId;";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<MarketValueEntity>(query, new { coinId });
            }
        }

        public async Task Add(MarketValueEntity marketValue)
        {
            var query = @"

                            IF EXISTS (
	                            SELECT *
	                            FROM MarketValue mv
	                            WHERE mv.CoinId = @CoinId
                            )
                            BEGIN 
	                            UPDATE MarketValue
	                            SET LastUpdate = @LastUpdate,
		                            Value = @Value
                                WHERE CoinId = @CoinId      
                            END
                            ELSE
                            BEGIN
	                            INSERT INTO MarketValue
	                            VALUES(@CoinId, @LastUpdate, @Value)
                            END ";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, marketValue);
            }
        }
    }
}
