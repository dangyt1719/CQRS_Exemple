using MediatR;
using UseCases.Candidates.Dto;

namespace UseCases.Candidates.Commands.CreateCandidateToNeocase
{
    public class CreateCandidateToNeocaseCommand : IRequest<bool>
    {
        public CandidateDto Candidate { get; set; }
    }
}