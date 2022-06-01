using MediatR;
using NeocaseProviderLibrary.Dto;
using NeocaseProviderLibrary.Providers;

namespace UseCases.NeocaseContacts.Queries.GetAllNeocaseContacts
{
    public class GetAllNeocaseContactsQueryHandler : IRequestHandler<GetAllNeocaseContactsQuery, IEnumerable<ContactDto>>
    {
        private readonly NeocaseRootProvider _neocase;

        public GetAllNeocaseContactsQueryHandler(NeocaseRootProvider neocase)
        {
            _neocase = neocase;
        }

        public async Task<IEnumerable<ContactDto>> Handle(GetAllNeocaseContactsQuery request, CancellationToken cancellationToken)
        {
            return await _neocase.NeocaseDbProvider.GetAllContacts();
        }
    }
}