using MediatR;
using NeocaseProviderLibrary.Dto;
using NeocaseProviderLibrary.Providers;

namespace UseCases.Conditions.Queries.GetAllConditions
{
    public class GetAllConditionsQueryHandler : IRequestHandler<GetAllConditionsQuery, IEnumerable<ConditionDto>>
    {
        private readonly NeocaseRootProvider _neocase;

        public GetAllConditionsQueryHandler(NeocaseRootProvider neocase)
        {
            _neocase = neocase;
        }

        public async Task<IEnumerable<ConditionDto>> Handle(GetAllConditionsQuery request, CancellationToken cancellationToken)
        {
            return await _neocase.NeocaseDbProvider.GetAllConditions();
        }
    }
}