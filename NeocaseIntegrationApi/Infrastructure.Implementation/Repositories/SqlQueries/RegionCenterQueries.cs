namespace Infrastructure.Implementation.Repositories.SqlQueries
{
    public static class RegionCenterQueries
    {
        public const string GetStructureByRcId = @"SELECT [GUID1C] as Id
                                                      ,[REGION_CENTER] as Title
                                                      ,loc.LOCATION as Location
                                                  FROM [HQ-IB-1CZ-DB].[DBMD].[dbo].[ZHR_REGION_CENTER] as rc
                                                  Join [HQ-IB-1CZ-DB].[DBMD].[dbo].[ZHR_REGION_LOCATION] as loc on loc.GUID1C_REGION_CENTER=rc.GUID1C
                                                  where GUID1C=@guid";
    }
}