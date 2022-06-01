using MediatR;
using NeocaseProviderLibrary.Dto;

namespace UseCases.Relations.Queries.GetAllRelations
{
    public class GetAllRelationsQuery : IRequest<IEnumerable<ContactRelationDto>>
    {
    }
}