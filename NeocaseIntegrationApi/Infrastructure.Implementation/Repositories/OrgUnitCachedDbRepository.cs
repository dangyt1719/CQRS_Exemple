using Dapper;
using Entities;
using Infrastructure.Interfaces.RepositoryInterfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace Infrastructure.Implementation.Repositories
{
    public class OrgUnitCachedDbRepository : IOrgUnitRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<OrgUnitCachedDbRepository> _logger;
        private readonly IMemoryCache _memoryCache;

        private readonly IConfiguration _configuration;

        public OrgUnitCachedDbRepository(IConfiguration configuration, ILogger<OrgUnitCachedDbRepository> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("OcoIntegrationsConnection");
            _memoryCache = memoryCache;
            _configuration = configuration;
        }

        private async Task<string> GetMvzCode(string code)
        {
            var connString = _configuration.GetConnectionString("1C-ConnectionString");
            using var connection = new SqlConnection(connString);

            connection.Open();
            var mvz = (await connection.QueryAsync<string>(mvzQuery, new { PERNR = code })).FirstOrDefault();
            return mvz;
        }

        private async Task<string> GetMvzName(string code)
        {
            using var connection = new SqlConnection(_connectionString);

            connection.Open();
            var mvzName = (await connection.QueryAsync<string>(mvzNameQuery, new { Mvz = code })).FirstOrDefault();
            return mvzName;
        }

        public async Task<string> GetRcByPernr(string pernr)
        {
            var conString = _configuration.GetConnectionString("Neocase");
            using var connection = new SqlConnection(conString);

            connection.Open();
            var rcName = (await connection.QueryAsync<string>(rcByPernrQuery, new { Pernr = pernr })).FirstOrDefault();
            return rcName;
        }

        public async Task<CaseFielsd> GetCaseFields(string caseNum)
        {
            var conString = _configuration.GetConnectionString("Neocase");
            using var connection = new SqlConnection(conString);

            connection.Open();
            var caseFielsd = (await connection.QueryAsync<CaseFielsd>(caseFieldsQuery, new { CaseNum = caseNum })).FirstOrDefault();
            return caseFielsd;
        }

        public async Task<OrgUnit> GetMvzInfo(string code)
        {
            string mvzCode = await GetMvzCode(code);

            string mvzName = await GetMvzName(mvzCode);

            var orgUnit = new OrgUnit
            {
                MvzCode = mvzCode,
                MvzName = mvzName
            };
            return orgUnit;
        }

        /// <summary>
        /// Получить список подразделений по текстовому шаблону
        /// </summary>
        /// <param name="namePattern">текстовый шаблон</param>
        /// <param name="maxCount">максимальное количество элементов возвращаемого списка</param>
        /// <returns></returns>
        public async Task<IEnumerable<OrgUnit>> GetOrgList(string namePattern, int maxCount)
        {
            var orgUnits = await GetAllOrgUnitsAsync();

            if (!string.IsNullOrEmpty(namePattern))
                orgUnits = orgUnits.Where(x => x.FullOrgPath.ToUpperInvariant().Contains(namePattern.ToUpperInvariant()));
            orgUnits = orgUnits.Take(maxCount);

            return orgUnits;
        }

        /// <summary>
        /// Закэшированный список всех подразделений
        /// </summary>
        public async Task<IEnumerable<OrgUnit>> GetAllOrgUnitsAsync()
        {
            return await _memoryCache.GetOrCreate(nameof(GetAllOrgUnitsAsync), async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromMinutes(30);

                using var connection = new SqlConnection(_connectionString);

                connection.Open();
                var orgList = await connection.QueryAsync<OrgUnit>(OrgUnitListQuery);

                return orgList;
            });
        }

        #region SQL

        private const string OrgUnitListQuery = $@"
;WITH cte AS
(
    SELECT
        cast(ORG_UNIT as nchar(8)) org_unit,
		ORG_NAME,
        ORG_PARENT,
		Boss_Name,
		BOSS_Pernr,
        1 AS HierarchyLevel,
        HierarchyPath = CONVERT(NVARCHAR(MAX), ORG_NAME)
    FROM hrdata.ZHR_ORG
    where ORG_UNIT=N'10003825' --ИБС - общий родитель для всех
    UNION ALL
    SELECT
        C.ORG_UNIT,
		C.ORG_NAME,
        C.ORG_PARENT,
		C.BOSS_NAME,
		C.BOSS_PERNR,
        HierarchyLevel = S.HierarchyLevel + 1,
        HierarchyPath= S.HierarchyPath + '/' + CONVERT(NVARCHAR(50), C.ORG_NAME)
    FROM hrdata.ZHR_ORG_VIEW  AS C
    INNER JOIN cte S  ON S.ORG_UNIT = C.ORG_PARENT
)
SELECT HierarchyLevel,ORG_PARENT CODEPARENT, org_unit AS Code, ORG_NAME AS [Name],
HierarchyPath AS FullOrgPath, BOSS_NAME BossName, BOSS_PERNR BossPernr  FROM cte
order by HierarchyLevel,ORG_NAME;";

        private const string mvzNameQuery = @"SELECT MvzText FROM OCOintegration.dbo.AllMvz where Mvz=@Mvz;";

        private const string caseFieldsQuery = @"select CHAMPI209 CourseCategory, CHAMPI211 CourseType from INTERVENTIONS_CHAMPVARS
where numero=@CaseNum";

        private const string rcByPernrQuery = @"select CHAMPU119 from NC16OCO.dbo.UTILISATEURS_CHAMPVARS ch
join NC16OCO.dbo.UTILISATEURS u on u.CODEUTILISATEUR=ch.CODEUTILISATEUR
where u.MATRICULE_UTILISATEUR=@pernr";

        private const string mvzQuery = @"SELECT distinct KOSTL
										FROM [HQ-IB-1CZ-DB].[DBMD].[dbo].[ZHR_EMP_FULL] as emp
										LEFT join [HQ-IB-1CZ-DB].[DBMD].[dbo].[ZHR_REGION_CENTER] as center on emp.GUID1C_REGION_CENTER=center.GUID1C
										join [HQ-IB-1CZ-DB].[DBMD].[dbo].[ZHR_PERSON_INFO] as inf on emp.PERNR=inf.PERNR
										WHERE NACHN IS NOT NULL and VORNA IS NOT NULL
											  and MIDNM IS NOT NULL and STATUSA <> 'Х'
											  and PERSG IN('1','R','6') and Plans is not null
											  and Grade is not null and gtext is not null
                                              and emp.PERNR=@PERNR";

        #endregion SQL
    }
}