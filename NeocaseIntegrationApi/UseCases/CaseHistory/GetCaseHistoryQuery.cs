using MediatR;

namespace UseCases.CaseHistory.Queries.GetCaseHistory
{
    public class GetCaseHistoryQuery : IRequest<int>
    {
        public string CaseId { get; set; }

        public long StepId { get; set; }
    }
}