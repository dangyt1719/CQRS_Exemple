namespace Infrastructure.Implementation.Repositories.SqlQueries
{
    public static class PowerAttorneyQueries
    {
        public const string AllPowerAttorneyRegistry = @"SELECT [Id]
                                                              ,[PowerAttorneyIndex]
                                                              ,[PowerAttorneyNumber]
                                                              ,[Year]
                                                              ,[DateIssue]
                                                              ,[PowerAttorneyType]
                                                              ,[Principal]
                                                              ,[WhoGranted]
                                                              ,[Powers]
                                                              ,[Attorney]
                                                              ,[Term]
                                                              ,[Initiator]
                                                          FROM [OcoIntegration].[dbo].[PowerAttorneysRegistry]
                                                          WHERE Year=@year";
    }
}