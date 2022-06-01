using MediatR;
using NeocaseProviderLibrary.Dto;

namespace UseCases.NeocaseContacts.Queries.GetAllNeocaseContacts
{
    public class GetAllNeocaseContactsQuery : IRequest<IEnumerable<ContactDto>>
    {
    }
}