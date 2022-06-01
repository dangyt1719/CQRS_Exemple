using Dapper;
using Entities;
using Infrastructure.Interfaces.RepositoryInterfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using NeocaseProviderLibrary.Providers;

namespace Infrastructure.Implementation.Repositories
{
    public class PermissionRepositiry : IPermissionRepositiry
    {
        private readonly string _connectionString;
        private readonly ILogger<OrgUnitCachedDbRepository> _logger;
        private readonly NeocaseRootProvider _neocase;

        private readonly IConfiguration _configuration;

        public PermissionRepositiry(IConfiguration configuration, ILogger<OrgUnitCachedDbRepository> logger, NeocaseRootProvider neocase)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("Neocase");
            _configuration = configuration;
            _neocase = neocase;
        }

        public async Task SaveSalaryDataToCase()
        {

        }

        public async Task<bool> HasPermissionForEmpMoneyAsync(string pernr, string userLogin, int[] roles)
        {
            if(await CheckOwn(pernr,userLogin))
                return true;
            foreach (var role in roles)
            {
                if (await CheckPermissionAsync(pernr, userLogin, role))
                    return true;
            }
            return false;
        }

        private async Task<bool> CheckOwn(string pernr, string userLogin)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var ownLogin = (await connection.QueryAsync<string>(ownLoginQuery, new { Pernr = pernr })).FirstOrDefault();
            if (userLogin.ToUpper().Contains(ownLogin.ToUpper()))
                return true;
            return false;

        }

        private async Task<bool> CheckPermissionAsync(string pernr, string userLogin, int role)
        {
            string relUserLogin = await GetUserLoginByRole(pernr, role);
            if (userLogin.ToUpper().Contains(relUserLogin.ToUpper()))
                return true;
            return false;
        }

        private async Task<string> GetUserLoginByRole(string pernr, int role)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var userLogin = (await connection.QueryAsync<string>(userLoginQuery, new { Role = role, Pernr = pernr })).FirstOrDefault();
            return userLogin;
        }

        #region SQL
        private const string userLoginQuery = @"
select SF_LOGIN, r.CODEUTILISATEUR1, r.CODEUTILISATEUR2 from NC16OCO.dbo.UTILISATEURS u join NC16OCO.dbo.UTILISATEURS_REL_CONTACTS r 
on u.CODEUTILISATEUR=r.CODEUTILISATEUR1
and r.CODERELATION=@role and r.CODEUTILISATEUR2=(
select CODEUTILISATEUR from NC16OCO.dbo.UTILISATEURS where MATRICULE_UTILISATEUR=@pernr)";

        private const string ownLoginQuery = @"
select SF_LOGIN from NC16OCO.dbo.UTILISATEURS
where MATRICULE_UTILISATEUR=@pernr";
        #endregion SQL
    }
}
