using Dapper;
using Entities;
using Infrastructure.Implementation.Repositories.SqlQueries;
using Infrastructure.Interfaces.RepositoryInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace Infrastructure.Implementation.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(IConfiguration configuration, ILogger<EmployeeRepository> logger)
        {
            _logger = logger;
            _connectionString = configuration["ConnectionStrings:1C-ConnectionString"];
        }

        public async Task<Employee> GetEmployeeByCpId(string cpId)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);

                connection.Open();
                var identifier = await connection.QueryFirstOrDefaultAsync<Employee>(EmployeeQueries.GetEmployeeByCpIdQuery, new { CpId = cpId });

                return identifier;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}