using MediatR;
using UseCases.ServicePrice.DTO;

namespace UseCases.ServicePrice.Queries.GetProcessPrice
{
    public class GetProcessPriceQuery : IRequest<string>
    {
        public CaseInfo CaseInfo { get; set; }
    }
}