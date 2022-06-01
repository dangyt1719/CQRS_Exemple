using Infrastructure.Interfaces.RepositoryInterfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using UseCases.ServicePrice.Enum;

namespace UseCases.ServicePrice.Queries.GetProcessPrice
{
    public class GetProcessPriceQueryHandler : IRequestHandler<GetProcessPriceQuery, string>
    {
        private readonly ILogger<GetProcessPriceQueryHandler> _logger;
        private readonly ILegalDepartmentPriceRepository _legalDepartmentPriceRepository;

        public GetProcessPriceQueryHandler(ILogger<GetProcessPriceQueryHandler> logger, ILegalDepartmentPriceRepository legalDepartmentPriceRepository)
        {
            _logger = logger;
            _legalDepartmentPriceRepository = legalDepartmentPriceRepository;
        }

        public async Task<string> Handle(GetProcessPriceQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.CaseInfo is null)
                    throw new ArgumentException($"Input parameter CaseInfo is null.");

                var servicePrice = await _legalDepartmentPriceRepository.GetPriceByProcessId(request.CaseInfo.ProcessId);
                if (servicePrice is null)
                    throw new ArgumentException($"Prices for process {request.CaseInfo.ProcessId} is not exist.");

                if (!System.Enum.TryParse<LegalDepartmentPriceColumns>(request.CaseInfo.ColumnNumber.ToString(), out var columnName))
                    throw new ArgumentException($"ColumnNumber is not exist.");

                try
                {
                    return servicePrice.GetType().GetProperty(columnName.ToString()).GetValue(servicePrice, null).ToString();
                }
                catch (Exception)
                {
                    throw new ArgumentException($"ColumnNumber is not exist.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}