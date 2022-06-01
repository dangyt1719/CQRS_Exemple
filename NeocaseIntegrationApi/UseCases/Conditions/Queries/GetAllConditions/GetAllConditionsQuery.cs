using MediatR;
using NeocaseProviderLibrary.Dto;

namespace UseCases.Conditions.Queries.GetAllConditions
{
    public class GetAllConditionsQuery : IRequest<IEnumerable<ConditionDto>>
    {
    }
}