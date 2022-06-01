using MediatR;
using NeocaseProviderLibrary.Providers;

namespace UseCases.CaseHistory.Queries.GetCaseHistory
{
    public class GetCaseHistoryHandler : IRequestHandler<GetCaseHistoryQuery, int>
    {
        private readonly NeocaseRootProvider _neocase;

        public GetCaseHistoryHandler(NeocaseRootProvider neocase)
        {
            _neocase = neocase;
        }

        public async Task<int> Handle(GetCaseHistoryQuery request, CancellationToken cancellationToken)
        {
            int result = await _neocase.NeocaseCaseProvider.GetCaseHistoryCount(long.Parse(request.CaseId), request.StepId);
            return result;
        }
    }
}