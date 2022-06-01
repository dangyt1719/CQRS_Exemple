using Dapper;
using Entities;
using Infrastructure.Implementation.Repositories.SqlQueries;
using Infrastructure.Interfaces.RepositoryInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace Infrastructure.Implementation.Repositories
{
    public class HrDirectoryRepository : IHrDirectoryRepository
    {
        private readonly ILogger<HrDirectoryRepository> _logger;
        private readonly string _connectionString;

        public HrDirectoryRepository(IConfiguration configuration, ILogger<HrDirectoryRepository> logger)
        {
            _connectionString = configuration.GetConnectionString("OcoIntegrationsConnection");
            _logger = logger;
        }

        public async Task<HrDirectory> GetHrDirectoryByRCMvz(Guid rcId, string mvz)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();

                var hRDirectory = await connection.QueryFirstOrDefaultAsync<HrDirectory>(HrDirectoryQueries.GetHrDirectoryByRCMvz, new { MvzId = mvz, RegionCenterId = rcId });
                return hRDirectory;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}