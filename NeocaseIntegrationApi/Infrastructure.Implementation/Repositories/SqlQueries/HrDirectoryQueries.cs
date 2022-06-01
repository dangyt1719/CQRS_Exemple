namespace Infrastructure.Implementation.Repositories.SqlQueries
{
    public static class HrDirectoryQueries
    {
        public const string GetHrDirectoryByRCMvz = @"SELECT [Id]
                                                         ,[MvzId]
                                                         ,[RegionCenterId]
                                                         ,[HRDirectorIdentifier]
                                                         ,[HRMvzIdentifier]
                                                         ,[HRRegionCenterIdentifier]
                                                         ,[Notifying]
                                                       FROM [OcoIntegration].[dbo].[HRDirectories]
                                                       WHERE MvzId=@mvzId and RegionCenterId=@regionCenterId";
    }
}