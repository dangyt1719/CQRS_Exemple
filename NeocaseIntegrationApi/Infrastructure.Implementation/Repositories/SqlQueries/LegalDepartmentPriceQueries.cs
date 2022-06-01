namespace Infrastructure.Implementation.Repositories.SqlQueries
{
    public static class LegalDepartmentPriceQueries
    {
        public const string GetPriceByProcessId = @"Select [ProcessId]
                                                        ,[ProcessName]
                                                        ,[InternalCategory1Price]
                                                        ,[InternalCategory2Price]
                                                        ,[NotarialServicesPrice]
                                                        ,[ExternalCategory1Price]
                                                        ,[ExternalCategory2Price]
                                                    From [OcoIntegration].[dbo].[LegalDepartmentServicesPrice]
                                                    Where ProcessId = @processId";

        public const string GetAllPrices = @"Select [ProcessId]
                                                        ,[ProcessName]
                                                        ,[InternalCategory1Price]
                                                        ,[InternalCategory2Price]
                                                        ,[NotarialServicesPrice]
                                                        ,[ExternalCategory1Price]
                                                        ,[ExternalCategory2Price]
                                                    From [OcoIntegration].[dbo].[LegalDepartmentServicesPrice]";

        public const string SavePrice = @"INSERT INTO [dbo].[LegalDepartmentServicesPrice]
                                               ([ProcessId]
                                               ,[ProcessName]
                                               ,[InternalCategory1Price]
                                               ,[InternalCategory2Price]
                                               ,[NotarialServicesPrice]
                                               ,[ExternalCategory1Price]
                                               ,[ExternalCategory2Price])
                                         VALUES
                                               (@processId
                                               ,@processName
                                               ,@internalCategory1Price
                                               ,@internalCategory2Price
                                               ,@notarialServicesPrice
                                               ,@externalCategory1Price
                                               ,@externalCategory2Price)";
    }
}