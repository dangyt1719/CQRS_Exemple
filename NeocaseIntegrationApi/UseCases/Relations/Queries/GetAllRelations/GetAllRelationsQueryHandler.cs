using MediatR;
using NeocaseProviderLibrary.Dto;
using NeocaseProviderLibrary.Providers;

namespace UseCases.Relations.Queries.GetAllRelations
{
    public class GetAllRelationsQueryHandler : IRequestHandler<GetAllRelationsQuery, IEnumerable<ContactRelationDto>>
    {
        private readonly NeocaseRootProvider _neocase;

        public GetAllRelationsQueryHandler(NeocaseRootProvider neocase)
        {
            _neocase = neocase;
        }

        public async Task<IEnumerable<ContactRelationDto>> Handle(GetAllRelationsQuery request, CancellationToken cancellationToken)
        {
            return await _neocase.NeocaseDbProvider.GetAllRelations();
        }
    }
}