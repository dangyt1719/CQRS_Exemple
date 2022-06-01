using Dapper;
using Entities;
using Infrastructure.Implementation.Repositories.SqlQueries;
using Infrastructure.Interfaces.RepositoryInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace Infrastructure.Implementation.Repositories
{
    public class LegalDepartmentPriceRepository : ILegalDepartmentPriceRepository
    {
        private readonly ILogger<LegalDepartmentPriceRepository> _logger;
        private readonly string _connectionString;

        public LegalDepartmentPriceRepository(ILogger<LegalDepartmentPriceRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("OcoIntegrationsConnection");
        }

        public async Task<LigalDepartmentPrice> GetPriceByProcessId(int processId)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                return await connection.QueryFirstOrDefaultAsync<LigalDepartmentPrice>(LegalDepartmentPriceQueries.GetPriceByProcessId, new { processId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}