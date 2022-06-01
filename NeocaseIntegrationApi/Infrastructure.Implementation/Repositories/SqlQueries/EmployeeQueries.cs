namespace Infrastructure.Implementation.Repositories.SqlQueries
{
    public static class EmployeeQueries
    {
        public const string GetEmployeeByCpIdQuery = @"SELECT distinct Pernr as Identifier,NACHN + ' ' + VORNA + ' ' + MIDNM as Name
                                                                 FROM [HQ-IB-1CZ-DB].[DBMD].[dbo].[ZHR_EMP_FULL]
                                                                 WHERE NACHN IS NOT NULL and VORNA IS NOT NULL and MIDNM IS NOT NULL and STATUSA <> 'Х' and PERSG='1' and Plans is not null and Grade is not null and gtext is not null
                                                                 and cp_id = @CpId";
    }
}