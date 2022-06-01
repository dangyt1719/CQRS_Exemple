using Infrastructure.Interfaces.RepositoryInterfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace UseCases.PowerAttorneyRegistry.Queries.GetPowerAttorneyExcelReport
{
    public class GetPowerAttorneyExcelReportQueryHandler : IRequestHandler<GetPowerAttorneyExcelReportQuery, byte[]>
    {
        private readonly ILogger<GetPowerAttorneyExcelReportQueryHandler> _logger;
        private readonly IPowerAttorneyRegistryRepository _powerAttorneyRegistryRepository;

        public GetPowerAttorneyExcelReportQueryHandler(ILogger<GetPowerAttorneyExcelReportQueryHandler> logger, IPowerAttorneyRegistryRepository powerAttorneyRegistryRepository)
        {
            _logger = logger;
            _powerAttorneyRegistryRepository = powerAttorneyRegistryRepository;
        }

        public async Task<byte[]> Handle(GetPowerAttorneyExcelReportQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var allPowerAttorneys = await _powerAttorneyRegistryRepository.GetPowerAttorneyRegistriesAsync();

                var result = Array.Empty<byte>();
                if (allPowerAttorneys.Any())
                {
                    result = _powerAttorneyRegistryRepository.GetPowerAttorneyRegistriesReport(allPowerAttorneys);
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}