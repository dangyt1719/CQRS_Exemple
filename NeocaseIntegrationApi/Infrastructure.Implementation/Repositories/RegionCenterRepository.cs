using Dapper;
using Entities;
using Infrastructure.Implementation.Repositories.SqlQueries;
using Infrastructure.Interfaces.RepositoryInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace Infrastructure.Implementation.Repositories
{
    public class RegionCenterRepository : IRegionCenterRepository
    {
        private readonly ILogger<RegionCenterRepository> _logger;
        private readonly string _connectionString;

        public RegionCenterRepository(ILogger<RegionCenterRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("1C-ConnectionString");
        }

        public async Task<RegionCenter> GetStructureByRcId(Guid guid)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();

                var regionCenter = await connection.QueryFirstOrDefaultAsync<RegionCenter>(RegionCenterQueries.GetStructureByRcId, new { Guid = guid });
                return regionCenter;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}